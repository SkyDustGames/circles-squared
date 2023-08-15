using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class FileStageSelector : LevelSelectMenu {

    public string path;

    public override Task<TextAsset[]> GetTextAssets() {
        List<TextAsset> assets = new List<TextAsset>();
        foreach (string file in Directory.EnumerateFiles(Path.Join(Application.persistentDataPath, path))) {
            if (!Path.GetFileName(file).EndsWith("xml")) continue;

            TextAsset asset = new TextAsset(File.ReadAllText(file));
            asset.name = Path.GetFileNameWithoutExtension(file);
            assets.Add(asset);
        }
        return Task.FromResult(assets.ToArray());
    }

    public override Task<Dictionary<string, Sprite>> GetSprites() {
        Dictionary<string, Sprite> sprites = new();
        foreach (string file in Directory.EnumerateFiles(Path.Join(Application.persistentDataPath, path))) {
            if (Path.GetFileName(file).EndsWith("xml")) continue;
            sprites.Add(Path.GetFileNameWithoutExtension(file), LoadNewSprite(file));
        }
        return Task.FromResult(sprites);
    }
}