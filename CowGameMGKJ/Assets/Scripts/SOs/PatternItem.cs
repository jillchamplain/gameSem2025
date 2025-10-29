using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "ScriptableObjects/PatternDataSO", order = 3)]

public class PatternItem : ShopItem
{
    [SerializeField] Pattern patternType;
    public Pattern getPattern() { return patternType; }
    [SerializeField] List<SpriteAsset> assets;
    public List<SpriteAsset> getAssets() {  return assets;}
    public SpriteAsset getAssetAt(int index)
    {
        for(int i = 0; i < assets.Count; i++)
        {
            if (i == index)
                return assets[i];
        }
        return null;
    }
}
