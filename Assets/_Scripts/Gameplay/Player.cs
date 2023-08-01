using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float jumpTime;
    public CameraFollow follow;
    public Transform spawnPoint;
    float startSpeed;
    float jumpTimer;
    Rigidbody2D rb;
    float movement;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        startSpeed = speed;
    }

    private void Update() {
        spawnPoint.position = new(transform.position.x, transform.position.y - 0.5f);

        movement = Input.GetAxis("Horizontal");
        follow.offset.x = movement * (speed / startSpeed);

        if (Input.GetButtonDown("Jump") && jumpTimer < jumpTime) {
            ParticleManager.instance.CreateParticle(spawnPoint, "Jump", Quaternion.identity);
            AudioManager.instance.PlaySound("Jump");
        }

        if (Input.GetButton("Jump") && jumpTimer < jumpTime) {
            jumpTimer += Time.deltaTime;
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
            jumpTimer = jumpTime;
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(speed * movement * Time.deltaTime, rb.velocity.y);
    }

    public void Jump() {
        rb.AddForce(new Vector2(0, jumpForce * Time.deltaTime), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        jumpTimer = 0;

        if (other.gameObject.CompareTag("SpeedPad") && speed != startSpeed * 2) {
            StartCoroutine(SpeedBoost());
            AudioManager.instance.PlaySound("ActivatePad");
            CameraFollow.Shake();

            return;
        }

        AudioManager.instance.PlaySound("HitFloor");
        ParticleManager.instance.CreateParticle(spawnPoint, "Land", Quaternion.identity);
    }

    private IEnumerator SpeedBoost() {
        speed = startSpeed * 2;
        yield return new WaitForSeconds(10f);
        speed = startSpeed;
    }
}