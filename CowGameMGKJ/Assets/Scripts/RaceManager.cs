using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class RaceManager : Manager
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
            bool isEqual = false;
            if (raceA.getPower() != raceB.getPower())
                isEqual = true;
            if (raceA.getTraitAt(0) != raceB.getTraitAt(0))
                isEqual = true;
            if (raceA.getTraitAt(1) != raceB.getTraitAt(1))
                isEqual = true;
            if (raceA.getTraitAt(2) != raceB.getTraitAt(2))
                isEqual = true;

            return isEqual;
        }
    }
    [Serializable]
    public struct League
    {
        [SerializeField] List<Race> races;
        public List<Race> getRaces() { return races; }
        public Race getRaceAt(int index) { return races[index]; }

        public static bool operator ==(League leagueA, League leagueB)
        {
            bool isEqual = true;
            if (leagueA.getRaces().Count != leagueB.getRaces().Count)
                return false;

            for(int i = 0; i < leagueA.getRaces().Count; i++)
            {
                if (leagueA.getRaceAt(i) != leagueB.getRaceAt(i))
                    isEqual = false;
            }

            return isEqual;
        }

        public static bool operator !=(League leagueA, League leagueB)
        {
            bool isEqual = false;
            if (leagueA.getRaces().Count != leagueB.getRaces().Count)
                return true;
            
            for(int i = 0; i < leagueA.getRaces().Count; i++)
            {
                if (leagueA.getRaceAt(i) != leagueB.getRaceAt(i))
                    return true;
            }
            return isEqual;
        }
    }

    private void Start()
    {
        curLeague = allLeagues[0];
        curRace = curLeague.getRaceAt(0);
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

            if(needNewLeague)
            {
                for(int i = 0; i < allLeagues.Count; i++)
                {
                    if(curLeague == allLeagues[i])
                    {
                        if(i < allLeagues.Count - 1)
                        {
                            curLeague = allLeagues[i + 1];
                            curRace = curLeague.getRaceAt(0);
                        }
                    }
                }
            }
        }
    }
}
