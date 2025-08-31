using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string name;
    public string getName() { return name; }
    [SerializeField] int generation;
    public int getGen() { return generation; }

    [SerializeField] int currentLevel;
    public int getLevel() { return currentLevel; }
    [SerializeField] int maxLevel;
    public int getMaxLevel() { return maxLevel; }

    [SerializeField] int currentPower;
    public int getPower() { return currentPower; }
    public void setPower(int newPower)
    {
        currentPower = newPower;
        if (currentPower > maxPower)
        {
            currentPower = maxPower;
            cowMaxLevel?.Invoke(this);
        }
            
    }
    [SerializeField] int maxPower;

    [SerializeField] int uiIndex;
    public int getUIIndex() { return uiIndex; }

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
            //Debug.Log("cow is" + this.gameObject);
        }
    }

    public void IncreasePower(int increase)
    {
        currentPower += increase;
        if (currentPower > maxPower)
            currentPower = maxPower;


    }
}
