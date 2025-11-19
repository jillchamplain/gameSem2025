using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CosmeticManager : Manager
{
    //List of cosmetic items that should be unlocked when initted
    //


    [SerializeField] List<CosmeticItem> cosmeticItems;
    public List<CosmeticItem> getCosmeticItems() { return cosmeticItems; }
    public CosmeticItem getCosmeticItemAt(int index)
    {
        for(int i = 0; i < cosmeticItems.Count; i++)
        {
            if(i == index)
            {
                return cosmeticItems[i];
            }
        }
        return null;
    }

    [SerializeField] List<CosmeticItem> unlockedCosmeticItems;
    public List<CosmeticItem> getUnlockedCosmeticItems() { return unlockedCosmeticItems; }
    public CosmeticItem getUnlockedCosmeticAt(int index)
    {
        for(int i = 0; i < unlockedCosmeticItems.Count; i++)
        {
            if (i == index)
                return unlockedCosmeticItems[i];
        }
        return null;
    }
    public void unlockCosmetic(ShopItem item)
    {
        for(int i = 0; i < cosmeticItems.Count; i++)
        {
            if (cosmeticItems[i] == item)
                unlockedCosmeticItems.Add(cosmeticItems[i]);
        }
    }
    [SerializeField] List<CosmeticItem> spawnCosmeticItems;
    public List<CosmeticItem> getSpawnCosmeticItems() { return spawnCosmeticItems; }

    public void InitCosmeticItems(GameData theData)
    {

    }

    //public ShopItem getCosmeticItemAt(int index)
    //{
        
    //}
}
