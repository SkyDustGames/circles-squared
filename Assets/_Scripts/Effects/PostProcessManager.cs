using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour {
    
    private PostProcessLayer layer;
    private PostProcessVolume volume;

    private void Start() {
        layer = GetComponent<PostProcessLayer>();
        volume = GetComponent<PostProcessVolume>();

        layer.enabled = Settings.postProcessing;
        volume.enabled = Settings.postProcessing;

        Settings.Change.AddListener(() => {
            layer.enabled = Settings.postProcessing;
            volume.enabled = Settings.postProcessing;
        });
    }
}