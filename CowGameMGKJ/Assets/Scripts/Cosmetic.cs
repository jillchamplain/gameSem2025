using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmetic : MonoBehaviour
{
    [SerializeField] string name;
    public string getName() { return name; }
    public void setName(string newName) { name = newName; }

    [SerializeField] EquipCosmetic type;
    public EquipCosmetic getType() { return type; }
    public void setType(EquipCosmetic newType) { type = newType; }

    [SerializeField] Trait traitType;
    public Trait getTraitType() { return traitType; }
    public void setTraitType(Trait newTrait) { traitType = newTrait; }

    [SerializeField] SpriteRenderer spriteRenderer;
    public SpriteRenderer getSpriteRenderer() { return spriteRenderer; }
    public void setSprite(Sprite theSprite) { spriteRenderer.sprite = theSprite; }

 
}
