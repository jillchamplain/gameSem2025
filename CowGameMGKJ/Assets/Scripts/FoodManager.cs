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
        GameObject newFood = Instantiate(SelectRandomFood(), gameObject.transform);
        newFood.transform.position = GameManager.getInstance().SelectRandomSpawn();

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
