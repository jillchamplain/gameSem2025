using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LeagueData", menuName = "ScriptableObjects/LeagueSO", order = 1)] 
public class League : ScriptableObject
{
    [SerializeField] List<Race> races;
    public List<Race> getRaces() { return races; }
    public Race getRaceAt(int index) { return races[index]; }

    [SerializeField] RaceReward clearReward;
    public RaceReward getClearReward() { return clearReward; }

}
