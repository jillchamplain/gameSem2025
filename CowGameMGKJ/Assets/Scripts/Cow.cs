using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string name;
    [SerializeField] int generation;

    [SerializeField] int currentLevel;
    [SerializeField] int maxLevel;

    [SerializeField] int currentPower;
    public int getCurrentPower() { return currentPower; }
    public void setCurrentPower(int newPower)
    {
        currentPower = newPower;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
            cowMaxLevel?.Invoke(this);
        }
            
    }
    [SerializeField] int maxPower;

    [Header("Refs")]
    [SerializeField] SpriteRenderer thisSprite;

    //EVENTS
    public delegate void CowEat(Cow thisCow, Food thisFood);
    public static event CowEat cowEat;

    public delegate void CowMaxLevel(Cow thisCow);
    public static event CowMaxLevel cowMaxLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Food"))
        {
            cowEat?.Invoke(this, collision.GetComponent<Food>());
        }
    }
}
