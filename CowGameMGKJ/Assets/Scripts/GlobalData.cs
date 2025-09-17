using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    //Need to store cow data

    public static GlobalData inst;
    [SerializeField] List<GameObject> cows;
    public List<GameObject> getCows() { return cows; }
    public GameObject getCowAtIndex(int index) { return cows[index]; }
    public void removeCow(GameObject cow) { cows.Remove(cow); }
    private void Start()
    {
        inst = this;
    }

    
}
