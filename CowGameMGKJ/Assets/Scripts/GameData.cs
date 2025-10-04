using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    //SAVE DATA
    public int curGeneration;
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

    //UNLOCKED FOODS
    public bool[] unlockedFoodFlags = new bool[5];

    //CURRENT RACE AND LEAGUE

    public GameData(int gen, Cow cow1, Cow cow2, Cow cow3, int numFoodsUnlocked)
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
        }

        //Reset Flags 
        for(int i = 0; i < unlockedFoodFlags.Length; i++)
        {
            unlockedFoodFlags[i] = false;
        }

        //Attribute flag unlocks by number of food unlocked
        for(int i = 0; i < numFoodsUnlocked; i++)
        {
            unlockedFoodFlags[i] = true;
        }
    }


    public GameData()
    {
        curGeneration = 3;

        name1 = "LIL COW";
        gen1 = 1;
        level1 = 0;
        mLevel1 = 1;
        power1 = 0;
        mPower1 = 100;
        traitA1 = "1";
        traitB1 = "2";
        traitC1 = "3";

        name2 = "COW JR";
        gen2 = 2;
        level2 = 0;
        mLevel2 = 2;
        power2 = 0;
        mPower2 = 200;
        traitA2 = "1";
        traitB2 = "2";
        traitC2 = "3";

        name3 = "COW III";
        gen3 = 3;
        level3 = 0;
        mLevel3 = 3;
        power3 = 0;
        mPower3 = 200;
        traitA3 = "1";
        traitB3 = "2";
        traitC3 = "3";

        for(int i = 0; i < 5; i++)
        {
            if (i == 0)
                unlockedFoodFlags[i] = true;
            else
                unlockedFoodFlags[i] = false;
        }
    }

}
