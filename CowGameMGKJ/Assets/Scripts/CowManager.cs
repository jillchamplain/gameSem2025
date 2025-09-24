using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowManager : Manager
{
    [Header("Stats")]
    [SerializeField] int curGeneration;
    [Header("Refs")]
    [SerializeField] List<GameObject> curCows;
    public List<GameObject> getCows() { return curCows; }

    public Cow getCowAt(int index) { return curCows[index].GetComponent<Cow>(); }

    public void setCows(bool value)
    {
        foreach(GameObject cow in curCows)
        {
            cow.SetActive(value);
        }
    }
    [SerializeField] public GameObject cowPrefab;

    //EVENTS
    public delegate void CowSpawned(Cow theCow);
    public static event CowSpawned cowSpawned;

    // Start is called before the first frame update

    public void UpdateCowData(GameData theData)
    {
        curGeneration = theData.curGeneration;

        //Transforms placeholder cows into cows from data
        getCowAt(0).InitCow(theData.name1, theData.gen1, theData.power1, theData.mPower1, theData.traitA1, theData.traitB1, theData.traitC1);
        getCowAt(1).InitCow(theData.name2, theData.gen2, theData.power2, theData.mPower2, theData.traitA2, theData.traitB2, theData.traitC2);
        getCowAt(2).InitCow(theData.name3, theData.gen3, theData.power3, theData.mPower3, theData.traitA3, theData.traitB3, theData.traitC3);

    }

    public void SpawnCows(int numCows)
    {
        for(int i = 0; i < numCows; i++)
        {
            SpawnCow();
        }
    }

    public void SpawnCow()
    {
        //Vector2 spawn = GameManager.getInstance().SelectRandomSpawn(cowPrefab);
        //GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);
        //theObject.transform.parent = this.gameObject.transform;
        //theObject.GetComponent<Cow>().InitCow("NONAME", curGeneration + 1, (curGeneration + 1) * 100, "1", "2", "3");
        //theObject.name = (curGeneration + 1).ToString();
        //curCows.Add(theObject);
        //ModifyCowUIIndex();
        //curGeneration++;
        //cowSpawned?.Invoke(theObject.GetComponent<Cow>());
    }

    public void SpawnCow(string name)
    {
        /*Vector2 spawn = GameManager.getInstance().SelectRandomSpawn(cowPrefab);
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);
        //theObject.transform.parent = this.gameObject.transform;
        theObject.GetComponent<Cow>().InitCow(name, curGeneration + 1, (curGeneration + 1) * 100, "1", "2", "3");
        theObject.name = name;
        curCows.Add(theObject);
        ModifyCowUIIndex();
        curGeneration++;
        cowSpawned?.Invoke(theObject.GetComponent<Cow>());
        */
    }

    public void SpawnCow(Vector3 spawnPos)
    {
        Vector2 spawn = spawnPos;
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);

        theObject.GetComponent<Cow>().InitCow("LIL COW", curGeneration + 1, (curGeneration + 1) * 100, "1", "2", "3");
        theObject.name = "LIL COW";

        curCows.Add(theObject);

        ModifyCowUIIndex();
        curGeneration++;
        
        cowSpawned?.Invoke(theObject.GetComponent<Cow>());
        //theObject.transform.parent = this.gameObject.transform;

    }

    public void DeleteCow(GameObject theCow)
    {
        List<GameObject> tempCows = new List<GameObject>();
        for(int i = 0; i < curCows.Count; i++)
        {
            if (curCows[i] != theCow)
            {
                tempCows.Add(curCows[i]);
            }
        }

        Destroy(theCow);
        curCows = tempCows;
        ModifyCowUIIndex();
    }

    public void DeleteCow()
    {
        List<GameObject> tempCows = new List<GameObject>();
        for (int i = 0; i < curCows.Count; i++)
        {
            if (i != 0)
            {
                tempCows.Add(curCows[i]);
            }
        }

        Destroy(curCows[0]);
        curCows = tempCows;
        ModifyCowUIIndex();
    }

    void ModifyCowUIIndex()
    {
        for(int i = 0; i < curCows.Count; i++)
        {
            curCows[i].GetComponent<Cow>().setUIIndex(i);
        }
    } //MOVE THIS BRUHHHHHHHHH
}
