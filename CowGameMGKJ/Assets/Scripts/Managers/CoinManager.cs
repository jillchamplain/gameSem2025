using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : Manager
{
    [Header("Refs")]
    [SerializeField] int numCoins;
    public int getCoins() { return numCoins; }
    public void addCoins(int addedCoins) { numCoins += addedCoins; }
    public void takeCoins(int takenCoins) { numCoins -= takenCoins; }

    public void InitCoins(GameData theData)
    {
        numCoins = theData.numCoins;
    }

}
