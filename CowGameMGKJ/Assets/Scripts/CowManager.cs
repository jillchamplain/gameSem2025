using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int curGeneration;
    [Header("Refs")]
    [SerializeField] List<GameObject> curCows;
    [SerializeField] GameObject cowPrefab;

    public List<GameObject> getCurCows() { return curCows; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnCows(int numCows)
    {
        for(int i = 0; i < numCows; i++)
        {
            SpawnCow();
        }
    }

    void SpawnCow()
    {
        GameObject theObject = GameObject.Instantiate(cowPrefab);
    }

    void SpawnCow(string name)
    {
        GameObject theObject = GameObject.Instantiate(cowPrefab);
        theObject.GetComponent<Cow>().InitCow(name, curGeneration + 1, (curGeneration + 1) * 100);
    }
}
