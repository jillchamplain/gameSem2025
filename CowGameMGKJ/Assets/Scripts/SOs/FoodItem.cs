using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/FoodDataSO", order = 3)]
public class FoodItem : ShopItem
{
    [SerializeField] int power;
    public int getPower() { return power; }

}
