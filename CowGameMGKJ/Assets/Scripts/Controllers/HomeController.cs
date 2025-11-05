using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using static Cow;
using static CowManager;
using static MouseManager;

public class HomeController : LogicController
{
    public static HomeController inst;
    [Header("Refs")]
    [SerializeField] HomeUI homeUI;
    [SerializeField] MouseManager playerMouse;
    [SerializeField] CowManager cowManager;
    [SerializeField] FoodManager foodManager;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] TrainManager trainManager;
    [SerializeField] ShopManager shopManager;
    [SerializeField] RaceManager raceManager;
    private void Awake()
    {
        setGameState(GameState.HOME);
    }
    private void Start()
    {
        if (inst == null)
            inst = this;


    }
    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;
        Cow.cowMaxLevel += OnCowMaxLevel;
        Cow.cowLevelUp += OnCowLevelUp;
        UIEventController.retireCow += OnCowRetire;
        UIEventController.trainCow += OnCowTrain;
        UIEventController.renameCow += OnCowRename;
        CowManager.cowSpawned += OnCowSpawned;


        FoodManager.foodSpawn += OnFoodSpawned;

        MouseManager.mouseClick += OnMouseClick;
        MouseManager.mouseRelease += OnMouseRelease;

       
        UIEventController.popUpOff += OnPopUpOff;
        UIEventController.renameCowOff += OnRenameOff;
    }
    private void OnDisable()
    {
        //Debug.Log("Disabling " + this.gameObject);
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;
        Cow.cowLevelUp -= OnCowLevelUp;
        UIEventController.retireCow -= OnCowRetire;
        UIEventController.trainCow -= OnCowTrain;
        UIEventController.renameCow -= OnCowRename;
        CowManager.cowSpawned -= OnCowSpawned;

        FoodManager.foodSpawn -= OnFoodSpawned;

        MouseManager.mouseClick -= OnMouseClick;
        MouseManager.mouseRelease -= OnMouseRelease;

        UIEventController.popUpOff -= OnPopUpOff;
        UIEventController.renameCowOff -= OnRenameOff;
    }
    public override void Reset()
    {
        if (getListening())
            return;
    
        if (playerMouse.getCurCow())
        {
            playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
            playerMouse.setCurCow(null);
        }
        playerMouse.setCurFood(null);
        
        foodManager.setCurFoods(false); //Need to start calculating for time elapsed
        cowManager.ClearCows();
        //Debug.Log("reset home");
    }

    public override void Init()
    {
        if(SaveSystem.LoadGameData() == null)
        {
            Debug.Log("file does not exist");
            SaveSystem.ResetGameData();
        }

        InitCows();
        InitFood();
        InitRaces();
        InitCoins();
        InitUI();
        foodManager.setCurFoods(true);
        //Debug.Log("init home");
    }
    private void InitCows()
    {
        //Debug.Log("initting from home");
        cowManager.InitPatterns(SaveSystem.LoadGameData());
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.DefaultSpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.InitCurCows(SaveSystem.LoadGameData());
    }

    private void InitFood()
    {
        foodManager.InitFood(SaveSystem.LoadGameData());
    }

    private void InitRaces()
    {
        raceManager.InitRaces(SaveSystem.LoadGameData());
    }
    private void InitCoins()
    {
        shopManager.InitCoins(SaveSystem.LoadGameData());
    }

    private void InitUI()
    {
        //Debug.Log("UI initting");
        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            // uiManager.UpdateCowUI(cowManager.getCowAt(i));
            homeUI.UpdateCowUI(cowManager.getCowAt(i).gameObject, i);
            //Debug.Log("updating ui");
        }
        homeUI.setUIGroup("Retire", false);
        homeUI.setUIGroup("Train", false);
    }

    private void UpdateUI()
    {
        if (!getListening())
        {
            return;
        }

        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            homeUI.UpdateCowUI(cowManager.getCowAt(i).gameObject, i);

        }
        homeUI.UICleanUp();
    }

    private void SaveData()
    {
        if (!getListening())
            return;
        SaveSystem.SaveGameData(cowManager, foodManager, shopManager, raceManager);
    }

    private void OnPopUpOff()
    {
        if (!getListening())
            return;
        homeUI.setUIGroup("Pop Up", false);
    }

    private void OnRenameOff()
    {
        if (!getListening())
            return;
        homeUI.setUIGroup("Rename", false);
    }

    private void OnCowSpawned(Cow theCow)
    {
        if (!getListening())
            return;


        UpdateUI();
        
    }

    private void OnCowRename(TextMeshProUGUI tf)
    {
        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        theCow.setName(tf.text);
        homeUI.setUIGroup("Rename", false);

        UpdateUI();
        SaveData();
    }

    private void OnCowEat(Cow theCow, Food theFood)
    {
        if (!getListening())
            return;

        playerMouse.setCurCow(theCow.gameObject); //Store cow

       

       
        string powerIncreaseText = "+ " + theFood.getPower();

        if(!(theCow.getPower() >= theCow.getMaxPower())) //If Below Max Power > Feed and Increase Power
        {
            theCow.setPower(theCow.getPower() + theFood.getPower()); //Increase cow power

            theCow.PlayAnimation(Cow.CowAnims.FEED); ///MOVE COW VISUALS TO DIFF CLASS
            particleManager.SpawnTextParticleAt("Power Increase", powerIncreaseText, theCow.gameObject.transform.position);
            homeUI.UpdateCowUI(theCow.gameObject, cowManager.getCowIndex(theCow));
        }
        else // If Max Power > Generate coins
        {
            theCow.PlayAnimation(Cow.CowAnims.FEED);
            //Generate coins
            int foodPower = theFood.getPower() / 100;
            int coinAmount = ((int)Mathf.Ceil(cowManager.curGeneration * (float)1.5)) * foodPower;
            string coinText = "+" + coinAmount.ToString() + " coins!";
            particleManager.SpawnTextParticleAt("Power Increase", coinText, theCow.gameObject.transform.position);
            shopManager.addCoins(coinAmount);
        }
        foodManager.DeleteFood(theFood.gameObject);
        homeUI.setUIGroup("Train", false);
        homeUI.UICleanUp();
        SaveData();
    }

   public void OnCowTrain()
    {
        if (!getListening())
            return;

        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        TrainManager.TrainRegimen theRegimen = trainManager.SelectRandomTraining();
        int increase = trainManager.RollTrainingSuccess(theRegimen);
        theCow.setPower(theCow.getPower() + increase);
        SaveData();


        if (theCow.getPower() >= theCow.getMaxPower())
        {
            homeUI.setUIGroup("Train", false);
            homeUI.setUIGroup("Retire", true);
            return;
        }
        theCow.PlayAnimation(Cow.CowAnims.FEED);
        string powerIncreaseText = "+ " + increase;
        particleManager.SpawnTextParticleAt("Power Increase", powerIncreaseText, theCow.gameObject.transform.position);
        homeUI.UpdateCowUI(theCow.gameObject, cowManager.getCowIndex(theCow));


        homeUI.UICleanUp();
        SaveData();
    }

    private void OnCowLevelUp(Cow theCow)
    {
        if (!getListening())
            return;

        if (theCow.getMaxPower() == theCow.getPower())
            return;
        Vector3 spawn = new Vector3(theCow.transform.position.x, theCow.transform.position.y + 0.75f, 0);
        particleManager.SpawnTextParticleAt("Power Increase", "Level Up!", spawn);
        SaveData();
    }

    private void OnCowMaxLevel(Cow theCow)
    {
        if (!getListening())
            return;

        UpdateUI();
        homeUI.UICleanUp();
        //theCow.PlayAnimation(Cow.CowAnims.RETIRE); //Hook up so deleting waits for animationt to play
        SaveData();
    }

    private void OnCowRetire()
    {
        if (!getListening())
            return;

        if (!playerMouse.getCurCow().GetComponent<Cow>().getIsMaxLevel())
            return;
        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();

        homeUI.setUIGroup("Train", false);
        homeUI.setUIGroup("Retire", false);
        playerMouse.setCurCow(null);
       // uiManager.SetUIGroup("PopUp", true);
        //uiManager.MakePopUp(theCow.getName() + " retired!");
        cowManager.DeleteCow(theCow.gameObject);
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        SaveData();
       
    }

    private void OnFoodSpawned(FoodItem theFood)
    {
        if (!getListening())
            return;

        //Debug.Log("food spawned by " + this.gameObject);
        foodManager.SpawnFood(theFood, spawnManager.SelectRandomSpawn(foodManager.getFoodPrefab()));
    }

    private void OnMouseClick(GameObject theObject, ClickType type)
    {
        if (!getListening())
            return;

        if (theObject.CompareTag("Food"))
        {
            if (playerMouse.getCurCow())
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                playerMouse.setCurCow(null);
                homeUI.setUIGroup("Train", false);
                homeUI.setUIGroup("Retire", false);
            }

            playerMouse.setCurFood(theObject);
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        else if (theObject.CompareTag("Cow"))
        {
            if (type == ClickType.LEFT)
            {
                /*if (playerMouse.getCurCow() == theObject)
                {
                    playerMouse.setCurCow(null);
                    theObject.GetComponent<Cow>().setSelected(false);
                    homeUI.setUIGroup("Rename", false);
                    homeUI.setUIGroup("Train", false);
                    homeUI.setUIGroup("Retire", false);
                }
                else
                {*/
                    if (playerMouse.getCurCow())
                    {
                        playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                    }
                    playerMouse.setCurCow(theObject);
                    theObject.GetComponent<Cow>().setSelected(true);
                    if (theObject.GetComponent<Cow>().getIsMaxLevel())
                    {
                        homeUI.setUIGroup("Rename", false);
                        homeUI.setUIGroup("Train", false);
                        homeUI.setUIGroup("Retire", true);
                    }
                    else
                    {
                        homeUI.setUIGroup("Rename", false);
                        homeUI.setUIGroup("Retire", false);
                        homeUI.setUIGroup("Train", true);
                    }
                /*}*/
            }
            else if(type == ClickType.RIGHT)
            {
                
                if (playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                    playerMouse.setCurCow(theObject);
                    theObject.GetComponent<Cow>().setSelected(true);
                }
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelected(true);       
                homeUI.setUIGroup("Rename", true);
            }
        }
    }

    private void OnMouseRelease()
    {
        if (!getListening())
            return;

        if (!playerMouse.getCurFood())
            return;
        List<GameObject> theCows = cowManager.getCows();
        for (int i = 0; i < theCows.Count; i++)
        {
            Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
        }
        playerMouse.setCurFood(null);
    }

}
