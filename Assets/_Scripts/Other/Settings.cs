using UnityEngine;
using UnityEngine.Events;

public static class Settings {
    
    public static bool postProcessing, fullscreen, cameraShake;
    public static float sfxVolume, musicVolume;
    public static UnityEvent Change = new UnityEvent();

    [RuntimeInitializeOnLoadMethod]
    public static void Load() { 
        sfxVolume = PlayerPrefs.GetFloat("SoundEffectVolume", 1);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        postProcessing = PlayerPrefs.GetInt("PostProcessing", 1) == 1;
        cameraShake = PlayerPrefs.GetInt("CameraShake", 1) == 1;
        fullscreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
    }

    public static void Save() {
        PlayerPrefs.SetFloat("SoundEffectVolume", sfxVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetInt("PostProcessing", postProcessing ? 1 : 0);
        PlayerPrefs.SetInt("CameraShake", cameraShake ? 1 : 0);
        PlayerPrefs.SetInt("FullScreen", fullscreen ? 1 : 0);
        
        Change.Invoke();
    }
}