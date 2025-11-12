using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ShopUI : UIController
{
    [SerializeField] UIGroup curUIGroup;
    [SerializeField] ShopType curShopType;
    public ShopType getCurShopType() { return curShopType; }
    public void setCurShopType(ShopType type)
    {
        switch (type)
        {
            case ShopType.FOOD:
                curShopType = ShopType.FOOD;
                setCurUIGroup("Food Items");
                break;
            case ShopType.COSMETIC:
                curShopType = ShopType.COSMETIC;
                setCurUIGroup("Cosmetic Items");
                break;
            case ShopType.PATTERN:
                curShopType = ShopType.PATTERN;
                setCurUIGroup("Pattern Items");
                break;
        }
        
    }
    public UIGroup getCurUIGroup() { return curUIGroup; }
    public void setCurUIGroup(string name)
    {
        foreach(UIGroup group in uiGroups)
        {
            if (group.getGroupName() == name)
            {
                curUIGroup = group;
                //Debug.Log("Found name: " + group.gameObject + " from " + gameObject);
            }
        }
        switch (getCurUIGroup().getGroupName())
        {
            case "Food Items":
                setUIGroup("Cosmetic Items", false);
                setUIGroup("Pattern Items", false);
                setUIGroup("Food Items", true);
                break;
            case "Cosmetic Items":
                setUIGroup("Food Items", false);
                setUIGroup("Pattern Items", false);
                setUIGroup("Cosmetic Items", true);
                break;
            case "Pattern Items":
                setUIGroup("Food Items", false);
                setUIGroup("Cosmetic Items", false);
                setUIGroup("Pattern Items", true);
                break;
        }

    }
    public void UpdateCoinUI(int numCoins)
    {
        UIGroup theGroup = getUIGroup("Shop");
        UIContainer theContainer = theGroup.getContainer("Coins2");
        theContainer.setTextElement("Coins2", numCoins.ToString());
    }

    public void UpdatePurchaseUI(List<ShopItem> items)
    {
        UIGroup theGroup = getCurUIGroup();
        for(int i = 0; i < 4; i++)
        {
            string ID = "Purchasable" + i.ToString();
            UIContainer theContainer = theGroup.getContainer(ID);

            theContainer.setTextElement("Name", items[i].getItemName());
            theContainer.setTextElement("Price", items[i].getCost().ToString());
        }
    }

    public void UpdateItemsUI(ShopType type, ShopItem item, int index)
    {
        string UIID = "Items";
        switch (type)
        {
            case ShopType.FOOD:
                UIID = "Food Items";
                break;
            case ShopType.COSMETIC:
                UIID = "Cosmetic Items";
                break;
            case ShopType.PATTERN:
                UIID = "Pattern Items";
                break;
        }

        UIGroup theGroup = getUIGroup(UIID);
        string ID = "Item" + (index + 1).ToString();
        UIContainer theContainer = theGroup.getContainer(ID);
        if (!item)
        {
            theContainer.gameObject.SetActive(false);
            return;
        }

        theContainer.gameObject.SetActive(true);
        theContainer.setTextElement("Name", item.getItemName());
        theContainer.setTextElement("Price", item.getCost().ToString());
        theContainer.setImageElement("Icon", item.getSprite());
    }

}
