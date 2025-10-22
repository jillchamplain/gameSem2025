using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : UIController
{
    public void UpdateCoinUI(int numCoins)
    {
        UIGroup theGroup = getUIGroup("Shop");
        UIContainer theContainer = theGroup.getContainer("Coins2");
        theContainer.setTextElement("Coins2", numCoins.ToString());
    }

    public void UpdatePurchaseUI(List<ShopItem> items)
    {
        UIGroup theGroup = getUIGroup("Items");
        for(int i = 0; i < 4; i++)
        {
            string ID = "Purchasable" + i.ToString();
            UIContainer theContainer = theGroup.getContainer(ID);

            theContainer.setTextElement("Name", items[i].getItemName());
            theContainer.setTextElement("Price", items[i].getCost().ToString());
        }
    }

    public void UpdatePurchaseUI(ShopItem item, int index)
    {
        UIGroup theGroup = getUIGroup("Items");
        string ID = "Item" + (index + 1).ToString();
        Debug.Log(ID);
        UIContainer theContainer = theGroup.getContainer(ID);
        Debug.Log(theContainer);
        theContainer.setTextElement("Name", item.getItemName());
        theContainer.setTextElement("Price", item.getCost().ToString());
    }

}
