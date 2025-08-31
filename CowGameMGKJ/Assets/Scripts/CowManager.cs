using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int curGeneration;
    [Header("Refs")]
    [SerializeField] List<GameObject> curCows;

    public List<GameObject> getCurCows() { return curCows; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
