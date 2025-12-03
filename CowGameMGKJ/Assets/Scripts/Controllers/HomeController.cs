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
    [SerializeField] CosmeticManager cosmeticManager;
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
        Cow.cowEquip += OnCowEquip;
        UIEventController.retireCow += OnCowRetire;
        UIEventController.trainCow += OnCowTrain;
        UIEventController.renameCow += OnCowRename;
        CowManager.cowSpawned += OnCowSpawned;


        FoodManager.foodSpawn += OnFoodSpawned;

        MouseManager.mouseClick += OnMouseProcess;

       
        UIEventController.popUpOff += OnPopUpOff;
        UIEventController.renameCowOff += OnRenameOff;
    }
    private void OnDisable()
    {
        //Debug.Log("Disabling " + this.gameObject);
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;
        Cow.cowLevelUp -= OnCowLevelUp;
        Cow.cowEquip -= OnCowEquip;
        UIEventController.retireCow -= OnCowRetire;
        UIEventController.trainCow -= OnCowTrain;
        UIEventController.renameCow -= OnCowRename;
        CowManager.cowSpawned -= OnCowSpawned;

        FoodManager.foodSpawn -= OnFoodSpawned;

        MouseManager.mouseClick -= OnMouseProcess;

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
        homeUI.UpdateCoins(shopManager.getCoins());
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
        homeUI.UpdateCoins(shopManager.getCoins());
        Debug.Log("kdjfkdjfdkjfk");
    }

    private void SaveData()
    {
        if (!getListening())
            return;
        SaveSystem.SaveGameData(cowManager, foodManager, shopManager, raceManager, cosmeticManager);
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
            int coinAmount = ((int)Mathf.Ceil(cowManager.curGeneration * (float)2.5f)) * foodPower;
            string coinText = "+" + coinAmount.ToString() + " coins!";
            particleManager.SpawnTextParticleAt("Power Increase", coinText, theCow.gameObject.transform.position);
            shopManager.addCoins(coinAmount);
        }
        foodManager.DeleteFood(theFood.gameObject);
        homeUI.setUIGroup("Train", false);
        UpdateUI();
        homeUI.UICleanUp();
        SaveData();
        
    }

   public void OnCowTrain()
    {
        if (!getListening())
            return;

        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        TrainManager.TrainRegimen theRegimen = trainManager.SelectRandomTraining();
        int increase = trainManager.RollTrainingSuccess(theRegimen) * (cowManager.curGeneration);
        increase = (int)(increase * 0.1f);
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
    private void OnCowEquip(GameObject theCow, Cosmetic item)
    {
        theCow.GetComponent<Cow>().Equip(item);
        theCow.GetComponent<Cow>().setTraitAt((int)item.getType() - 1, item.getTraitType());
        //Destroy(item.gameObject);
        UpdateUI();
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

    private void OnMouseProcess(GameObject theObject, ClickType type)
    {
        if (!getListening())
            return;

        switch (type)
        {
            case ClickType.LEFT:
                OnMouseLeftClick(theObject);
                break;
            case ClickType.RIGHT:
                OnMouseRightClick(theObject);
                break;
            case ClickType.HOLD:
                OnMouseHold(theObject);
                break;
            case ClickType.RELEASE:
                OnMouseRelease(theObject);
                break;
            default:
                break;
        }

    }

    private void OnMouseLeftClick(GameObject theObject)
    {
        if (!getListening())
            return;
        playerMouse.setCurMouseState(MouseState.FREE);
        playerMouse.setIsHolding(false);
        StartCoroutine(playerMouse.HoldTimer());
        playerMouse.setIsHolding(true);
        //Click FOOD
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

            //Disable Food Collision
            playerMouse.setCurFood(theObject);
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        if(theObject.CompareTag("Cosmetic"))
        {
            if(playerMouse.getCurCow())
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                playerMouse.setCurCow(null);
                homeUI.setUIGroup("Train", false);
                homeUI.setUIGroup("Retire", false);
            }
            else if(playerMouse.getCurFood())
            {
                List<GameObject> theCows = cowManager.getCows();
                for (int i = 0; i < theCows.Count; i++)
                {
                    Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
                }
                playerMouse.setCurFood(null);
            }

            //Disable Food Collision
            playerMouse.setCurCosmetic(theObject);
            List<GameObject> theCows2 = cowManager.getCows();
            for (int i = 0; i < theCows2.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurCosmetic().GetComponent<BoxCollider2D>(), theCows2[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        //Click COW
        if (theObject.CompareTag("Cow"))
        {
            //Deselect
            if (playerMouse.getCurCow() == theObject)
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                playerMouse.setCurCow(null);
                homeUI.setUIGroup("Rename", false);
                homeUI.setUIGroup("Retire", false);
                homeUI.setUIGroup("Train", false);
            }

            else
            {
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
            }
        }
    }

    private void OnMouseRightClick(GameObject theObject)
    {
        if (!getListening())
            return;

        playerMouse.setCurMouseState(MouseState.FREE);

        //Click COW
        if (theObject.CompareTag("Cow"))
        {
            if (playerMouse.getCurCow()) //Deselect
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
            }
            playerMouse.setCurCow(theObject);
            theObject.GetComponent<Cow>().setSelected(true);
            homeUI.setUIGroup("Rename", true);
        }
    }

    private void OnMouseHold(GameObject theObject)
    {
        if (!getListening())
            return;

        Debug.Log("Holding");

        if(theObject.CompareTag("Cosmetic"))
        {
            //Disable Food Collision
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurCosmetic().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        //Click FOOD
        if (theObject.CompareTag("Food"))
        {
            //Disable Food Collision
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        //Click COW
        if (theObject.CompareTag("Cow"))
        {
            if(playerMouse.getCurMouseState() == MouseState.HOLD)
            {
                playerMouse.getCurCow().GetComponent<Cow>().PlayAnimation(CowAnims.HOLD);
            }
            //Disable Food Collision
           
        }
    }

    private void OnMouseRelease(GameObject theObject)
    {
        if (!getListening())
            return;

        playerMouse.setIsHolding(false);
        playerMouse.setCurMouseState(MouseState.FREE);
        if(playerMouse.getCurCow())
        {
            Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
            theCow.PlayAnimation(CowAnims.IDLE);
        }

        //Release FOOD
        if (playerMouse.getCurCosmetic())
        {
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurCosmetic().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
            }
            playerMouse.setCurCosmetic(null);
        }


        //Release FOOD
        if (playerMouse.getCurFood())
        {
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
            }
            playerMouse.setCurFood(null);
        }

        //Release COW
        if (playerMouse.getCurCow() && playerMouse.getCurMouseState() == MouseState.HOLD)
        {
            playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
            playerMouse.setCurCow(null);
            //homeUI.setUIGroup("Train", false);
            homeUI.setUIGroup("Retire", false);
        }
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
                //homeUI.setUIGroup("Train", false);
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
                        Debug.Log("deselecting cow");

                }
                else
                {
                    playerMouse.setCurCow(theObject);
                    theObject.GetComponent<Cow>().setSelected(true);
                    if (theObject.GetComponent<Cow>().getIsMaxLevel())
                    {
                        homeUI.setUIGroup("Rename", false);
                        //homeUI.setUIGroup("Train", false);
                        homeUI.setUIGroup("Retire", true);
                    }
                    else
                    {
                        homeUI.setUIGroup("Rename", false);
                        homeUI.setUIGroup("Retire", false);
                        //homeUI.setUIGroup("Train", true);
                    }
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

            else if(type == ClickType.RELEASE)
            {
                if(playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                }
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
