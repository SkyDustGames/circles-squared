using UnityEngine;

public class WinLoseScreen : MonoBehaviour {

    [HideInInspector] public string nextStage;
    [HideInInspector] public bool isScene;
    
    public void Next() {
        if (isScene) {
            Scenes.Load(name: nextStage);
            return;
        }

        StageData data = StageParser.ParseData(Resources.Load(nextStage) as TextAsset);
        LevelManager.stage = data;

        Scenes.Load(index: Scenes.Current.buildIndex);
    }

    public void Restart() {
        Scenes.Load(index: Scenes.Current.buildIndex);
    }

    public void Quit() {
        Scenes.Load(name: "MainMenu");
    }
}