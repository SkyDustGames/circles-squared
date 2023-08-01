using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerHealth>().Damage();
            return;
        }

        AudioManager.instance.PlaySound("BulletHit");
        ParticleManager.instance.CreateParticle(transform, "BulletExplosion");
    }
}