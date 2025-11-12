using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Manager
{
    [SerializeField] List<ShopItem> foodItems;
    public List<ShopItem> getFoodItems() { return foodItems; }
    public ShopItem getFoodItemAt(int index) //Use index passed from UI Buttons
    {
        for (int i = 0; i < foodItems.Count; i++)
        {
            if (i == index)
                return foodItems[i];
        }
        return null;
    }
    public ShopItem getFoodItem(string ID)
    {
        foreach (ShopItem item in foodItems)
        {
            if (item.getItemName() == ID)
                return item;
        }
        return null;
    }

    [SerializeField] List<ShopItem> cosmeticItems;
    public List<ShopItem> getCosmeticItems() { return cosmeticItems; }
    public ShopItem getCosmeticItemAt(int index) //Use index passed from UI Buttons
    {
        for (int i = 0; i < cosmeticItems.Count; i++)
        {
            if (i == index)
                return cosmeticItems[i];
        }
        return null;
    }
    public ShopItem getCosmeticItem(string ID)
    {
        foreach (ShopItem item in cosmeticItems)
        {
            if (item.getItemName() == ID)
                return item;
        }
        return null;
    }

    [SerializeField] List<ShopItem> patternItems;
    public List<ShopItem> getPatternItems() { return patternItems; }
    public ShopItem getPatternItemAt(int index) //Use index passed from UI Buttons
    {
        for (int i = 0; i < patternItems.Count; i++)
        {
            if (i == index)
                return patternItems[i];
        }
        return null;
    }
    public ShopItem getPatternItem(string ID)
    {
        foreach (ShopItem item in patternItems)
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
