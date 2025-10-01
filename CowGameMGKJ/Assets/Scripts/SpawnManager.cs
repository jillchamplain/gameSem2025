using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Manager
{
    //[SerializeField] BoxCollider2D spawnZone;
    [SerializeField] Vector2 spawnBounds;
    public Vector2 SelectRandomSpawn(GameObject theObject)
    {
        
        bool canSelectSpawn = false;
        Vector2 theSpawn = Vector2.zero;
        //Debug.Log("ZAX: " + spawnBounds);
        //Keeps setting initializing cows to 0, -1.25?
        theSpawn.x = Random.Range(-spawnBounds.x, spawnBounds.x);
        theSpawn.y = Random.Range(-spawnBounds.y - 1.25f, spawnBounds.y - 2);
        int attempt = 0;
        int numAttempts = 200;

        while (!canSelectSpawn && attempt < numAttempts)
        {
            //Debug.Log("trying to spawn at: " + theSpawn);
            bool isWithinBounds = true;
            if (theSpawn.x > spawnBounds.x || theSpawn.x < -spawnBounds.x)
            {
                isWithinBounds = false;
            }
            if (theSpawn.y > spawnBounds.y - 2 || theSpawn.y < -spawnBounds.y - 1.25)
            {
                isWithinBounds = false;
            }

            if (isWithinBounds)
            {
                canSelectSpawn = true;
            }
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(theSpawn, theObject.transform.localScale.x);
            foreach (Collider2D collider in colliders)
            {
                //Debug.Log("colliding with " + collider);
                canSelectSpawn = false;
            }

            if (!canSelectSpawn)
            {
                attempt++;
                theSpawn.x = Random.Range(-spawnBounds.x, spawnBounds.x);
                theSpawn.y = Random.Range(-spawnBounds.y, spawnBounds.y);
                //Debug.Log("Resetting to " + theSpawn);
            }
        }
        if (attempt >= numAttempts)
        {
            //Debug.Log("numAttempts bad: " + attempt);
            return Vector2.zero;
        }
        return theSpawn;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnBounds.x * 2, spawnBounds.y * 2, 0));
    }
}
