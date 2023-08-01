using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour {
    
    public TextAsset[] assets;
    public GameObject template;

    private void Awake() {
        int i = 1;
        foreach (TextAsset asset in assets) {
            GameObject button = Instantiate(template, template.transform.parent);

            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "" + i;

            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(() => {
                LevelManager.stage = StageParser.ParseData(asset);
                Scenes.Load(name: "Stage");
            });

            button.SetActive(true);
            i++;
        }

        Destroy(template);
    }
}