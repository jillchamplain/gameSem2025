using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//MAYBE DELETE

public class GlobalData : MonoBehaviour
{
    //Need to store cow data


    public static GlobalData inst;
    [SerializeField] List<GameObject> curCows;
    public void AddCow(Cow newCow) { curCows.Add(newCow.gameObject); }
    public void RemoveCow(Cow theCow)
    {
        curCows.Remove(theCow.gameObject);
        UpdateCurCowData();
    }

    [SerializeField] int cowGeneration;
    private void Start()
    {
        inst = this;
        DontDestroyOnLoad(this);


        UpdateCurCowData();
    }

    void UpdateCurCowData()
    {
        foreach(GameObject cow in curCows)
        {
            DontDestroyOnLoad(cow);
        }
    }

    
}
