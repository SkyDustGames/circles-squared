using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [System.Serializable]
    public class SongGroup {
        
        public string name;
        public AudioClip[] clips;
    }

    public float fadeDuration;
    public SongGroup[] groups;
    AudioSource source;
    SongGroup group;
    bool fade;

    public static MusicManager instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null, false);
        instance = this;
        DontDestroyOnLoad(instance);

        source = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += (scene, mode) => {
            SongGroup previous = group;
            foreach (SongGroup group in groups) {
                if (scene.name.Contains(group.name)) {
                    this.group = group;
                    break;
                }
            }

            if (group != null && group != previous)
                StartCoroutine(ChangeSong());
        };
    }

    private void Start() {
        source.volume = Settings.musicVolume;
        Settings.Change.AddListener(() => {
            source.volume = Settings.musicVolume;
        });
    }

    private void Update() {
        if (source.clip != null && source.time >= source.clip.length - 2f && !fade)
            StartCoroutine(ChangeSong());
    }

    private AudioClip GetRandomSong() {
        return group.clips[Random.Range(0, group.clips.Length)];
    }

    private IEnumerator Fade(bool fadeIn) {
        float currentTime = 0;
        float start = source.volume;

        while (currentTime < fadeDuration){
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, fadeIn? Settings.musicVolume : 0f, currentTime / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator ChangeSong() {
        fade = true;

        yield return Fade(false);
        source.Stop();
        source.clip = GetRandomSong();
        source.Play();
        yield return Fade(true);

        fade = false;
    }
}