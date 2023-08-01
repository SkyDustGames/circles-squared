using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public float radius;
    public LayerMask whatIsPlayer;
    public Vector3 other;
    Vector3 original;
    Vector3 current;

    private void Awake() {
        original = transform.position;
        current = other;
    }

    private void Update() {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        if (collider != null) {
            Vector3 move = Vector3.MoveTowards(transform.position, collider.transform.position, speed * Time.deltaTime);
            transform.position = new(move.x, transform.position.y);
        } else {
            Vector3 move = Vector3.MoveTowards(transform.position, current, speed * Time.deltaTime);
            transform.position = move;

            if (move.x.Equals(current.x))
                current = current.x == other.x ? original : other;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerHealth>().Damage();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Rigidbody2D>().AddForce(new(0, 20), ForceMode2D.Impulse);
            Destroy(gameObject);
            ParticleManager.instance.CreateParticle(transform, "EnemyExplosion");
            AudioManager.instance.PlaySound("KillEnemy");

            CameraFollow.Shake();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, other);
    }
}