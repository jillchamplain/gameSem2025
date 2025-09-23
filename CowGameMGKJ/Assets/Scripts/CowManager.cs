using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int curGeneration;
    [Header("Refs")]
    [SerializeField] List<GameObject> curCows;
    public List<GameObject> getCows() { return curCows; }

    public Cow getCowAt(int index) { return curCows[index].GetComponent<Cow>(); }
    [SerializeField] GameObject cowPrefab;

    //EVENTS
    public delegate void CowSpawned(Cow theCow);
    public static event CowSpawned cowSpawned;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Vector2 spawn = GameManager.getInstance().SelectRandomSpawn(cowPrefab);
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);
        //theObject.transform.parent = this.gameObject.transform;
        theObject.GetComponent<Cow>().InitCow("NONAME", curGeneration + 1, (curGeneration + 1) * 100, "1", "2", "3");
        theObject.name = (curGeneration + 1).ToString();
        curCows.Add(theObject);
        ModifyCowUIIndex();
        curGeneration++;
        cowSpawned?.Invoke(theObject.GetComponent<Cow>());
    }

    public void SpawnCow(string name)
    {
        Vector2 spawn = GameManager.getInstance().SelectRandomSpawn(cowPrefab);
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);
        //theObject.transform.parent = this.gameObject.transform;
        theObject.GetComponent<Cow>().InitCow(name, curGeneration + 1, (curGeneration + 1) * 100, "1", "2", "3");
        theObject.name = name;
        curCows.Add(theObject);
        ModifyCowUIIndex();
        curGeneration++;
        cowSpawned?.Invoke(theObject.GetComponent<Cow>());
    }

    public void SpawnCow(Cow theCow)
    {
        Vector2 spawn = GameManager.getInstance().SelectRandomSpawn(cowPrefab);
        GameObject theObject = GameObject.Instantiate(cowPrefab, spawn, Quaternion.identity);
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
    }
}
