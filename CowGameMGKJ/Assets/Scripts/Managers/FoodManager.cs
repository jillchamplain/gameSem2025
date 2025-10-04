using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : Manager
{
    [Header("Stats")]
    [SerializeField] float foodSpawnInterval;
    bool canSpawn = true;
    [Header("Refs")]
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
    [SerializeField] List<GameObject> unlockedFoods;
    public List<GameObject> getUnlockedFoods() { return unlockedFoods; }
    [SerializeField] List<GameObject> allFoods;
    public delegate void FoodSpawn(GameObject theFood);
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
        unlockedFoods.Clear();

        if (allFoods.Count <= 0)
            return;

        for(int i = 0; i < theData.unlockedFoodFlags.Length; i++)
        {
            if (theData.unlockedFoodFlags[i])
            {
                unlockedFoods.Add(allFoods[i]);
            }
        }

        //BEHAVIOR FOR RESPAWNING FOOD THAT WAS LEFT
    }

    public void UnlockFood()
    {
        //Debug.Log("Unlocking Food");
        //Get index of last unlocked food
        int index = unlockedFoods.Count - 1;
        for(int i = 0; i < allFoods.Count; i++)
        {
            if (i - 1 == index)
                unlockedFoods.Add(allFoods[i]);
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
    GameObject SelectRandomFood()
    {
        GameObject theObject = null;
        int index = Random.Range(0, unlockedFoods.Count);
        for(int i = 0; i < unlockedFoods.Count; i++)
        {
            if (i == index)
                return unlockedFoods[i];
        }
        return theObject;

    }
}
