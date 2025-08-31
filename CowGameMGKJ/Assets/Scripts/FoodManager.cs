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
    [SerializeField] List<GameObject> unlockedFoods;
    [SerializeField] List<GameObject> allFoods;

    [SerializeField] BoxCollider2D spawnZone;

    void Update()
    {
        if (canSpawn)
            StartCoroutine(FoodSpawnTimer());
    }

    IEnumerator FoodSpawnTimer()
    {
        canSpawn = false;
        yield return new WaitForSecondsRealtime(foodSpawnInterval);
        SpawnFood();
        canSpawn = true;
    }

    public void DeleteFood(GameObject theFood)
    {
        curFoods.Remove(theFood);
        Destroy(theFood);
    }

    void SpawnFood()
    {
        GameObject newFood = Instantiate(SelectRandomFood(), gameObject.transform);
        newFood.transform.position = SelectRandomSpawn();

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

    Vector2 SelectRandomSpawn() //NEED TO!!!!!!!!!!!!! CHECK FOR OVERLAP WITH OTHER FOOD AND COWS 
    {
        bool canSelectSpawn = false;
        Vector2 theSpawn = Vector2.zero;
        theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        while(!canSelectSpawn)
        {
            if (spawnZone.bounds.Contains(theSpawn))
            {
                canSelectSpawn = true;
            }
            else
                Debug.Log("spawned Wrong");
                theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
                theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        }
        //Debug.Log(theSpawn);

        return theSpawn;
    }
}
