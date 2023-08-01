using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ButtonController : MonoBehaviour {

    public GameObject target;
    public SpriteRenderer graphics;
    public bool toggleButton;
    public bool enable;
    bool activated;

    private void OnTriggerEnter2D(Collider2D other) {
        graphics.transform.DOLocalMoveY(-.3f, .3f);
        AudioManager.instance.PlaySound("ButtonTrigger");

        CameraFollow.Shake();
        if (toggleButton) {
            activated = !activated;
            if (activated)
                graphics.DOColor(new Color(0f, graphics.color.g, graphics.color.b, graphics.color.a), .5f);
            else
                graphics.DOColor(new Color(1f, graphics.color.g, graphics.color.b, graphics.color.a), .5f);
            target.SetActive(enable? activated : !activated);

            return;
        }

        activated = true;
        target.SetActive(enable);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!toggleButton) {
            activated = false;
            target.SetActive(!enable);
            CameraFollow.Shake();
        }

        graphics.transform.DOLocalMoveY(-.2f, .3f);
    }
}