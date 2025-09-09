using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string curName;
    public string getName() { return curName; }
    public void setName(string name) { curName = name; }

    [SerializeField] int gen;
    public int getGen() { return gen; }
    public void setGen(int newGen) { gen = newGen; }

    [SerializeField] int curLevel;
    public int getLevel() { return curLevel; }

    [SerializeField] int maxLevel;
    public int getMaxLevel() { return maxLevel; }
    public void setMaxLevel(int theMax) { maxLevel = theMax; }

    [SerializeField] int curPower;
    public int getPower() { return curPower; }
    public void setPower(int newPower)
    {
        curPower = newPower;
        if (curPower >= maxPower)
        {
            curPower = maxPower;
            cowMaxLevel?.Invoke(this);
        }
            
    }
    [SerializeField] int maxPower;

    public int getMaxPower() { return maxPower; }
    public void setMaxPower(int theMax)
    {
        maxPower = theMax;
        setMaxLevel(maxPower % 100);
    }

    [SerializeField] int uiIndex;
    public int getUIIndex() { return uiIndex; }
    public void setUIIndex(int newIndex) { uiIndex = newIndex; }

    [Header("Refs")]
    [SerializeField] SpriteRenderer thisSprite;

    //EVENTS
    public delegate void CowEat(Cow thisCow, Food thisFood);
    public static event CowEat cowEat;

    public delegate void CowMaxLevel(Cow thisCow);
    public static event CowMaxLevel cowMaxLevel;


    public void InitCow(string name, int theGen, int theMaxPower)
    {
        setName(name);
        setGen(theGen);
        setMaxPower(theMaxPower);
    }

    public void InitCow()
    {
        setName("NULLCOW");
        setGen(-1);
        setMaxPower(-1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Food"))
        {
            cowEat?.Invoke(this, collision.GetComponent<Food>());
            //Debug.Log("cow is" + this.gameObject);
        }
    }
}
