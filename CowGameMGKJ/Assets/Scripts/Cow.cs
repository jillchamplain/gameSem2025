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
    public void setTraitRandom(int trait)
    {
        int index = UnityEngine.Random.Range(0, 3);
        bool canSet = false;
        while(!canSet)
        {
            index = UnityEngine.Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    if (getTraitAt(0) == (Trait)trait)
                    {
                        canSet = false;
                    }
                    else
                        canSet = true;
                        break;
                case 1:
                    if (getTraitAt(1) == (Trait)trait)
                    {
                        canSet = false;
                    }
                    else
                        canSet = true;
                        break;
                case 2:
                    if (getTraitAt(2) == (Trait)trait)
                    {
                        canSet = false;
                    }
                    else
                        canSet = true;
                        break;
            }
            if (getTraitAt(0) == getTraitAt(1) && getTraitAt(0) == getTraitAt(2))
                canSet = true;
        }
        setTraitAt(index, trait);
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
    [SerializeField] string hat; //get name from save data, get sprite from cosmetic manager
    public string getHat() { return hat; }
    public void setHat(CosmeticItem item)
    {
        getSpriteRenderer("Hat").sprite = item.getSprite();
        hat = item.getItemName();
    }
    [SerializeField] string top;
    public string getTop() { return top; }
    public void setTop(CosmeticItem item)
    {
        getSpriteRenderer("Top").sprite = item.getSprite();
        top = item.getItemName();
    }
    [SerializeField] string bot;
    public string getBot() { return bot; }
    public void setBot(CosmeticItem item)
    {
        getSpriteRenderer("Bot").sprite = item.getSprite();
        bot = item.getItemName();
    }

    //EVENTS
    public delegate void CowEat(Cow thisCow, Food thisFood);
    public static event CowEat cowEat;

    public delegate void CowRetire(Cow thisCow);
    public static event CowRetire cowRetire;

    public delegate void CowMaxLevel(Cow thisCow);
    public static event CowMaxLevel cowMaxLevel;

    public delegate void CowLevelUp(Cow thisCow);
    public static event CowLevelUp cowLevelUp;

    public delegate void CowEquip(GameObject thisCow,Cosmetic item);
    public static event CowEquip cowEquip;


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
        hat = "NULL";
        top = "NULL";
        bot = "NULL";

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

    public void InitCow(string name, int theGen, int level, int maxLevel, int power, int theMaxPower, int trait1, int trait2, int trait3, PatternItem pattern, string hat, string top, string bot)
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

        //Debug.Log(pattern);
        setPattern(pattern);

        this.hat = hat;
        this.top = top;
        this.bot = bot;
        //Equip on spawn!

        DontDestroyOnLoad(this.gameObject);
    }

    public enum CowAnims
    {
        NULL = -1,
        IDLE,
        HOLD,
        SPAWN,
        FEED,
        RETIRE,
        NUM_ANIMS
    }
    public void PlayAnimation(CowAnims theAnimation)
    {
        switch(theAnimation)
        {
            case CowAnims.IDLE:
                float animStart = UnityEngine.Random.Range(0, 0.05f);
               // Debug.Log(animStart);
                thisAnimator.Play("CowIdle", 0, animStart);
                break;
            case CowAnims.HOLD:
                float animStart2 = UnityEngine.Random.Range(0, 0.05f);
                if(!thisAnimator.GetCurrentAnimatorStateInfo(0).IsName("CowHoldIdle"))
                    thisAnimator.Play("CowHoldIdle", 0, animStart2);
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

    public void Equip(CosmeticItem item)
    {
        if (!item)
            return;
        switch (item.getCosmeticType())
        {
            case EquipCosmetic.HAT:
                setHat(item);
                break;
            case EquipCosmetic.TOP:
                setTop(item);
                break;
            case EquipCosmetic.BOT:
                setBot(item);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Food")) 
        {
            //Debug.Log("this gameObject is at " + gameObject.transform.position + " and food is at " + collision.gameObject.transform.position);
            cowEat?.Invoke(this, collision.GetComponent<Food>());
            //Debug.Log("cow is" + this.gameObject);
        }
        if(collision.gameObject.CompareTag("Cosmetic"))
        {
            cowEquip?.Invoke(gameObject, collision.gameObject.GetComponent<Cosmetic>());
           
            //Destroy(collision.gameObject);
        }
    }


}
