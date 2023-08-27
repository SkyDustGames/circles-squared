using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class StageSelector : LevelSelectMenu {
    
    public TextAsset[] assets;

    public override Task<TextAsset[]> GetTextAssets() {
        containsName = false;

        return Task.FromResult(assets.Where(
            (asset) => Saves.loaded.unlockedStages.Contains(asset.name)
        ).ToArray());
    }
}