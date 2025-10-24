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
            Debug.Log(food + " is " + value);
        }
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

        if (allFoodData.Count <= 0)
            return;

        /*for(int i = 0; i < theData.unlockedFoodFlags.Length; i++)
        {
            if (theData.unlockedFoodFlags[i])
            {
                unlockedFoodData.Add(allFoodData[i]);
            }
        }*/
        Debug.Log(theData.unlockedFoodNames);
        for(int i = 0; i < theData.unlockedFoodNames.Length; i++)
        {
            if (theData.unlockedFoodNames[i] == allFoodData[i].getItemName())
            {

                unlockedFoodData.Add(allFoodData[i]);
            }
        }

        //BEHAVIOR FOR RESPAWNING FOOD THAT WAS LEFT
    }
     
    public void UnlockFood(ShopItem item) //Takes purchased ShopItem data from shop and unlocks corresponding fooditem
    {
        foreach(FoodItem food in allFoodData)
        {
            if(food.getItemName() == item.getItemName())
            {
                unlockedFoodData.Add(food);
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
        curFoods.Remove(theFood);
        Destroy(theFood);
    }
    public void SpawnFood(GameObject theFood, Vector3 spawnPos)
    {
        GameObject foodPrefab = theFood;
        Vector2 spawn = spawnPos; //MOVE THIS 
        GameObject newFood = Instantiate(foodPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newFood.transform.parent = this.transform;

        curFoods.Add(newFood);
    }

    public void SpawnFood(FoodItem theFood, Vector3 spawnPos)
    {
        Vector2 spawn = spawnPos; //MOVE THIS 
        GameObject newFood = Instantiate(foodPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newFood.transform.parent = this.transform;


        //Assign data
        Food newFoodData = newFood.GetComponent<Food>();
        newFoodData.setName(theFood.getItemName());
        newFoodData.setPower(theFood.getPower());
        newFoodData.setSprite(theFood.getSprite());
        

        curFoods.Add(newFood);
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
        int index = Random.Range(0, unlockedFoodData.Count);
        for(int i = 0; i < unlockedFoodData.Count; i++)
        {
            if (i == index)
                return unlockedFoodData[i];
        }
        return null;
    }
    
}
