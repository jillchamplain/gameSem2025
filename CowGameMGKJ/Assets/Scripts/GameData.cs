using DG.Tweening.Plugins;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int traitA1;
    public int traitB1;
    public int traitC1;
    public int pattern1; //FIGURE OUT PATTERNS
    public bool isMax1;
    public float x1;
    public float y1;
    public float z1;
    public string hat1;
    public string top1;
    public string bot1;

    //COW 2
    public string name2;
    public int gen2;
    public int level2;
    public int mLevel2;
    public int power2;
    public int mPower2;
    public int traitA2; 
    public int traitB2;
    public int traitC2;
    public int pattern2;
    public bool isMax2;
    public float x2;
    public float y2;
    public float z2;
    public string hat2;
    public string top2;
    public string bot2;

    //COW 3
    public string name3;
    public int gen3;
    public int level3;
    public int mLevel3;
    public int power3;
    public int mPower3;
    public int traitA3;
    public int traitB3;
    public int traitC3;
    public int pattern3;
    public bool isMax3;
    public float x3;
    public float y3;
    public float z3;
    public string hat3;
    public string top3;
    public string bot3;

    //UNLOCKED FOODS AGAIn
    public string[] unlockedFoodNames = new string[10];
    public string[] purchasedFoodNames = new string[10];
    public string[] currentFoodNames = new string[100];
    //public Vector3[] currentFoodPos = new Vector3[100];
    public float[] currentFoodPosX = new float[100];
    public float[] currentFoodPosY = new float[100];
    public float[] currentFoodPosZ = new float[100];
    

    //Unlocked Patterns
    public string[] unlockedPatternNames = new string[4];
    public string[] purchasedPatternNames = new string[4];

    //Cosmetics
    public string[] unlockedCosmeticNames = new string[18];
    public string[] purchasedCosmeticNames = new string[100];
    //public Vector3[] purchasedCosmeticPos = new Vector3[100];
    public float[] purchasedCosmeticPosX = new float[100];
    public float[] purchasedCosmeticPosY = new float[100];
    public float[] purchasedCosmeticPosZ = new float[100];

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
            traitA1 = (int)cow1.getTraitAt(0);
            traitB1 = (int)cow1.getTraitAt(1);
            traitC1 = (int)cow1.getTraitAt(2);
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
            traitA2 = (int)cow2.getTraitAt(0);
            traitB2 = (int)cow2.getTraitAt(1);
            traitC2 = (int)cow2.getTraitAt(2);
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
            traitA3 = (int)cow3.getTraitAt(0);
            traitB3 = (int)cow3.getTraitAt(1);
            traitC3 = (int)cow3.getTraitAt(2);
            isMax3 = cow3.getIsMaxLevel();
            x3 = cow3.gameObject.transform.position.x;
            y3 = cow3.gameObject.transform.position.y;
            z3 = cow3.gameObject.transform.position.z;
        }

    

        curRaceID = curRaceIndex;
        curLeagueID = curLeagueIndex;
        
    }

    public GameData(CowManager cowManager, FoodManager foodManager, ShopManager shopManager, RaceManager raceManager, CosmeticManager cosmeticManager)
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
            traitA1 = (int)cow1.getTraitAt(0);
            traitB1 = (int)cow1.getTraitAt(1);
            traitC1 = (int)cow1.getTraitAt(2);
            pattern1 = (int)cow1.getPattern();
            isMax1 = cow1.getIsMaxLevel();
            x1 = cow1.gameObject.transform.position.x;
            y1 = cow1.gameObject.transform.position.y;
            z1 = cow1.gameObject.transform.position.z;
            hat1 = cow1.getHat();
            top1 = cow1.getTop();
            bot1 = cow1.getBot();
        }

        if (cow2 != null)
        {
            name2 = cow2.getName();
            gen2 = cow2.getGen();
            level2 = cow2.getLevel();
            mLevel2 = cow2.getMaxLevel();
            power2 = cow2.getPower();
            mPower2 = cow2.getMaxPower();
            traitA2 = (int)cow2.getTraitAt(0);
            traitB2 = (int)cow2.getTraitAt(1);
            traitC2 = (int)cow2.getTraitAt(2);
            pattern2 = (int)cow2.getPattern();
            isMax2 = cow2.getIsMaxLevel();
            x2 = cow2.gameObject.transform.position.x;
            y2 = cow2.gameObject.transform.position.y;
            z2 = cow2.gameObject.transform.position.z;
            hat2 = cow2.getHat();
            top2 = cow2.getTop();
            bot2 = cow2.getBot();
        }

        if (cow3 != null)
        {
            name3 = cow3.getName();
            gen3 = cow3.getGen();
            level3 = cow3.getLevel();
            mLevel3 = cow3.getMaxLevel();
            power3 = cow3.getPower();
            mPower3 = cow3.getMaxPower();
            traitA3 = (int)cow3.getTraitAt(0);
            traitB3 = (int)cow3.getTraitAt(1);
            traitC3 = (int)cow3.getTraitAt(2);
            pattern3 = (int)cow3.getPattern();
            isMax3 = cow3.getIsMaxLevel();
            x3 = cow3.gameObject.transform.position.x;
            y3 = cow3.gameObject.transform.position.y;
            z3 = cow3.gameObject.transform.position.z;
            hat3 = cow3.getHat();
            top3 = cow3.getTop();
            bot3 = cow3.getBot();
        }

        //Types of food to purchase in store
        for (int i = 0; i < foodManager.getUnlockedFoodData().Count; i++)
        {
            unlockedFoodNames[i] = foodManager.getUnlockedFoodDataAt(i).getItemName();
        }

        //Types of food can spawn
        for (int i = 0; i < foodManager.getPurchasedFoodData().Count; i++)
        {
            purchasedFoodNames[i] = foodManager.getPurchasedFoodDataAt(i).getItemName();
        }

        //Current foods spawned
        for(int i = 0; i < foodManager.getCurFoodData().Count; i++)
        {
            currentFoodNames[i] = foodManager.getCurFoodDataAt(i).getItemName();
        }

        for(int i = 0; i < foodManager.getCurFoodPos().Count; i++)
        {
            //currentFoodPos[i] = foodManager.getCurFoodAt(i).gameObject.transform.position;
            currentFoodPosX[i] = foodManager.getCurFoodPosAt(i).x;
            currentFoodPosY[i] = foodManager.getCurFoodPosAt(i).y;
            currentFoodPosZ[i] = foodManager.getCurFoodPosAt(i).z;
        }



        //Types of patterns to purchase in store
        for (int i = 0; i < cowManager.getUnlockedPatternData().Count; i++)
        {
            unlockedPatternNames[i] = cowManager.getUnlockedPatternDataAt(i).getItemName();
        }
        //Types of pattern can spawn
        for(int i = 0; i < cowManager.getPurchasedPatternData().Count; i++)
        {
            purchasedPatternNames[i] = cowManager.getPurchasedPatternDataAt(i).getItemName();
        }


        //Types of cosmetics to purchase in store
        for(int i = 0; i < cosmeticManager.getUnlockedCosmeticItems().Count(); i++)
        {
            unlockedCosmeticNames[i] = cosmeticManager.getUnlockedCosmeticAt(i).getItemName();
        }

        //Types of cosmetics to spawn 
        for(int i = 0; i < cosmeticManager.getPurchasedCosmeticItems().Count(); i++)
        {
            purchasedCosmeticNames[i] = cosmeticManager.getPurchasedCosmeticAt(i).getItemName();
        }

        for(int i = 0; i < cosmeticManager.getCurrentCosmeticItems().Count(); i++)
        {
            //purchasedCosmeticPos[i] = cosmeticManager.getCurrentCosmeticItemAt(i).transform.position;
            purchasedCosmeticPosX[i] = cosmeticManager.getCurCosmeticPosAt(i).x;
            purchasedCosmeticPosY[i] = cosmeticManager.getCurCosmeticPosAt(i).y;
            purchasedCosmeticPosZ[i] = cosmeticManager.getCurCosmeticPosAt(i).z;
        }


        curRaceID = raceManager.getCurRaceIndex();
        curLeagueID = raceManager.getCurLeagueIndex();

        numCoins = shopManager.getCoins();

    }

    public GameData(FoodManager foodManager, ShopManager shopManager, RaceManager raceManager)
    {
        for (int i = foodManager.getUnlockedFoodData().Count; i < unlockedFoodNames.Length; i++)
        {
            unlockedFoodNames[i] = "NULL";
        }

        for (int i = 0; i < foodManager.getUnlockedFoodData().Count; i++)
        {
            unlockedFoodNames[i] = foodManager.getUnlockedFoodDataAt(i).getItemName();
        }
        

        curRaceID = raceManager.getCurRaceIndex();
        curLeagueID = raceManager.getCurLeagueIndex();

        numCoins = shopManager.getCoins();

    }


    public GameData()
    {
        Debug.Log("Being reset");
        curGeneration = 3;
        numCoins = 0;
        name1 = "LIL COW";
       // Debug.Log(name1);
        gen1 = 1;
        level1 = 0;
        mLevel1 = 1;
        power1 = 0;
        mPower1 = 100;
        traitA1 = -1;
        traitB1 = -1;
        traitC1 = -1;
        pattern1 = 0;
        isMax1 = false;
        x1 = -1;
        y1 = -1;
        z1 = -1;
        hat1 = "NULL";
        top1 = "NULL";
        bot1 = "NULL";

        name2 = "COW JR";
        gen2 = 2;
        level2 = 0;
        mLevel2 = 2;
        power2 = 0;
        mPower2 = 200;
        traitA2 = -1;
        traitB2 = -1;
        traitC2 = -1;
        pattern2 = 0;
        isMax2 = false;
        x2 = -1;
        y2 = -1;
        z2 = -1;
        hat2 = "NULL";
        top2 = "NULL";
        bot2 = "NULL";

        name3 = "COW III";
        gen3 = 3;
        level3 = 0;
        mLevel3 = 3;
        power3 = 0;
        mPower3 = 300;
        traitA3 = -1;
        traitB3 = -1;
        traitC3 = -1;
        pattern3 = 0;
        isMax3 = false;
        x3 = -1;
        y3 = -1;
        z3 = -1;
        hat3 = "NULL";
        top3 = "NULL";
        bot3 = "NULL";

        for (int i = 0; i < unlockedFoodNames.Length; i++)
        {
            unlockedFoodNames[i] = "NULL";
        }

        for (int i = 0; i < purchasedFoodNames.Length; i++)
        {
           purchasedFoodNames[i] = "NULL";
        }
        purchasedFoodNames[0] = "Berry";

        for(int i = 0; i < currentFoodNames.Length; i++)
        {
            currentFoodNames[i] = "NULL";
        }

        for(int i = 0; i < currentFoodPosX.Length; i++)
        {
            currentFoodPosX[i] = 0;
            currentFoodPosY[i] = 0;
            currentFoodPosZ[i] = 0;
        }

        for (int i = 0; i < unlockedPatternNames.Length; i++)
        {
            unlockedPatternNames[i] = "NULL";
        }

        for (int i = 0; i < purchasedPatternNames.Length; i++)
        {
            purchasedPatternNames[i] = "NULL";
        }
        purchasedPatternNames[0] = "Blank";

        for(int i = 0; i < unlockedCosmeticNames.Length; i++)
        {
            unlockedCosmeticNames[i] = "NULL";
        }

        unlockedCosmeticNames[0] = "Party Hat";

        for(int i = 0; i < purchasedCosmeticNames.Length; i++)
        {
            purchasedCosmeticNames[i] = "NULL";
        }

        for(int i = 0; i< purchasedCosmeticPosX.Length; i++)
        {
            purchasedCosmeticPosX[i] = 0;
            purchasedCosmeticPosY[i] = 0;
            purchasedCosmeticPosZ[i] = 0;
        }

        curRaceID = 0;
        curLeagueID = 0;
        numCoins = 0;
    }

}
