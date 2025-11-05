using JetBrains.Annotations;
using System;
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
    public void setName(string name)
    {   curName = name;
        thisNameLabel.text = name;
        gameObject.name = name;
    }

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
            setIsMaxLevel(true);
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
        //ERROR HANDLING
        if(theMax == -1)
        {
            maxPower = theMax;
            return;
        }

        maxPower = theMax;
        setMaxLevel(maxPower / 100);
    }
    [SerializeField] List<Trait> traits;
    public List<Trait> getTraits() { return traits; }
    public Trait getTraitAt(int index)
    {
        if (traits == null)
            return Trait.NULL;
        if (index >= traits.Count)
            return Trait.NULL;
        if (traits[index] != null)
            return traits[index];
        return Trait.NULL;
    }
    public void setTraits(int trait1, int trait2, int trait3)
    {
        Trait theTrait1 = (Trait) trait1;
        Trait theTrait2 = (Trait) trait2;
        Trait theTrait3 = (Trait) trait3;
        traits.Clear();
        if(trait1 == -1)
        {
            bool repeat = true;
            while (repeat)
            {
                int index = UnityEngine.Random.Range(0, (int)Trait.NUM_TRAITS);
                theTrait1 = (Trait)index;
                if (theTrait1 == theTrait2 || theTrait1 == theTrait3)
                    repeat = true;
                else
                    repeat = false;
            }
        }
        if(trait2 == -1)
        {
            bool repeat = true;
            while (repeat)
            {
                int index = UnityEngine.Random.Range(0, (int)Trait.NUM_TRAITS);
                theTrait2 = (Trait)index;
                if (theTrait2 == theTrait1 || theTrait2 == theTrait3)
                    repeat = true;
                else
                    repeat = false;
            }
        }

        if(trait3 == -1)
        {
            bool repeat = true;
            while (repeat)
            {
                int index = UnityEngine.Random.Range(0, (int)Trait.NUM_TRAITS);
                theTrait3 = (Trait)index;
                if (theTrait3 == theTrait1 || theTrait3 == theTrait2)
                    repeat = true;
                else
                    repeat = false;
            }
        }
        traits.Add(theTrait1);
        traits.Add(theTrait2);
        traits.Add(theTrait3);

    }
    public void setTraitAt(int index, int newTrait)
    {
        for(int i = 0; i < traits.Count; i++)
        {
            if(i == index)
            {
                traits[i] = (Trait)newTrait;
            }
        }
    }
    public void setTraitAt(int index, Trait newTrait)
    {
        for (int i = 0; i < traits.Count; i++)
        {
            if (i == index)
            {
                traits[i] = (Trait)newTrait;
            }
        }
    }
    bool isMaxLevel = false;
    public bool getIsMaxLevel() { return isMaxLevel; }
    public void setIsMaxLevel(bool value) { isMaxLevel = value; }
    [SerializeField] Pattern pattern;
    public Pattern getPattern() { return pattern; }
    public void setPattern(PatternItem item)
    {
        pattern = item.getPattern();

        List<SpriteAsset> assets = item.getAssets();
        foreach (SpriteAsset sa in assets) //Sets all the sprite renderer sprites to the pattern set
        {
            foreach(RendererAsset ra in renderers)
            {
                //Debug.Log("Renderer: " + ra.getID() + " Sprite: " + sa.getID());
                if(ra.getID() == sa.getID())
                {
                    //Debug.Log("Setting sprite ");
                    ra.getSpriteRenderer().sprite = sa.getSprite();
                }
            }
        }
    }

    [Header("Refs")]
    [SerializeField] Animator thisAnimator;
    [SerializeField] SpriteRenderer thisSprite;
    [SerializeField] SpriteRenderer thisSelectionSprite;
    [SerializeField] List<RendererAsset> renderers;
    public List<RendererAsset> getSpriteRenderers() { return renderers; }
    public SpriteRenderer getSpriteRenderer(string name)
    {
        foreach (RendererAsset ra in renderers)
        {
            if (ra.getID() == name)
                return ra.getSpriteRenderer();
        }
        return null;
    }
    public void setSpriteRenderer(string ID, Sprite theSprite)
    {
        foreach(RendererAsset ra in renderers)
        { 
            if(ra.getID() == ID)
            {
                ra.getSpriteRenderer().sprite = theSprite;
            }
        }

    }
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

    public void InitCow(string theName, int theGen, int theMaxPower, int trait1, int trait2, int trait3)
    {
        //Debug.Log("initting cow");
        setName(theName);
        //Debug.Log("this name is " + theName);
        //Debug.Log("it is set to " + curName);
        thisNameLabel.text = theName;
        setGen(theGen);
        setMaxPower(theMaxPower);
        setLevel(0);

        setTraits(trait1, trait2, trait3);

        DontDestroyOnLoad(this.gameObject);

    }

    public void InitCow(string theName, int theGen, int theMaxPower, int trait1, int trait2, int trait3, PatternItem item)
    {
        //Debug.Log("initting cow");
        setName(theName);
        //Debug.Log("this name is " + theName);
        //Debug.Log("it is set to " + curName);
        thisNameLabel.text = theName;
        setGen(theGen);
        setMaxPower(theMaxPower);
        setLevel(0);

        setTraits(trait1, trait2, trait3);

        setPattern(item);

        DontDestroyOnLoad(this.gameObject);

    }

    public void InitCow(string name, int theGen, int level, int maxLevel, int power, int theMaxPower, int trait1, int trait2, int trait3)
    {
        //Debug.Log("initting cow");
        setName(name);
        thisNameLabel.text = name;
        setGen(theGen);
        setMaxPower(theMaxPower);
        setPower(power);
        setLevel(level);
        setMaxLevel(maxLevel);

        setTraits(trait1, trait2, trait3);

        DontDestroyOnLoad(this.gameObject);
    }

    public void InitCow(string name, int theGen, int level, int maxLevel, int power, int theMaxPower, int trait1, int trait2, int trait3, PatternItem pattern)
    {
        //Debug.Log("initting cow");
        setName(name);
        thisNameLabel.text = name;
        setGen(theGen);
        setMaxPower(theMaxPower);
        setPower(power);
        setLevel(level);
        setMaxLevel(maxLevel);

        setTraits(trait1, trait2, trait3);

        setPattern(pattern);

        DontDestroyOnLoad(this.gameObject);
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
                float animStart = UnityEngine.Random.Range(0, 0.05f);
                Debug.Log(animStart);
                thisAnimator.Play("CowIdle", 0, animStart);
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
