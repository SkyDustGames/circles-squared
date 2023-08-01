using UnityEngine;
using DG.Tweening;

public class PauseMenu : MonoBehaviour {

    public static bool paused;
    CanvasGroup group;

    private void Awake() {
        group = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause() {
        paused = !paused;
        Time.timeScale = paused? 0 : 1;
        group.DOFade(paused? 1 : 0, .5f).SetUpdate(true);
        group.interactable = paused;
        group.blocksRaycasts = paused;
        AudioManager.instance.PlaySound("Select");
    }

    public void Quit() {
        Scenes.Load(name: "MainMenu");
        TogglePause();
    }

    public void Restart() {
        Scenes.Load(index: Scenes.Current.buildIndex);
        TogglePause();
    }
}