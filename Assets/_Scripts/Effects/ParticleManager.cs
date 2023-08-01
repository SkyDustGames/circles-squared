using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {
    
    public ParticleSystem[] particlePrefabs;
    Dictionary<string, ParticleSystem> particles;
    public static ParticleManager instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null, false);
        instance = this;
        DontDestroyOnLoad(instance);

        particles = new Dictionary<string, ParticleSystem>();
        foreach (ParticleSystem particle in particlePrefabs)
            particles.Add(particle.gameObject.name, particle);
    }

    public void CreateParticle(Transform t, string name, Quaternion rotation) {
        ParticleSystem s = Instantiate(particles.GetValueOrDefault(name), t.position, rotation);
        ParticleSystem.MainModule main = s.main;
        Destroy(s.gameObject, main.startLifetime.constantMax);
    }

    public void CreateParticle(Transform t, string name) {
        ParticleSystem s = Instantiate(particles.GetValueOrDefault(name), t.position, t.rotation);
        ParticleSystem.MainModule main = s.main;
        Destroy(s.gameObject, main.startLifetime.constantMax);
    }
}