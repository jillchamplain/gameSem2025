using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Manager
{
    [SerializeField] List<ShopItem> items;
    public List<ShopItem> getItems() { return items; }
    public ShopItem getItemAt(int index) //Use index passed from UI Buttons
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i == index)
                return items[i];
        }
        return null;
    }

    public ShopItem getItem(string ID)
    {
        foreach (ShopItem item in items)
        {
            if (item.getItemName() == ID)
                return item;
        }
        return null;
    }
    [SerializeField] int numCoins;
    public int getCoins() { return numCoins; }
    public void addCoins(int addedCoins) { numCoins += addedCoins; }
    public void takeCoins(int takenCoins) { numCoins -= takenCoins; }

    public void InitCoins(GameData theData)
    {
        numCoins = theData.numCoins;
    }
   
}
