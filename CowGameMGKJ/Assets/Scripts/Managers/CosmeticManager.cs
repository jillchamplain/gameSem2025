using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CosmeticManager : Manager
{
    //List of cosmetic items that should be unlocked when initted
    //


    [SerializeField] List<CosmeticItem> cosmeticItems; //current cosmeti
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

    [SerializeField] List<GameObject> currentCosmeticItems;
    public List<GameObject> getCurrentCosmeticItems() { return currentCosmeticItems; }
    public GameObject getCurrentCosmeticItemAt(int index)
    {
        for(int i = 0; i < currentCosmeticItems.Count; i++)
        {
            if (i == index)
                return currentCosmeticItems[i];
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
    [SerializeField] List<CosmeticItem> purchasedCosmeticItems;
    public List<CosmeticItem> getPurchasedCosmeticItems() { return purchasedCosmeticItems; }

    public CosmeticItem getPurchasedCosmeticAt(int index)
    {
        for(int i = 0; i < purchasedCosmeticItems.Count; i++)
        {
            if(i == index)
            {
                return purchasedCosmeticItems[i];
            }
            
        }
        return null;
    }

    public void InitCosmeticItems(GameData theData)
    {
        unlockedCosmeticItems.Clear();
        purchasedCosmeticItems.Clear();
        foreach(CosmeticItem item in cosmeticItems)
        {
            for(int i = 0; i < theData.unlockedCosmeticNames.Length; i++)
            {
                if (theData.unlockedCosmeticNames[i] == item.getItemName())
                {
                    unlockedCosmeticItems.Add(item);
                }
            }

            for(int i = 0; i < theData.purchasedCosmeticNames.Length; i++)
            {
                if (theData.purchasedCosmeticNames[i] == item.getItemName())
                {
                    purchasedCosmeticItems.Add(item);
                }
            }
        }
        
    }

    public void PurchaseCosmetic(ShopItem item)
    {
        foreach(CosmeticItem cos in cosmeticItems)
        {
            if(cos.getItemName() == item.getItemName())
            {
                purchasedCosmeticItems.Add(cos);
            }
        }
    }

    public void SpawnCosmetic()
    {

    }

    //public ShopItem getCosmeticItemAt(int index)
    //{
        
    //}
}
