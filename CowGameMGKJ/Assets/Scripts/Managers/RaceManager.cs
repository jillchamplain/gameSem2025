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
    public Race getCurRace() { return curRace; }
    [SerializeField] int curRaceIndex = 0; //index of cur race in league
    public int getCurRaceIndex() { return curRaceIndex; }
    [SerializeField] League curLeague;
    [SerializeField] int curLeagueIndex = 0; //index of cur league in league list
    public int getCurLeagueIndex() { return curLeagueIndex; }
    [SerializeField] List<League> allLeagues;
    public League getLeague() { return curLeague; }
    public League getLeagueAt(int index)
    {
        for(int i  = 0; i < allLeagues.Count; i++)
        {
            if (i == index)
            {
                return allLeagues[i];
            }
        }
        return null;
    }

    //EVENTS
    public delegate void LeagueClear(RaceReward leagueReward);
    public static event LeagueClear leagueClear;

    private void Start()
    {
        curLeague = allLeagues[0];
        curRace = curLeague.getRaceAt(0);
    }

    public void InitRaces(GameData theData)
    {
      
        //Find cur league
        for(int i = 0; i < allLeagues.Count; i++)
        {
            if(i == theData.curLeagueID)
            {
                curLeagueIndex = i;
                curLeague = allLeagues[i];
            }
        }


        //Find cur race
        for(int i = 0; i < curLeague.getRaces().Count; i++)
        {
            if(i == theData.curRaceID)
            {
                curRaceIndex = i;
                curRace = curLeague.getRaceAt(i);
            }
        }
    }

    public RaceReward RaceCow(Cow theCow)
    {
        int tempPower = theCow.getPower();

        //Checking if traits are shared at all
        //If trait is shared increase cowPower by 5%
        for(int i = 0; i < theCow.getTraits().Count; i++)
        {
            if(theCow.getTraitAt(i) == curRace.getTraitAt(i))
            {
                //Debug.Log("Shared trait! " + theCow.getTraitAt(i));
                tempPower = (int)(tempPower * 1.05f);
                //Debug.Log("Increasing power to " + tempPower);
            }
        }

        //Debug.Log("Cow's power is: " + tempPower);

        if(tempPower < curRace.getPower())
        {
            Debug.Log("Not enough power! Power needed is: " + curRace.getPower());
            return RaceReward.NOWIN;
            
        }
        else
        {
            Debug.Log("You win the race! Power needed was: " + curRace.getPower());
            
            bool needNewLeague = true;
            for(int i = 0; i < curLeague.getRaces().Count; i++)
            {
                if(curRace == curLeague.getRaceAt(i))
                {
                    if (i < curLeague.getRaces().Count - 1)
                    {
                        curRace = curLeague.getRaceAt(i + 1);
                        curRaceIndex = i + 1;
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
                        //Debug.Log("need new league");
                        if (i < allLeagues.Count - 1)
                        {
                            curLeague = allLeagues[i + 1];
                            curLeagueIndex = i;
                            curRaceIndex = 0;
                            curRace = curLeague.getRaceAt(0);
                            needNewLeague = false;
                            i++; //Prevents from skipping ahead and reattributing curRace and curLeague
                        }
                    }
                }
                Debug.Log("called event");
                leagueClear?.Invoke(curLeague.getClearReward());
                
            }
            return curRace.getClearReward();
        }
    }
}
