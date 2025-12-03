using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CosmeticManager : Manager
{
    //List of cosmetic items that should be unlocked when initted
    //
    [SerializeField] public GameObject cosmeticPrefab;

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
    public CosmeticItem getCosmeticItemWithName(string name)
    {
        for(int i = 0; i < cosmeticItems.Count; i++)
        {
            if (cosmeticItems[i].getItemName() == name)
                return cosmeticItems[i];
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

    [SerializeField] List<Vector3> curCosmeticPos;
    public List<Vector3> getCurCosmeticPos() { return curCosmeticPos; }
    public Vector3 getCurCosmeticPosAt(int index)
    {
        for(int i = 0; i < curCosmeticPos.Count; i++)
        {
            if (i == index)
                return curCosmeticPos[i];
        }
        return Vector3.zero;
    }

    public void InitCosmeticItems(GameData theData)
    {
        unlockedCosmeticItems.Clear();
        purchasedCosmeticItems.Clear();
        currentCosmeticItems.Clear();
        curCosmeticPos.Clear();
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
        SpawnCosmetics(theData);
        
    }

    public void PurchaseCosmetic(ShopItem item, Vector3 pos)
    {
        foreach(CosmeticItem cos in cosmeticItems)
        {
            if(cos.getItemName() == item.getItemName())
            {
                purchasedCosmeticItems.Add(cos);
                curCosmeticPos.Add(pos);
                //get position
            }
        }
    }

    public void SpawnCosmetics(GameData theData)
    {
        for(int i = 0; i < purchasedCosmeticItems.Count; i++)
        {
            SpawnCosmetic(getCosmeticItemWithName(theData.purchasedCosmeticNames[i]), new Vector3(theData.purchasedCosmeticPosX[i], theData.purchasedCosmeticPosY[i], theData.purchasedCosmeticPosZ[i]));
        }
    }

    public void SpawnCosmetic(CosmeticItem item, Vector3 pos)
    {
        Debug.Log("Should spawn " + name + " at " + pos);
        Vector2 spawn = pos; //MOVE THIS 
        GameObject newCos = Instantiate(cosmeticPrefab, spawn, Quaternion.identity); //Disable collision with cows until pickup?
        newCos.transform.parent = this.transform;

        //Assign data
        Cosmetic newCosData = newCos.GetComponent<Cosmetic>();
        newCosData.setName(item.getItemName());
        newCosData.setType(item.getCosmeticType());
        newCosData.setTraitType(item.getTraitType());
        newCosData.setSprite(item.getSprite());
        
        currentCosmeticItems.Add(newCos);
        curCosmeticPos.Add(newCos.transform.position);


    }

    //public ShopItem getCosmeticItemAt(int index)
    //{

    //}
}
