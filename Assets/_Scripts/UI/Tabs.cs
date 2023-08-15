using UnityEngine;
using UnityEngine.UI;

public class Tabs : MonoBehaviour {

    public Button[] buttons;
    public GameObject[] tabs;
    public GameObject active;

    private void Awake() {
        for (int i = 0; i < buttons.Length; i++) {
            int j = i;
            buttons[i].onClick.AddListener(() => {
                active.SetActive(false);

                active = tabs[j];
                active.SetActive(true);

                AudioManager.instance.PlaySound("Select");
            });
        }
    }
}