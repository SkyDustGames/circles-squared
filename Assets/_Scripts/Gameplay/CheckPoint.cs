using UnityEngine;
using DG.Tweening;

public class CheckPoint : MonoBehaviour {
    
    bool activated;
    SpriteRenderer spriteRenderer;
    new Transform particleSystem;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !activated) {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player.lastCheckpoint = transform.position;

            spriteRenderer.DOColor(Color.green, 1f);
            particleSystem.DOScale(Vector3.zero, 1f);

            activated = true;
            AudioManager.instance.PlaySound("ActivatePad");

            CameraFollow.Shake();
        }
    }
}