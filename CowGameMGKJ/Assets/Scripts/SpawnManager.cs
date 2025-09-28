using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Manager
{
    [SerializeField] BoxCollider2D spawnZone;
    public Vector2 SelectRandomSpawn(GameObject theObject) //NEED TO!!!!!!!!!!!!! CHECK FOR OVERLAP WITH OTHER FOOD AND COWS STILL DOESN'T WORK
    {
        Debug.Log(theObject);
        bool canSelectSpawn = false;
        Vector2 theSpawn = Vector2.zero;
        theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);

        int attempt = 0;
        int numAttempts = 200;

        while (!canSelectSpawn && attempt < numAttempts)
        {
            if (spawnZone.bounds.Contains(theSpawn))
            {
                canSelectSpawn = true;
            }
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(theSpawn, theObject.transform.localScale.x * 2f);
            foreach (Collider2D collider in colliders)
            {
                canSelectSpawn = false;
            }

            if (!canSelectSpawn)
            {
                theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
                theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
                attempt++;
            }
        }
        if (attempt >= numAttempts)
        {
            return Vector2.zero;
        }
        return theSpawn;
    }
}
