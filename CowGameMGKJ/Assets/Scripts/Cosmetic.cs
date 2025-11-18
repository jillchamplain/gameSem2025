using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipCosmetic
{
    NULL,
    HAT,
    TOP,
    BOT,
    NUM_COSMETICS,
}
public class Cosmetic : MonoBehaviour
{
    [SerializeField] string name;
    public string getName() { return name; }

    [SerializeField] EquipCosmetic type;
    public EquipCosmetic getType() { return type; }

    [SerializeField] Trait traitType;
    public Trait getTraitType() { return traitType; }

    [SerializeField] SpriteRenderer spriteRenderer;
    public SpriteRenderer getSpriteRenderer() { return spriteRenderer; }

 
}
