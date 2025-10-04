using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaceData", menuName = "ScriptableObjects/RaceSO", order = 2)]
public class Race : ScriptableObject
{
    [SerializeField] int power;
    public int getPower() { return power; }
    [SerializeField] List<string> traits;
    public List<string> getTraits() { return traits; }
    public string getTraitAt(int index) { return traits[index]; }

    [SerializeField] RaceReward clearReward;
    public RaceReward getClearReward() { return clearReward; }
}
