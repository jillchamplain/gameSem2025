using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAsset", menuName = "ScriptableObjects/SpriteAssetSO", order = 4)]
public class SpriteAsset : ScriptableObject
{
    [SerializeField] string ID;
    public string getID() { return ID; }
    [SerializeField] Sprite sprite;
    public Sprite getSprite() { return sprite; }
}
