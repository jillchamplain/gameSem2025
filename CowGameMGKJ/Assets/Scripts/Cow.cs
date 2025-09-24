using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public void setLevel(int level) { curLevel = level; }

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
            //Debug.Log("current power: " + curPower + " max: " + maxPower);
            curPower = maxPower;
            //Debug.Log("setting to: " + curPower);
        }

        if (Mathf.Floor(curPower / 100) >= curLevel)
        {
            setLevel((int)Mathf.Floor(curPower / 100));
            if (getLevel() >= maxLevel)
                setLevel(getMaxLevel());
        }

        //load events properly
        if(curPower >= maxPower)
        {
            cowMaxLevel?.Invoke(this);
        }
        else if(Mathf.Floor(curPower / 100) >= curLevel)
        {
            cowLevelUp?.Invoke(this);
        }
        
            
    }
    [SerializeField] int maxPower;

    public int getMaxPower() { return maxPower; }
    public void setMaxPower(int theMax)
    {
        maxPower = theMax;
        setMaxLevel(maxPower / 100);
    }

    [SerializeField] List<string> traits;
    public string getTraitAt(int index) { return traits[index]; }

    [SerializeField] int uiIndex;
    public int getUIIndex() { return uiIndex; } //MOVE THIS SHIT
    public void setUIIndex(int newIndex) { uiIndex = newIndex; } //BAD

    bool isInitted = false;

    [Header("Refs")]
    [SerializeField] Animator thisAnimator;
    [SerializeField] SpriteRenderer thisSprite;
    [SerializeField] SpriteRenderer thisSelectionSprite;
    public void setSelected(bool isSelected)
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

    public delegate void CowRetire(Cow thisCow);
    public static event CowRetire cowRetire;

    public delegate void CowMaxLevel(Cow thisCow);
    public static event CowMaxLevel cowMaxLevel;

    public delegate void CowLevelUp(Cow thisCow);
    public static event CowLevelUp cowLevelUp;

    public void InitCow(Cow theCow)
    {
        setName(theCow.name);
        thisNameLabel.text = name;
        setGen(theCow.gen);
        setMaxPower(theCow.maxPower);
        setLevel(theCow.curLevel);
    }
    public void InitCow(string name, int theGen, int theMaxPower, string trait1, string trait2, string trait3)
    {
        setName(name);
        thisNameLabel.text = name;
        setGen(theGen);
        setMaxPower(theMaxPower);
        setLevel(1);

        string firstTrait = trait1;
        string secondTrait = trait2;
        string thirdTrait = trait3;
        traits.Add(firstTrait);
        traits.Add(secondTrait);
        traits.Add(thirdTrait);

        DontDestroyOnLoad(this.gameObject);

        isInitted = true;
    }

    public void InitCow(string name, int theGen, int power, int theMaxPower, string trait1, string trait2, string trait3)
    {
        setName(name);
        thisNameLabel.text = name;
        setGen(theGen);
        setPower(power);
        setMaxPower(theMaxPower);
        setLevel(1);

        string firstTrait = trait1;
        string secondTrait = trait2;
        string thirdTrait = trait3;
        traits.Add(firstTrait);
        traits.Add(secondTrait);
        traits.Add(thirdTrait);

        DontDestroyOnLoad(this.gameObject);

        isInitted = true;
    }

    public void InitCow()
    {
        setName("NULLCOW");
        thisNameLabel.text = name;
        setGen(-1);
        setMaxPower(-1);
        setLevel(1);

        DontDestroyOnLoad(this.gameObject);
        isInitted = true;
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
                thisAnimator.Play("CowIdle");
                break;
            case CowAnims.SPAWN:
                thisAnimator.Play("CowSpawn");
                break;
            case CowAnims.FEED:
                thisAnimator.Play("CowFeed");
                break;
            case CowAnims.RETIRE:
                thisAnimator.Play("CowRetire");
                //Debug.Log("playing retire");
                break;
        }
    }

  
    void Retire()
    {
        cowRetire?.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Food")) 
        {
            //Debug.Log("this gameObject is at " + gameObject.transform.position + " and food is at " + collision.gameObject.transform.position);
            cowEat?.Invoke(this, collision.GetComponent<Food>());
            //Debug.Log("cow is" + this.gameObject);
        }
    }


}
