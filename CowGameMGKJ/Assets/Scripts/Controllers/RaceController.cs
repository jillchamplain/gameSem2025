using System.Collections.Generic;
using UnityEngine;
using static Cow;

public class RaceController : LogicController
{
    public static RaceController inst;

    [Header("Refs")]
    [SerializeField] RaceUI raceUI;
    [SerializeField] MouseManager playerMouse;
    [SerializeField] CowManager cowManager;
    [SerializeField] RaceManager raceManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] FoodManager foodManager; //Need this for unlocking foods
    [SerializeField] SpawnManager spawnManager;//Separate visuals eventually
    [SerializeField] ShopManager shopManager;
    [SerializeField] CosmeticManager cosmeticManager;
    bool shouldEquipCows = false;
    private void Awake()
    {
        setGameState(GameState.RACE);
    }

    private void Start()
    {
        if (inst == null)
            inst = this;
    }

    private void Update()
    {
        if (shouldEquipCows)
        {
            shouldEquipCows = false;
            EquipCows();
        }
    }

    private void OnEnable()
    {
        MouseManager.mouseClick += OnMouseProcess;
        RaceManager.leagueClear += OnClearLeague;
        RaceManager.allLeagueClear += OnClearLeagueAll;

        UIEventController.raceCowPrompt += OnCowRacePrompt;
        UIEventController.noRaceCow += OnNoCowRace;
        UIEventController.raceCow += OnCowRace;
        UIEventController.popUpOff += OnPopUpOff;

    }

    private void OnDisable()
    {
        MouseManager.mouseClick -= OnMouseProcess;
        RaceManager.leagueClear -= OnClearLeague;
        RaceManager.allLeagueClear -= OnClearLeagueAll;

        UIEventController.raceCowPrompt -= OnCowRacePrompt;
        UIEventController.noRaceCow -= OnNoCowRace;
        UIEventController.raceCow -= OnCowRace;
        UIEventController.popUpOff -= OnPopUpOff;
    }

    public override void Reset()
    {
        if (getListening())
            return;
        raceUI.setUIGroup("Passive", false);
        if (playerMouse.getCurCow())
        {
            playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
            playerMouse.setCurCow(null);
        }
        cowManager.ClearCows();
        
    }

    public override void Init()
    {
        if (SaveSystem.LoadGameData() == null)
        {
            Debug.Log("file does not exist");
            SaveSystem.ResetGameData();
        }

        InitCows();
        InitFood();
        InitRaces();
        InitCoins();
        InitUI();
        InitCosmetics();
        //Debug.Log("init home");
        shouldEquipCows = true;
    }

    private void InitCosmetics()
    {

    }
    private void InitCows()
    {
        //Debug.Log("initting from home");
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

        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            //uiManager.UpdateCowUI(cowManager.getCowAt(i));
            raceUI.UpdateCowUI(cowManager.getCowAt(i).gameObject, i);
        }

    }

    public void SaveData()
    {
        SaveSystem.SaveGameData(cowManager, foodManager, shopManager, raceManager, cosmeticManager);
    }

    private void UpdateUI()
    {
        if (!getListening())
            return;

        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            raceUI.UpdateCowUI(cowManager.getCowAt(i).gameObject, i);
        }
        // uiManager.UpdateUIGroup("Cow", cowManager.getCows());
        raceUI.UICleanUp();
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
       

      

        //Click COW
        if (theObject.CompareTag("Cow"))
        {
            //Deselect
            if (playerMouse.getCurCow() == theObject)
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                playerMouse.setCurCow(null);
                raceUI.setUIGroup("Passive", false);

            }

            else
            {
                if (playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                    playerMouse.setCurCow(null);
                }
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelected(true);
                raceUI.setUIGroup("Passive", true);
            }
        }
        else
        {
            //Debug.Log("selected " + theObject.name);
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
        }
    }
    private void OnMouseHold(GameObject theObject)
    {
        if (!getListening())
            return;

        //Debug.Log("Holding");

       

        //Click COW
        if (theObject.CompareTag("Cow"))
        {
            List<GameObject> theCos = cosmeticManager.getCurrentCosmeticItems();
            if (playerMouse.getCurMouseState() == MouseState.HOLD)
            {
                playerMouse.getCurCow().GetComponent<Cow>().PlayAnimation(CowAnims.HOLD);
                for (int i = 0; i < theCos.Count; i++)
                {
                    Physics2D.IgnoreCollision(playerMouse.getCurCow().GetComponent<BoxCollider2D>(), theCos[i].GetComponent<BoxCollider2D>(), true);
                }
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

        //Release FOOD
        if (playerMouse.getCurCosmetic())
        {
            List<GameObject> theCows = cowManager.getCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurCosmetic().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
            }
            playerMouse.setCurCosmetic(null);
            SaveData();
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
            SaveData();
        }

        //Release COW
        if (playerMouse.getCurCow())
        {
            Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
            theCow.PlayAnimation(CowAnims.IDLE);
            //homeUI.setUIGroup("Train", false);
            //homeUI.setUIGroup("Retire", false);
            SaveData();
        }
    }
    private void OnMouseClick(GameObject theObject, ClickType type)
    {
        if (!getListening())
            return;

        if (type == ClickType.RIGHT)
            return;
        else if (theObject.CompareTag("Cow"))
        {
            if (playerMouse.getCurCow() == theObject)
            {
                playerMouse.setCurCow(null);
                theObject.GetComponent<Cow>().setSelected(false);
                raceUI.setUIGroup("Passive", false);
            }
            else
            {
                if (playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                }
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelected(true);
                raceUI.setUIGroup("Passive", true);
            }
        }
    }

    private void OnPopUpOff()
    {
        raceUI.setUIGroup("Pop Up", false);
    }

    private void OnCowRacePrompt()
    {
        raceUI.setUIGroup("Active", true);
        //Debug.Log("cow race");
        raceUI.UpdateRaceUI(playerMouse.getCurCow().GetComponent<Cow>(), raceManager.getCurRace());
        raceUI.UICleanUp();
        //uiManager.UpdateRaceUI(playerMouse.getCurCow().GetComponent<Cow>(), raceManager.getCurRace());
    }

    private void OnNoCowRace()
    {
        raceUI.setUIGroup("Active", false);
    }

    private void OnCowRace()
    {
        Race theRace = raceManager.getCurRace();
        RaceReward theWin = raceManager.RaceCow(playerMouse.getCurCow().GetComponent<Cow>());
        Debug.Log("Raced:" + theWin);
        
        if (theWin != RaceReward.NOWIN)
        {
            switch (theWin)
            {
                case RaceReward.NONE:
                    //uiManager.SetUIGroup("PopUp", true);
                    //uiManager.MakePopUp("You won the race!");
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won the race!");
                    raceUI.UICleanUp();
                    break;
                case RaceReward.FOOD:
                    //foodManager.UnlockFood(); FIX THIS
                    foodManager.UnlockFood(theRace.getRewardItem());
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won! Got new food");
                    raceUI.UICleanUp();
                    break;
                case RaceReward.PATTERN:
                    cowManager.UnlockPattern(theRace.getRewardItem());
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won! Got new pattern");
                    raceUI.UICleanUp();
                    break;
                case RaceReward.COIN:


                    //Calculation for coins granted
                    int coinAmount = cowManager.curGeneration * 100;
                    shopManager.addCoins(coinAmount);
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won! Got " + coinAmount + " coins");
                    raceUI.UICleanUp();
                    break;
            }
            SaveData();
            OnCowRacePrompt();
        }
        else
        {
            //uiManager.MakePopUp("You lost the race");
            Debug.Log("race results");
            raceUI.setUIGroup("Active", false);
            raceUI.setUIGroup("Pop Up", true);
            raceUI.UpdatePopUpUI("You lost the race");
            raceUI.UICleanUp();
            SaveData();
        }
    }

    private void EquipCows()
    {
        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            Cow theCow = cowManager.getCowAt(i);
            if (theCow.GetComponent<Cow>().getHat() != null)
                theCow.Equip(cosmeticManager.getCosmeticItemWithName(theCow.GetComponent<Cow>().getHat()));
            if (theCow.GetComponent<Cow>().getTop() != null)
                theCow.Equip(cosmeticManager.getCosmeticItemWithName(theCow.GetComponent<Cow>().getTop()));
            if (theCow.GetComponent<Cow>().getBot() != null)
                theCow.Equip(cosmeticManager.getCosmeticItemWithName(theCow.GetComponent<Cow>().getBot()));
        }
    }

    private void OnClearLeague(RaceReward clearReward)
    {
        Debug.Log("league clear");
        switch (clearReward)
        {
            case RaceReward.NONE:
                break;
            case RaceReward.FOOD:
                //foodManager.UnlockFood(); FIX THIS
                
                
                Debug.Log(playerMouse.getCurCow());
                //particleManager.SpawnTextParticleAt("Power Increase", "Unlocked New Food!", playerMouse.getCurCow().gameObject.transform.position);
                break;
            case RaceReward.PATTERN:
                //particleManager.SpawnTextParticleAt("Power Increase", "Unlocked New Pattern!", playerMouse.getCurCow().gameObject.transform.position);
                break;
        }

    }

    private void OnClearLeagueAll()
    {
        Debug.Log("Won all leagues");
    }
}
