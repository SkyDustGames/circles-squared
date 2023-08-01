using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float speed;
    public Vector3 offset;
    float shakeTimer;
    float strength;
    static CameraFollow instance;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        if (shakeTimer > 0) {
            transform.position += Random.insideUnitSphere * strength;
            shakeTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, speed);
    }

    public static void Shake(float shakeDuration = 0.1f, float strength = 0.1f) {
        if (!Settings.cameraShake) return;
        instance.shakeTimer = shakeDuration;
        instance.strength = strength;
    }
}