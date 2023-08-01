using UnityEngine;

public class Turret : MonoBehaviour {
    
    public Transform graphics;
    public Transform firePoint;
    public Rigidbody2D bullet;
    public float time;
    public float bulletSpeed;
    float timer;

    private void Update() {
        graphics.Rotate(new Vector3(0, 0, timer * Time.deltaTime * 72));

        timer += Time.deltaTime;

        float a = timer / time * 0.1f;
        graphics.localScale = new Vector3(0.25f+a, 0.25f+a, 0.25f+a);

        if (timer >= time) {
            timer = 0;
            
            Rigidbody2D rb = Instantiate(bullet, firePoint.position, firePoint.rotation);
            rb.AddForce(transform.right * bulletSpeed);

            AudioManager.instance.PlaySound("Damage");
            ParticleManager.instance.CreateParticle(firePoint, "BulletExplosion");
        }
    }
}