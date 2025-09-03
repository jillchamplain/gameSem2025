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
        [SerializeField] string name;
        public string getName() { return name; }

        [SerializeField] int goodEffortIncrease;
        public int getGoodEffortIncrease() { return goodEffortIncrease; }
        [SerializeField] int greatEffortIncrease;
        public int getGreatEffortIncrease() { return greatEffortIncrease; }
        [SerializeField] int amazingEffortIncrease;
        public int getAmazingEffortIncrease() { return amazingEffortIncrease; }
    }

    public void TrainCow()
    {
        TrainRegimen theTraining = SelectRandomTraining();
        Debug.Log(theTraining.getName());
    }
    
    TrainRegimen SelectRandomTraining()
    {
        TrainRegimen theTraining = unlockedTrains[0];
        int index = UnityEngine.Random.Range(0, unlockedTrains.Count);
        for(int i = 0; i < unlockedTrains.Count; i++)
        {
            if (i == index)
                return unlockedTrains[i];
        }

        return theTraining;
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
