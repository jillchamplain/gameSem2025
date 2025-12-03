using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : Manager
{
    //Stores list of fooditem data containers 
    //Spawns using gameobject prefab and assigns class data from data container
    //Spawns using timer

    [Header("Stats")]
    [SerializeField] float foodSpawnInterval;
    bool canSpawn = true;
    [Header("Refs")]
    [SerializeField] GameObject foodPrefab;
    public GameObject getFoodPrefab() { return foodPrefab; }
    [SerializeField] List<GameObject> curFoods;
    public List<GameObject> getCurFoods() {  return curFoods; }
    public GameObject getCurFoodAt(int index) { return curFoods[index]; }
    public void setCurFoods(bool value)
    {
        foreach(GameObject food in curFoods)
        {
            food.SetActive(value);
        }
    }
    public void ClearFood()
    {
        foreach(GameObject food in curFoods)
        {
            Destroy(food);
        }
        curFoods.Clear();
    }

    [SerializeField] List<FoodItem> curFoodData;
    public List<FoodItem> getCurFoodData() { return curFoodData; }
    public FoodItem getCurFoodDataAt(int index)
    {
        for(int i = 0; i < curFoodData.Count; i++)
        {
            if (i == index)
                return curFoodData[i];
        }
        return null;
    }

    [SerializeField] List<Vector3> curFoodPos;
    public List<Vector3> getCurFoodPos() { return curFoodPos; }
    public Vector3 getCurFoodPosAt(int index)
    {
        for(int i = 0; i < curFoodPos.Count; i++)
        {
            if (i == index)
                return curFoodPos[i];
        }
        return Vector3.zero;
    }

    [SerializeField] List<FoodItem> unlockedFoodData;
    public List<FoodItem> getUnlockedFoodData() { return unlockedFoodData; }
    public FoodItem getUnlockedFoodDataAt(int index)
    {
        for(int i = 0; i < unlockedFoodData.Count; i++)
        {
            if (i == index)
                return unlockedFoodData[i];
        }
        return null;
    }
    [SerializeField] List<FoodItem> allFoodData;
    public List<FoodItem> getAllFoodData() { return allFoodData; }
    public FoodItem getFoodDataAt(int index)
    {
        for(int i = 0; i < allFoodData.Count; i++)
        {
            if(i == index)
            {
                return allFoodData[i];
            }
        }
        return null;
    }

    public FoodItem getFoodDataWithName(string name)
    {
        for (int i = 0; i < allFoodData.Count; i++)
        {
            if (allFoodData[i].getItemName() == name)
                return allFoodData[i];
        }
        return null;
    }

    [SerializeField] List<FoodItem> purchasedFoodData;
    public List<FoodItem> getPurchasedFoodData() { return purchasedFoodData; }
    public FoodItem getPurchasedFoodDataAt(int index)
    {
        for(int i = 0; i <  purchasedFoodData.Count; i++)
        {
            if (i == index)
                return purchasedFoodData[i];
        }
        return null;
    }

    public delegate void FoodSpawn(FoodItem theFood);
    public static event FoodSpawn foodSpawn;

    private void OnEnable()
    {
        canSpawn = true;
    }


    private void OnDisable()
    {
        canSpawn = false;
    }
    void Update()
    {
        //Debug.Log("canSpawn is " + canSpawn);
        if (canSpawn)
        {
            //Debug.Log("can spawn food: running coroutine");
            StartCoroutine(FoodSpawnTimer());
        }
    }

    public void InitFood(GameData theData)
    {
        //Reads Save Data to determine what foods the player has unlocked already
        unlockedFoodData.Clear();
        purchasedFoodData.Clear();
        curFoodData.Clear();
        curFoodPos.Clear();
        curFoods.Clear();

        if (allFoodData.Count <= 0)
            return;

        /*for(int i = 0; i < theData.unlockedFoodFlags.Length; i++)
        {
            if (theData.unlockedFoodFlags[i])
            {
                unlockedFoodData.Add(allFoodData[i]);
            }
        }*/
        //Debug.Log(theData.unlockedFoodNames);
       /* for(int i = 0; i < theData.unlockedFoodNames.Length; i++)
        {
            Debug.Log("Name in save is " + theData.unlockedFoodNames[i]);
             
            if (theData.unlockedFoodNames[i] == allFoodData[i].getItemName())
            {
                Debug.Log("Adding" + allFoodData)
                unlockedFoodData.Add(allFoodData[i]);
            }
        }*/

        foreach(FoodItem food in allFoodData)
        {
            for(int i = 0; i < theData.unlockedFoodNames.Length; i++)
            {
                if (theData.unlockedFoodNames[i] == food.getItemName())
                {
                    Debug.Log("Food unlocked is " + food.getItemName());
                    unlockedFoodData.Add(food);
                }
                
            }
            for(int i = 0; i < theData.purchasedFoodNames.Length; i++)
            {
                if (theData.purchasedFoodNames[i] == food.getItemName())
                {
                    purchasedFoodData.Add(food);
                }
            }
            ///Debug.Log(theData.currentFoodNames);
            for(int i = 0; i < theData.currentFoodNames.Length; i++)
            {
                if (theData.currentFoodNames[i] == food.getItemName())
                {
                    curFoodData.Add(food);
                }
            }
        }

        //BEHAVIOR FOR RESPAWNING FOOD THAT WAS LEFT
    }
     
    public void UnlockFood(ShopItem item) //Takes purchased ShopItem data from shop and unlocks corresponding fooditem
    {
        //Debug.Log("WORK");
        foreach(FoodItem food in allFoodData)
        {
            //Debug.Log("food in list is: " + food.getItemName());
            //Debug.Log("food unlocking is " + item.getItemName());
            if(food.getItemName() == item.getItemName())
            {
                Debug.Log("adding food" + item.getItemName());
                unlockedFoodData.Add(food);
            }
        }
    }

    public void PurchaseFood(ShopItem item)
    {
        foreach(FoodItem food in allFoodData)
        {
            if(food.getItemName() == item.getItemName())
            {
                unlockedFoodData.Remove(food);
                purchasedFoodData.Add(food);
            }
        }
    }

    //FOOD SPAWNING LOGIC
    IEnumerator FoodSpawnTimer()
    {
        canSpawn = false;
        //Debug.Log("spawning from " + this.gameObject);
        yield return new WaitForSecondsRealtime(foodSpawnInterval);
        foodSpawn?.Invoke(SelectRandomFood());
        canSpawn = true;
    }
    public void DeleteFood(GameObject theFood)
    {
        int index = 0;
        for(int i = 0; i < curFoods.Count; i++)
        {
            if (curFoods[i] == theFood)
                index = i;
        }
        curFoods.Remove(theFood);
        curFoodData.Remove(getCurFoodDataAt(index));
        curFoodPos.Remove(getCurFoodPosAt(index));
        Destroy(theFood);
    }

    public void SpawnAllFood(GameData theData)
    {
        Debug.Log("spawning all food in");
        for (int i = 0; i < curFoodData.Count; i++)
        {
            SpawnFoodFromData(getFoodDataWithName(theData.currentFoodNames[i]), new Vector3(theData.currentFoodPosX[i], theData.currentFoodPosY[i], theData.currentFoodPosZ[i]));
        }
    }
    

    public void SpawnFood(GameObject theFood, Vector3 spawnPos)
    {
        if (!theFood)
            return;

        GameObject foodPrefab = theFood;
        Vector2 spawn = spawnPos; //MOVE THIS 
        GameObject newFood = Instantiate(foodPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newFood.transform.parent = this.transform;

        curFoods.Add(newFood);
        curFoodPos.Add(newFood.transform.position);
    }

    public void SpawnFood(FoodItem theFood, Vector3 spawnPos)
    {
        if (!theFood)
        {
            return;
        }
        Vector2 spawn = spawnPos; //MOVE THIS 
        GameObject newFood = Instantiate(foodPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newFood.transform.parent = this.transform;


        //Assign data
        Food newFoodData = newFood.GetComponent<Food>();
        newFoodData.setName(theFood.getItemName());
        newFoodData.setPower(theFood.getPower());
        newFoodData.setSprite(theFood.getSprite());

        curFoodData.Add(theFood);
        curFoods.Add(newFood);
        curFoodPos.Add(newFood.transform.position);
    }

    public void SpawnFoodFromData(FoodItem theFood, Vector3 spawnPos)
    {
        if (!theFood)
        {
            return;
        }
        Vector2 spawn = spawnPos; //MOVE THIS 
        GameObject newFood = Instantiate(foodPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newFood.transform.parent = this.transform;


        //Assign data
        Food newFoodData = newFood.GetComponent<Food>();
        newFoodData.setName(theFood.getItemName());
        newFoodData.setPower(theFood.getPower());
        newFoodData.setSprite(theFood.getSprite());
        curFoods.Add(newFood);
        curFoodPos.Add(newFood.transform.position);
    }

    /*GameObject SelectRandomFood()
    {
        GameObject theObject = null;
        int index = Random.Range(0, unlockedFoods.Count);
        for(int i = 0; i < unlockedFoods.Count; i++)
        {
            if (i == index)
                return unlockedFoods[i];
        }
        return theObject;

    }*/

    FoodItem SelectRandomFood()
    {
        FoodItem theItem = null;
        int index = Random.Range(0, purchasedFoodData.Count);
        for(int i = 0; i < purchasedFoodData.Count; i++)
        {
            if (i == index)
                return purchasedFoodData[i];
        }
        return null;
    }
    
}
