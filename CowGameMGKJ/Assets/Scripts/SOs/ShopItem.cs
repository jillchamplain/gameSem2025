using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "ScriptableObjects/ShopItemSO", order = 2)]
public class ShopItem : ScriptableObject
{
    [SerializeField] string name;
    public string getItemName() { return name; }
    [SerializeField] ShopType type;
    public ShopType getType() { return type; }
    [SerializeField] Sprite sprite;
    public Sprite getSprite() { return sprite; }
    [SerializeField] int cost;
    public int getCost() { return cost; }
}
