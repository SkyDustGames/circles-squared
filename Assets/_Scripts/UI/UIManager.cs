using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UIManager : MonoBehaviour {
    
    public int deactivatedX = -600, activatedX = 0;
    public Transform active;

    public void Activate(Transform t) {
        StartCoroutine(IActivate(t));
        AudioManager.instance.PlaySound("Select");
    }

    private IEnumerator IActivate(Transform t) {
        if (active != null) {
            active.DOLocalMoveX(deactivatedX, .5f).SetUpdate(PauseMenu.paused);
            yield return new WaitForSecondsRealtime(.5f);
            active.gameObject.SetActive(false);
        }

        active = t;
        t.gameObject.SetActive(true);
        t.DOLocalMoveX(activatedX, .5f).SetUpdate(PauseMenu.paused);
    }
}