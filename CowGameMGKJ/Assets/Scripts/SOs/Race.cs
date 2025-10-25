using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaceData", menuName = "ScriptableObjects/RaceSO", order = 2)]
public class Race : ScriptableObject
{
    [SerializeField] int power;
    public int getPower() { return power; }
    [SerializeField] List<Trait> traits;
    public List<Trait> getTraits() { return traits; }
    public Trait getTraitAt(int index) { return traits[index]; }

    [SerializeField] RaceReward clearReward;
    public RaceReward getRewardType() { return clearReward; }
    [SerializeField] ShopItem rewardItem;
    public ShopItem getRewardItem() { return rewardItem; }
}
