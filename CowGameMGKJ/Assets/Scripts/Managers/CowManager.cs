using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowManager : Manager
{
    [Header("Stats")]
    [SerializeField] public int curGeneration;
    [Header("Refs")]
    [SerializeField] List<string> randomCowNames;
    public string getRandomName()
    {
        string theName;
        int index = Random.Range(0, randomCowNames.Count);

        for(int i = 0; i < randomCowNames.Count; i++)
        {
            if(i == index)
            {
                return randomCowNames[i];
            }
        }

        return "NULL";
    }


    [SerializeField] List<GameObject> curCows;
    public List<GameObject> getCows() { return curCows; }
    public Cow getCowAt(int index) 
    {
        if (index > curCows.Count)
            return null;
        if (curCows[index].GetComponent<Cow>())
            return curCows[index].GetComponent<Cow>();
        return null;
    }
    public int getCowIndex(Cow theCow)
    {
        for(int i = 0; i < curCows.Count;i++)
        {
            if (theCow == curCows[i])
                return i;
        }
        return -1;
    }
    public void setCows(bool value)
    {
        foreach(GameObject cow in curCows)
        {
            cow.SetActive(value);
        }
    }
    public void ClearCows()
    {
        foreach(GameObject cow in curCows)
        {
            Destroy(cow);
        }
        curCows.Clear();
    }
    [SerializeField] public GameObject cowPrefab;

    //EVENTS
    public delegate void CowSpawned(Cow theCow);
    public static event CowSpawned cowSpawned;

    public void InitCurCows(GameData theData)
    {
        if(theData == null)
        {
            Debug.Log("Save file doesn't exist!");
        }

        curGeneration = theData.curGeneration;
        //Debug.Log("Generation: " + curGeneration);

        //Transforms placeholder cows into cows from data
        if (theData.gen1 != 0)
        {
            //Debug.Log("Cow1: " + theData.name1 + " " + theData.power1 + " " + theData.mPower1 + " " + theData.traitA1 + " " + theData.traitB1 + " " + theData.traitC1);
            getCowAt(0).InitCow(theData.name1, theData.gen1, theData.level1, theData.mLevel1, theData.power1, theData.mPower1, theData.traitA1, theData.traitB1, theData.traitC1);
            if(theData.x1 != -1 && theData.y1 != -1 && theData.z1 != -1)
                getCowAt(0).transform.position = new Vector3(theData.x1, theData.y1, theData.z1);
        }
        if (theData.gen2 != 0)
        {
            //Debug.Log("Cow2: " + theData.name2 + " " + theData.power2 + " " + theData.mPower2 + " " + theData.traitA2 + " " + theData.traitB2 + " " + theData.traitC2);
            getCowAt(1).InitCow(theData.name2, theData.gen2, theData.level2, theData.mLevel2, theData.power2, theData.mPower2, theData.traitA2, theData.traitB2, theData.traitC2);
            if (theData.x2 != -1 && theData.y2 != -1 && theData.z2 != -1)
                getCowAt(1).transform.position = new Vector3(theData.x2, theData.y2, theData.z2);
        }
        if (theData.gen3 != 0)
        {
            //Debug.Log("Cow3: " + theData.name3 + " " + theData.power3 + " " + theData.mPower3 + " " + theData.traitA3 + " " + theData.traitB3 + " " + theData.traitC3);
            getCowAt(2).InitCow(theData.name3, theData.gen3, theData.level3, theData.mLevel3, theData.power3, theData.mPower3, theData.traitA3, theData.traitB3, theData.traitC3);
            if (theData.x3 != -1 && theData.y3 != -1 && theData.z3 != -1)
                getCowAt(2).transform.position = new Vector3(theData.x3, theData.y3, theData.z3);
        }

    }
    public void SpawnCow(Vector3 spawnPos)
    {
        //Debug.Log("Spawning cow at " + spawnPos);
        curGeneration++;
        Vector2 spawn = spawnPos;
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);

        //CHECK IF COWS SHARE NAMES(?)
        theObject.GetComponent<Cow>().InitCow(getRandomName(), curGeneration, (curGeneration) * 100, "1", "2", "3");
        theObject.name = getRandomName();

        curCows.Add(theObject);

        ModifyCowUIIndex();
        
        
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
