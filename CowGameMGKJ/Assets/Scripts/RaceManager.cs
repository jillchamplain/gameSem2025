using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Race curRace;
    [SerializeField] League curLeague;
    [SerializeField] List<League> allLeagues;
    public League getLeague() { return curLeague; }

    [Serializable]
    public struct Race
    {
        [SerializeField] int power;
        public int getPower() { return power; }
        [SerializeField] List<string> traits;
        public List<string> getTraits() { return traits; }
        public string getTraitAt(int index) { return traits[index]; }

        public static bool operator ==(Race raceA, Race raceB)
        {
            bool isEqual = true;
            if (raceA.getPower() != raceB.getPower())
                isEqual = false;
            if (raceA.getTraitAt(0) != raceB.getTraitAt(0))
                isEqual = false;
            if (raceA.getTraitAt(1) != raceB.getTraitAt(1))
                isEqual = false;
            if (raceA.getTraitAt(2) != raceB.getTraitAt(2))
                isEqual = false;

                return isEqual;
        }

        public static bool operator !=(Race raceA, Race raceB)
        {
            bool isEqual = true;
            if (raceA.getPower() != raceB.getPower())
                isEqual = false;
            if (raceA.getTraitAt(0) != raceB.getTraitAt(0))
                isEqual = false;
            if (raceA.getTraitAt(1) != raceB.getTraitAt(1))
                isEqual = false;
            if (raceA.getTraitAt(2) != raceB.getTraitAt(2))
                isEqual = false;

            return isEqual;
        }
    }
    [Serializable]
    public struct League
    {
        [SerializeField] List<Race> races;
        public List<Race> getRaces() { return races; }
        public Race getRaceAt(int index) { return races[index]; }
    }

    private void Start()
    {
        curRace = curLeague.getRaceAt(0);
        curLeague = allLeagues[0];
    }

    public void RaceCow(Cow theCow)
    {
        
        if(theCow.getPower() < curRace.getPower())
        {
            Debug.Log("Not enough power!");
            
        }
        else
        {
            Debug.Log("You win the race!");
            bool needNewLeague = true;
            for(int i = 0; i < curLeague.getRaces().Count; i++)
            {
                if(curRace == curLeague.getRaceAt(i))
                {
                    if (i < curLeague.getRaces().Count - 1)
                    {
                        curRace = curLeague.getRaceAt(i + 1);
                        needNewLeague = false;
                        break;
                    }
                }
            }
        }
    }
}
