using UnityEngine;
using DG.Tweening;

public class EndCheckpoint : MonoBehaviour {

    public string nextStage;
    public bool isScene;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            CanvasGroup group = GameObject.Find("YouWin").GetComponent<CanvasGroup>();
            group.DOFade(1f, 1f);
            group.interactable = true;
            group.blocksRaycasts = true;

            WinLoseScreen wls = FindObjectOfType<WinLoseScreen>();
            wls.isScene = isScene;
            wls.nextStage = nextStage;

            other.gameObject.SetActive(false);
        }
    }
}