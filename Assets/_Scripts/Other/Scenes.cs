using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {
    
    Animator animator;
    public static Scenes instance;

    public static Scene Current { get => SceneManager.GetActiveScene(); }

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        transform.SetParent(null, false);
        instance = this;
        DontDestroyOnLoad(instance);

        Destroy(GetComponent<CanvasGroup>());
        animator = GetComponent<Animator>();
    }

    public static void Load(int index = -1, string name = null) {
        instance.StartCoroutine(instance.ILoad(index, name));
    }

    public static void TriggerAnimation() {
        instance.animator.SetTrigger("Close");
    }

    private IEnumerator ILoad(int index, string name) {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1f);

        if (name == null) SceneManager.LoadScene(index);
        else SceneManager.LoadScene(name);

        yield return null;

        Settings.Change.Invoke();
        animator.SetTrigger("Close");
    }
}