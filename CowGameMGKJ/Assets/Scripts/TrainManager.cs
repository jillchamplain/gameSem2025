using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] List<TrainRegimen> unlockedTrains;
    [SerializeField] List<TrainRegimen> allTrains;

    [Serializable]
    struct TrainRegimen
    {
        [SerializeField] string regimenName;
        public string getRegimenName() { return regimenName; }

        [SerializeField] int goodEffortIncrease;
        public int getGoodEffortIncrease() { return goodEffortIncrease; }
        [SerializeField] int greatEffortIncrease;
        public int getGreatEffortIncrease() { return greatEffortIncrease; }
        [SerializeField] int amazingEffortIncrease;
        public int getAmazingEffortIncrease() { return amazingEffortIncrease; }
    }
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ___________
        // |  ~   ~  |    
        // |  O   O  |   
        // |    ^    |        -nbrody
        // |  \___/  |   
        // -----------
        //      |   
        //      |
        //     /|\
       //     / | \
       //    /  |  \
       //       |
       //      / \
       //     /   \
        //   /     \               

    }
}
