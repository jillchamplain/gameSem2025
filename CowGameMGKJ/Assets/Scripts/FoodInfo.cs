using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/FoodDataSO", order = 3)]
public class FoodInfo : ScriptableObject
{
    [SerializeField] string name;
    public string getName() { return name; }
    [SerializeField] int power;
    public int getPower() { return power; }
    [SerializeField] Sprite sprite;
    public Sprite getSprite;
}
