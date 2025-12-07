using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string name;
    public string getName() { return name; }
    public void setName(string theName) { name = theName; }
    [SerializeField] int power;
    public int getPower() { return power; }
    public void setPower(int thePower) { power = thePower; }
   
    [Header("Refs")]
    [SerializeField] SpriteRenderer thisSprite;
    public void setSprite(Sprite theSprite) { thisSprite.sprite = theSprite; }

     bool hasBeenEaten = false;
    public bool getEaten() { return hasBeenEaten; }
    public void setEaten(bool newEat) { hasBeenEaten = newEat; }
}
