using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float foodSpawnInterval;
    bool canSpawn = true;

    [Header("Refs")]
    [SerializeField] List<GameObject> curFoods;
    public List<GameObject> getCurFoods() {  return curFoods; }
    public GameObject getCurFoodAt(int index) { return curFoods[index]; }
    [SerializeField] List<GameObject> unlockedFoods;
    [SerializeField] List<GameObject> allFoods;

    void Update()
    {
        if (canSpawn)
            StartCoroutine(FoodSpawnTimer());
    }

    IEnumerator FoodSpawnTimer()
    {
        canSpawn = false;
        SpawnFood();
        yield return new WaitForSecondsRealtime(foodSpawnInterval);
        canSpawn = true;
    }

    public void DeleteFood(GameObject theFood)
    {
        curFoods.Remove(theFood);
        Destroy(theFood);
    }

    void SpawnFood()
    {
        GameObject foodPrefab = SelectRandomFood();
        Vector2 spawn = GameManager.instance.SelectRandomSpawn(foodPrefab);
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
