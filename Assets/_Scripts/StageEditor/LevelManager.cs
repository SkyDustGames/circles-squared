using UnityEngine;
using static StageData;

public class LevelManager : MonoBehaviour {
    
    public Transform environment;
    public static StageData stage;

    private void Start() {
        if (stage == null) stage = StageParser.ParseData(Resources.Load("Stage1") as TextAsset);

        foreach (StageObject stageObject in stage.objects) {
            GameObject gameObject = Instantiate(stageObject.assign, stageObject.position, Quaternion.Euler(0, 0, stageObject.rotation), environment);
            gameObject.transform.localScale = stageObject.size;

            object name;
            stageObject.properties.TryGetValue("name", out name);

            if (name != null) gameObject.name = (string)name;

            if (gameObject.CompareTag("Player")) {
                Player player = gameObject.GetComponent<Player>();
                player.follow = FindObjectOfType<CameraFollow>();
                player.follow.target = player.transform;
            }
            
            if (gameObject.CompareTag("Turret")) {
                Turret turret = gameObject.GetComponent<Turret>();
                
                object prop;
                stageObject.properties.TryGetValue("bulletSpeed", out prop);
                if (prop != null)
                    turret.bulletSpeed = (float)prop;

                stageObject.properties.TryGetValue("time", out prop);
                if (prop != null)
                    turret.time = (float)prop;
            }

            if (gameObject.CompareTag("Enemy")) {
                Enemy enemy = gameObject.GetComponent<Enemy>();

                object prop;
                stageObject.properties.TryGetValue("awareness", out prop);
                if (prop != null)
                    enemy.radius = (float)prop;

                stageObject.properties.TryGetValue("moveTo", out prop);
                if (prop != null)
                    enemy.other = (Vector3)prop;
            }

            if (gameObject.CompareTag("Button")) {
                ButtonController button = gameObject.GetComponent<ButtonController>();

                stageObject.properties.TryGetValue("target", out name);
                if (name != null)
                    button.target = GameObject.Find((string)name);

                object prop;
                stageObject.properties.TryGetValue("toggle", out prop);
                if (prop != null && (bool)prop)
                    button.toggleButton = true;

                stageObject.properties.TryGetValue("enable", out prop);
                if (prop != null && (bool)prop)
                    button.enable = true;

                if (button.target != null && button.enable) button.target.SetActive(false);
            }
        }

        if (stage.index >= 0) {
            EndCheckpoint end = FindObjectOfType<EndCheckpoint>();
            end.isScene = false;
            end.nextStage = "Stage" + (stage.index + 1);
        }
    }
}