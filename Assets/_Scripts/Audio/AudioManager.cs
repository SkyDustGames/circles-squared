using UnityEngine;

public class AudioManager : MonoBehaviour {

    [System.Serializable]
    public class Sound {

        public string name;
        public AudioClip clip;
        public float volume;
        [HideInInspector] public AudioSource source;
    }

    public Sound[] sounds;
    public static AudioManager instance;

    public void Start() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null, false);
        instance = this;
        DontDestroyOnLoad(instance);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
        }
    }

    public void PlaySound(string name) {
        foreach (Sound sound in sounds) {
            if (sound.name == name) {
                sound.source.pitch = Random.Range(1f, 2f);
                sound.source.volume = sound.volume * Settings.sfxVolume;
                sound.source.Play();
                break;
            }
        }
    }
}