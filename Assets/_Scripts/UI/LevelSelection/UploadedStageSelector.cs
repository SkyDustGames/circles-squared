using System.IO;
using System.Net;
using UnityEngine;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Collections.Generic;

// Same as FileStageSelector, but downloads files instead of searching a directory that was already created.
#pragma warning disable 38, IDE0059
public class UploadedStageSelector : FileStageSelector {

    private static readonly string url = "https://api.skydust.dev/c2/"; // temporary URL

    public override async Task<TextAsset[]> GetTextAssets() {
        return (TextAsset[]) await ExtractAndLoad("xml");
    }

    public override async Task<Dictionary<string, Sprite>> GetSprites() {
        return (Dictionary<string, Sprite>) await ExtractAndLoad("img");
    }

    private async Task<object> ExtractAndLoad(string name) {
        string path = Path.Join(Application.persistentDataPath, $"{name}.zip");
        using (WebClient wc = new()) {
            await wc.DownloadFileTaskAsync(new System.Uri(url + $"get/{name}"),
                path);
        }

        ZipFile.ExtractToDirectory(path, Path.Join(Application.persistentDataPath, name));
        path = name;

        if (name == "img") return await base.GetSprites();
        else return await base.GetTextAssets();
    }
}