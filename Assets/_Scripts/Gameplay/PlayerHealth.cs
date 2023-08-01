using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public int lives;
    public float damagePreventionTime;
    [HideInInspector] public Vector3 lastCheckpoint;
    float damagePreventionTimer;
    SpriteRenderer spriteRenderer;
    TrailRenderer trailRenderer;

    public void Awake() {
        lastCheckpoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update() {
        if (damagePreventionTimer > 0)
            damagePreventionTimer -= Time.deltaTime;
    }

    public void Damage() {
        if (damagePreventionTimer > 0) return;

        lives--;
        ParticleManager.instance.CreateParticle(transform, "PlayerExplosion");
        CameraFollow.Shake();
        
        if (lives <= 0) {
            CanvasGroup group = GameObject.Find("YouLose").GetComponent<CanvasGroup>();
            group.DOFade(1f, 1f);
            group.interactable = true;
            group.blocksRaycasts = true;

            AudioManager.instance.PlaySound("Defeat");
            gameObject.SetActive(false);

            return;
        }

        transform.position = lastCheckpoint;
        damagePreventionTimer = damagePreventionTime;
        StartCoroutine(DamagePreventionAnimation());
        AudioManager.instance.PlaySound("Damage");
    }

    private IEnumerator DamagePreventionAnimation() {
        int steps = 6 * 2;
        float time = damagePreventionTime / steps;

        trailRenderer.enabled = false;
        for (int i = 0; i < steps; i++) {
            spriteRenderer.enabled = i % 2 == 1;
            yield return new WaitForSeconds(time);
        }
        trailRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Spike")) Damage();
    }
}