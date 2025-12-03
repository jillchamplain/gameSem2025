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
    [SerializeField] CowManager cowManager;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] CosmeticManager cosmeticManager;
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
        UIEventController.foodTab += UpdateUIType;
        UIEventController.cosmeticsTab += UpdateUIType;
        UIEventController.patternsTab += UpdateUIType;

        UIEventController.purchaseFood += OnPurchaseFood;
        UIEventController.purchaseCosmetic += OnPurchaseCosmetic;
        UIEventController.purchasePattern += OnPurchasePattern;
    }

    private void OnDisable()
    {
        UIEventController.foodTab -= UpdateUIType;
        UIEventController.cosmeticsTab -= UpdateUIType;
        UIEventController.patternsTab -= UpdateUIType;
        UIEventController.addCoin -= OnAddCoins;

        UIEventController.purchaseFood -= OnPurchaseFood;
        UIEventController.purchaseCosmetic -= OnPurchaseCosmetic;
        UIEventController.purchasePattern -= OnPurchasePattern;
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
        InitCows();
        InitItems();
        InitUI();
    }

    public void SaveData()
    {
        SaveSystem.SaveGameData(cowManager, foodManager, shopManager, raceManager, cosmeticManager);
    }
    
    public void InitCows()
    {
        //Debug.Log("initting from home");
        cowManager.InitPatterns(SaveSystem.LoadGameData());
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.InitCurCows(SaveSystem.LoadGameData());
        cowManager.setCows(false); //Hides Cows;
    }

    public void InitItems()
    {
        shopManager.InitFoodItems(SaveSystem.LoadGameData(), foodManager);
        shopManager.InitCosmeticItems(SaveSystem.LoadGameData(), cosmeticManager);
        shopManager.InitPatternItems(SaveSystem.LoadGameData(), cowManager);
    }

    public void InitCoins()
    {
        shopManager.InitCoins(SaveSystem.LoadGameData());
    }

    public void InitUI()
    {
        shopUI.setUIGroup("Shop", true);
        UpdateUIType(ShopType.FOOD);
        UpdateUI();
    }

    public void UpdateUI()
    {
        shopUI.UpdateCoinUI(shopManager.getCoins());
        switch (shopUI.getCurShopType())
        {
           
            case ShopType.FOOD:
                for (int i = 0; i < 4; i++)
                {
                    shopUI.UpdateItemsUI(shopUI.getCurShopType(), shopManager.getFoodItemAt(i), i);
                    
                }
                break;
            case ShopType.COSMETIC:
                for (int i = 0; i < 4; i++)
                {
                    shopUI.UpdateItemsUI(shopUI.getCurShopType(), shopManager.getCosmeticItemAt(i), i);
                }
                break;
            case ShopType.PATTERN:
                for (int i = 0; i < 4; i++)
                {
                    Debug.Log("updating pattern");
                    shopUI.UpdateItemsUI(shopUI.getCurShopType(), shopManager.getPatternItemAt(i), i);
                }
                break;
        }

        /*for (int i = 0; i < 4; i++)
        {
            shopUI.UpdatePurchaseUI(shopManager.getFoodItemAt(i), i);
        }*/
    }

    public void UpdateUIType(ShopType type)
    {
        shopUI.setCurShopType(type);
        UpdateUI();
        
    }

    /*void OnPurchase(int index)
    {

        //Read from ShopManager
        ShopItem theItem;
        ShopType theType = theItem.getType();

        //Get Item Based on Type
        switch (theType)
        {
            case ShopType.FOOD:
                //Unlock Food > Food Manager
                theItem = shopManager.getFoodItemAt(index);
                foodManager.UnlockFood(theItem);
                break;
            case ShopType.PATTERN:
                //Unlock Pattern > Cow Manager
                break;
            case ShopType.COSMETIC:
                //Figure this out
                break;
        }

        if (shopManager.getCoins() - theItem.getCost() <= 0)
            return;

        shopManager.takeCoins(theItem.getCost());
        shopUI.UpdateCoinUI(shopManager.getCoins());

        //Unlock Item
        switch (theType)
        {
            case ShopType.FOOD:
                //Unlock Food > Food Manager
                theItem = shopManager.getFoodItemAt(index);
                foodManager.UnlockFood(theItem);
                break;
            case ShopType.PATTERN:
                //Unlock Pattern > Cow Manager
                break;
            case ShopType.COSMETIC:
                //Figure this out
                break;
        }



        shopManager.getFoodItems().Remove(theItem);
        UpdateUI();

           
        SaveData();
    }*/

    void OnPurchaseFood(int index)
    {
        ShopItem theItem = shopManager.getFoodItemAt(index);

        if (shopManager.getCoins() == 0)
            return;

        if (shopManager.getCoins() - theItem.getCost() < 0)
            return;

        shopManager.takeCoins(theItem.getCost());
        shopUI.UpdateCoinUI(shopManager.getCoins());

        //Unlock Food
        foodManager.PurchaseFood(theItem);

        shopManager.getFoodItems().Remove(theItem);
        UpdateUI();


        SaveData();

    }

    void OnPurchaseCosmetic(int index)
    {
        ShopItem theItem = shopManager.getCosmeticItemAt(index);

        if (shopManager.getCoins() == 0)
            return;

        if (shopManager.getCoins() - theItem.getCost() < 0)
            return;

        shopManager.takeCoins(theItem.getCost());
        shopUI.UpdateCoinUI(shopManager.getCoins());

        cosmeticManager.PurchaseCosmetic(theItem);

        //Spawn cosmetic item when returning home
        UpdateUI();


        SaveData();
    }

    void OnPurchasePattern(int index)
    {
        ShopItem theItem = shopManager.getPatternItemAt(index);

        if (shopManager.getCoins() == 0)
            return;

        if (shopManager.getCoins() - theItem.getCost() < 0)
            return;

        shopManager.takeCoins(theItem.getCost());
        shopUI.UpdateCoinUI(shopManager.getCoins());

        //Unlock Food
        cowManager.UnlockPattern(theItem);

        shopManager.getPatternItems().Remove(theItem);
        UpdateUI();


        SaveData();
    }
    void OnAddCoins(int amount)
    {
        shopManager.addCoins(amount);
        shopUI.UpdateCoinUI(shopManager.getCoins());
        SaveData();
    }
}
