using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ShopController : LogicController
{
    public static ShopController inst;
    [Header("Refs")]
    [SerializeField] MouseManager playerMouse;
    [SerializeField] ShopManager shopManager;
    [SerializeField] FoodManager foodManager;
    [SerializeField] RaceManager raceManager;
    [SerializeField] ShopUI shopUI;
    // Start is called before the first frame update
    private void Awake()
    {
        setGameState(GameState.SHOP);
    }
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UIEventController.addCoin += OnAddCoins;
        UIEventController.takeCoin += OnPurchase;
    }

    private void OnDisable()
    {
        UIEventController.addCoin -= OnAddCoins;
        UIEventController.takeCoin -= OnPurchase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Reset()
    {
        if (getListening())
            return;
    }

    public override void Init()
    {
        if (SaveSystem.LoadGameData() == null)
        {
            Debug.Log("file does not exist");
            SaveSystem.ResetGameData();
        }
        InitCoins();
        InitUI();
    }

    public void SaveData()
    {
        SaveSystem.SaveGameData(foodManager, shopManager, raceManager);
    }

    public void InitCoins()
    {
        shopManager.InitCoins(SaveSystem.LoadGameData());
    }

    public void InitUI()
    {
        shopUI.setUIGroup("Shop", true);
        shopUI.setUIGroup("Items", true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        shopUI.UpdateCoinUI(shopManager.getCoins());
        for (int i = 0; i < 4; i++)
        {
            shopUI.UpdatePurchaseUI(shopManager.getItemAt(i), i);
        }
    }

    void OnPurchase(int index)
    {
        //Read from ShopManager
        ShopItem theItem = shopManager.getItemAt(index);
        shopManager.getItems().Remove(theItem);
        UpdateUI();
        ShopType theType = theItem.getType();
        switch (theType)
        {
            case ShopType.FOOD:
                //Unlock Food > Food Manager
                break;
            case ShopType.PATTERN:
                //Unlock Pattern > Cow Manager
                break;
            case ShopType.COSMETIC:
                //Figure this out
                break;
        }


        shopManager.takeCoins(theItem.getCost());
        shopUI.UpdateCoinUI(shopManager.getCoins());

        
        SaveData();
    }



    void OnAddCoins(int amount)
    {
        shopManager.addCoins(amount);
        shopUI.UpdateCoinUI(shopManager.getCoins());
        SaveData();
    }
}
