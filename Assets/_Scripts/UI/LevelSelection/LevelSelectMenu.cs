using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelSelectMenu : MonoBehaviour {
    
    public GameObject template;
    protected bool containsName = true;

    private void Awake() {
        AsyncAwake();
    }

    private async void AsyncAwake() {
        TextAsset[] textAssets = await GetTextAssets();
        Dictionary<string, Sprite> images = containsName? await GetSprites(): null;

        int i = 1;
        foreach (TextAsset asset in textAssets) {
            GameObject button = Instantiate(template, template.transform.parent);

            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();

            if (!containsName)
                text.text = "" + i;
            else {
                text.text = asset.name;

                Image image = button.transform.GetChild(1).GetComponent<Image>();
                image.sprite = images.GetValueOrDefault(asset.name);
            }

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

    protected Sprite LoadNewSprite(string filePath) {
        Texture2D texture = LoadTexture(filePath);
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            Vector2.zero,
            texture.height
        );
    
        return sprite;
    }

    private Texture2D LoadTexture(string FilePath) {
        if (File.Exists(FilePath)) {
            byte[] data = File.ReadAllBytes(FilePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(data))
                return texture;
        }  
        return null;
   }

    public virtual Task<TextAsset[]> GetTextAssets() {
        throw new System.NotImplementedException();
    }

    public virtual Task<Dictionary<string, Sprite>> GetSprites() {
        throw new System.NotImplementedException();
    }
}