using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] int maxLevel; //NEED TO FIGURE OUT MATH FOR THIS
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
    [SerializeField] Animator thisAnimator;
    [SerializeField] SpriteRenderer thisSprite;
    [SerializeField] SpriteRenderer thisSelectionSprite;
    public void setSelection(bool isSelected)
    {
        if(isSelected)
        {
            thisSelectionSprite.color = new Color(thisSelectionSprite.color.r, thisSelectionSprite.color.g, thisSelectionSprite.color.b, 1.0f);
        }
        else
        {
                thisSelectionSprite.color = new Color(thisSelectionSprite.color.r, thisSelectionSprite.color.g, thisSelectionSprite.color.b, 0.0f);
        }
    }
    [SerializeField] TextMeshProUGUI thisNameLabel;

    //EVENTS
    public delegate void CowEat(Cow thisCow, Food thisFood);
    public static event CowEat cowEat;

    public delegate void CowMaxLevel(Cow thisCow);
    public static event CowMaxLevel cowMaxLevel;


    public void InitCow(string name, int theGen, int theMaxPower)
    {
        setName(name);
        thisNameLabel.text = name;
        setGen(theGen);
        setMaxPower(theMaxPower);
    }

    public void InitCow()
    {
        setName("NULLCOW");
        thisNameLabel.text = name;
        setGen(-1);
        setMaxPower(-1);
    }

    public enum CowAnims
    {
        IDLE = 0,
        SPAWN = 1,
        FEED = 2,
        RETIRE = 3,
        NUM_ANIMS
    }
    public void PlayAnimation(CowAnims theAnimation)
    {
        switch(theAnimation)
        {
            case CowAnims.IDLE:
                IdleAnimation();
                break;
            case CowAnims.SPAWN:
                SpawnAnimation();
                break;
            case CowAnims.FEED:
                FeedAnimation();
                break;
            case CowAnims.RETIRE:
                RetireAnimation();
                break;
        }
    }

    void IdleAnimation()
     {
        thisAnimator.Play("CowIdle");
     }

    void SpawnAnimation()
    {
        thisAnimator.Play("CowSpawn");
    }

    void FeedAnimation()
    {
        thisAnimator.Play("CowFeed");
        
    }

    void RetireAnimation()
    {
        thisAnimator.Play("CowRetire");
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
