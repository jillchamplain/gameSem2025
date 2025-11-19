using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticData", menuName = "ScriptableObjects/CosmeticDataO", order = 4)]
public class CosmeticItem : ShopItem
{
    [SerializeField] EquipCosmetic cosmeticType;
    public EquipCosmetic getCosmeticType() { return cosmeticType; }
}
