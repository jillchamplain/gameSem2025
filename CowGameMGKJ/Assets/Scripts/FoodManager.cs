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
    [SerializeField] List<GameObject> allFoods;
    public delegate void FoodSpawn(GameObject theFood);
    public static event FoodSpawn foodSpawn;
    void Update()
    {
        if (canSpawn)
            StartCoroutine(FoodSpawnTimer());
    }
    IEnumerator FoodSpawnTimer()
    {
        canSpawn = false;
        //Debug.Log("spawning from " + this.gameObject);
        foodSpawn?.Invoke(SelectRandomFood());
        yield return new WaitForSecondsRealtime(foodSpawnInterval);
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
