using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    //SAVE DATA
    public int curGeneration;
    public int numCoins;
    //COW 1
    public string name1;
    public int gen1;
    public int level1;
    public int mLevel1;
    public int power1;
    public int mPower1;
    public string traitA1;
    public string traitB1;
    public string traitC1;
    public string pattern1; //FIGURE OUT PATTERNS
    public bool isMax1;
    public float x1;
    public float y1;
    public float z1;

    //COW 2
    public string name2;
    public int gen2;
    public int level2;
    public int mLevel2;
    public int power2;
    public int mPower2;
    public string traitA2; 
    public string traitB2;
    public string traitC2;
    public string pattern2;
    public bool isMax2;
    public float x2;
    public float y2;
    public float z2;

    //COW 3
    public string name3;
    public int gen3;
    public int level3;
    public int mLevel3;
    public int power3;
    public int mPower3;
    public string traitA3;
    public string traitB3;
    public string traitC3;
    public string pattern3;
    public bool isMax3;
    public float x3;
    public float y3;
    public float z3;

    //UNLOCKED FOODS AGAIn
    public string[] unlockedFoodNames = new string[10];

    //CURRENT RACE AND LEAGUE
    public int curLeagueID;
    public int curRaceID;

    public GameData(int gen, Cow cow1, Cow cow2, Cow cow3, int numFoodsUnlocked, int curRaceIndex, int curLeagueIndex)
    {
        curGeneration = gen;

        if (cow1 != null)
        {
            name1 = cow1.getName();
            gen1 = cow1.getGen();
            level1 = cow1.getLevel();
            mLevel1 = cow1.getMaxLevel();
            power1 = cow1.getPower();
            mPower1 = cow1.getMaxPower();
            traitA1 = cow1.getTraitAt(0);
            traitB1 = cow1.getTraitAt(1);
            traitC1 = cow1.getTraitAt(2);
            isMax1 = cow1.getIsMaxLevel();
            x1 = cow1.gameObject.transform.position.x;
            y1 = cow1.gameObject.transform.position.y;
            z1 = cow1.gameObject.transform.position.z;
        }

        if (cow2 != null)
        {
            name2 = cow2.getName();
            gen2 = cow2.getGen();
            level2 = cow2.getLevel();
            mLevel2 = cow2.getMaxLevel();
            power2 = cow2.getPower();
            mPower2 = cow2.getMaxPower();
            traitA2 = cow2.getTraitAt(0);
            traitB2 = cow2.getTraitAt(1);
            traitC2 = cow2.getTraitAt(2);
            isMax2 = cow2.getIsMaxLevel();
            x2 = cow2.gameObject.transform.position.x;
            y2 = cow2.gameObject.transform.position.y;
            z2 = cow2.gameObject.transform.position.z;
        }

        if (cow3 != null)
        {
            name3 = cow3.getName();
            gen3 = cow3.getGen();
            level3 = cow3.getLevel();
            mLevel3 = cow3.getMaxLevel();
            power3 = cow3.getPower();
            mPower3 = cow3.getMaxPower();
            traitA3 = cow3.getTraitAt(0);
            traitB3 = cow3.getTraitAt(1);
            traitC3 = cow3.getTraitAt(2);
            isMax3 = cow3.getIsMaxLevel();
            x3 = cow3.gameObject.transform.position.x;
            y3 = cow3.gameObject.transform.position.y;
            z3 = cow3.gameObject.transform.position.z;
        }

    

        curRaceID = curRaceIndex;
        curLeagueID = curLeagueIndex;
        
    }

    public GameData(CowManager cowManager, FoodManager foodManager, ShopManager shopManager, RaceManager raceManager)
    {
        curGeneration = cowManager.curGeneration;
        Cow cow1 = cowManager.getCowAt(0);
        Cow cow2 = cowManager.getCowAt(1);
        Cow cow3 = cowManager.getCowAt(2);

        if (cow1 != null)
        {
            name1 = cow1.getName();
            gen1 = cow1.getGen();
            level1 = cow1.getLevel();
            mLevel1 = cow1.getMaxLevel();
            power1 = cow1.getPower();
            mPower1 = cow1.getMaxPower();
            traitA1 = cow1.getTraitAt(0);
            traitB1 = cow1.getTraitAt(1);
            traitC1 = cow1.getTraitAt(2);
            isMax1 = cow1.getIsMaxLevel();
            x1 = cow1.gameObject.transform.position.x;
            y1 = cow1.gameObject.transform.position.y;
            z1 = cow1.gameObject.transform.position.z;
        }

        if (cow2 != null)
        {
            name2 = cow2.getName();
            gen2 = cow2.getGen();
            level2 = cow2.getLevel();
            mLevel2 = cow2.getMaxLevel();
            power2 = cow2.getPower();
            mPower2 = cow2.getMaxPower();
            traitA2 = cow2.getTraitAt(0);
            traitB2 = cow2.getTraitAt(1);
            traitC2 = cow2.getTraitAt(2);
            isMax2 = cow2.getIsMaxLevel();
            x2 = cow2.gameObject.transform.position.x;
            y2 = cow2.gameObject.transform.position.y;
            z2 = cow2.gameObject.transform.position.z;
        }

        if (cow3 != null)
        {
            name3 = cow3.getName();
            gen3 = cow3.getGen();
            level3 = cow3.getLevel();
            mLevel3 = cow3.getMaxLevel();
            power3 = cow3.getPower();
            mPower3 = cow3.getMaxPower();
            traitA3 = cow3.getTraitAt(0);
            traitB3 = cow3.getTraitAt(1);
            traitC3 = cow3.getTraitAt(2);
            isMax3 = cow3.getIsMaxLevel();
            x3 = cow3.gameObject.transform.position.x;
            y3 = cow3.gameObject.transform.position.y;
            z3 = cow3.gameObject.transform.position.z;
        }

        //Reset Food Flags 
        /* for (int i = 0; i < unlockedFoodFlags.Length; i++)
         {
             unlockedFoodFlags[i] = false;
         }

         //Attribute flag unlocks by number of food unlocked
         for (int i = 0; i < foodManager.getUnlockedFoods().Count; i++)
         {
             unlockedFoodFlags[i] = true;
         }*/

       /* for (int i = 0; i < unlockedFoodNames.Length; i++)
        {
            unlockedFoodNames[i] = "NULL";
        }
       */
       

        curRaceID = raceManager.getCurRaceIndex();
        curLeagueID = raceManager.getCurLeagueIndex();

        numCoins = shopManager.getCoins();

    }

    public GameData(FoodManager foodManager, ShopManager shopManager, RaceManager raceManager)
    {

        Debug.Log(foodManager.getUnlockedFoodData().Count);
        for (int i = 0; i < foodManager.getUnlockedFoodData().Count; i++)
        {
            unlockedFoodNames[i] = foodManager.getUnlockedFoodDataAt(i).getItemName();
        }
        for (int i = foodManager.getUnlockedFoodData().Count; i < unlockedFoodNames.Length; i++)
        {
            unlockedFoodNames[i] = "NULL";
        }

        curRaceID = raceManager.getCurRaceIndex();
        curLeagueID = raceManager.getCurLeagueIndex();

        numCoins = shopManager.getCoins();

    }


    public GameData()
    {
        //Debug.Log("Being reset");
        curGeneration = 3;
        numCoins = 0;
        name1 = "LIL COW";
       // Debug.Log(name1);
        gen1 = 1;
        level1 = 0;
        mLevel1 = 1;
        power1 = 0;
        mPower1 = 100;
        traitA1 = "1";
        traitB1 = "2";
        traitC1 = "3";
        isMax1 = false;
        x1 = -1;
        y1 = -1;
        z1 = -1;

        name2 = "COW JR";
        gen2 = 2;
        level2 = 0;
        mLevel2 = 2;
        power2 = 0;
        mPower2 = 200;
        traitA2 = "1";
        traitB2 = "2";
        traitC2 = "3";
        isMax2 = false;
        x2 = -1;
        y2 = -1;
        z2 = -1;

        name3 = "COW III";
        gen3 = 3;
        level3 = 0;
        mLevel3 = 3;
        power3 = 0;
        mPower3 = 300;
        traitA3 = "1";
        traitB3 = "2";
        traitC3 = "3";
        isMax3 = false;
        x3 = -1;
        y3 = -1;
        z3 = -1;

        for (int i = 0; i < unlockedFoodNames.Length; i++)
        {
            unlockedFoodNames[i] = "NULL";
        }
        unlockedFoodNames[0] = "Default";
        curRaceID = 0;
        curLeagueID = 0;
        numCoins = 0;
    }

}
