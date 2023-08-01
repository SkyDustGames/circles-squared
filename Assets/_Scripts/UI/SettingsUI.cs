using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {

    public Toggle postProcessing, fullscreen, cameraShake;
    public Slider sfx, music;

    private void Start() {
        postProcessing.isOn = Settings.postProcessing;
        fullscreen.isOn = Settings.fullscreen;
        cameraShake.isOn = Settings.cameraShake;
        sfx.value = Settings.sfxVolume;
        music.value = Settings.musicVolume;
    }

    public void SetPostProcessing(bool v) {
        Settings.postProcessing = v;
        AudioManager.instance.PlaySound("Select");
    }

    public void SetFullscreen(bool v) {
        Settings.fullscreen = v;
        AudioManager.instance.PlaySound("Select");
    }

    public void SetCameraShake(bool v) {
        Settings.cameraShake = v;
        AudioManager.instance.PlaySound("Select");
    }

    public void SetSoundEffects(float v) {
        Settings.sfxVolume = v;
    }

    public void SetMusic(float v) {
        Settings.musicVolume = v;
    }

    public void Apply() {
        Settings.Save();
    }
}