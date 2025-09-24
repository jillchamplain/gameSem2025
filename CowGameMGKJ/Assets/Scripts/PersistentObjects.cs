using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    //Manages persistent gameObjects and manages duplicates between scenes

    [SerializeField] static GameObject[] persistentObjects;
    [SerializeField] int objectIndex;
    void Awake()
    {
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(this.gameObject);

        }
        else if (persistentObjects[objectIndex] != null)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
