using System.Threading.Tasks;
using UnityEngine;

public class StageSelector : LevelSelectMenu {
    
    public TextAsset[] assets;

    public override Task<TextAsset[]> GetTextAssets() {
        containsName = false;
        return Task.FromResult(assets);
    }
}