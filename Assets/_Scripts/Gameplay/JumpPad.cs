using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float jumpForce;

    private void OnCollisionEnter2D(Collision2D other) {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            AudioManager.instance.PlaySound("Jump");
            ParticleManager.instance.CreateParticle(transform, "Jump");
        }
    }
}