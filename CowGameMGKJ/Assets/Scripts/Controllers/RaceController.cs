using UnityEngine;

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
    [SerializeField] CoinManager coinManager;
    private void Awake()
    {
        setGameState(GameState.RACE);
    }

    private void Start()
    {
        if (inst == null)
            inst = this;
    }

    private void OnEnable()
    {
        MouseManager.mouseClick += OnMouseClick;
        RaceManager.leagueClear += OnClearLeague;

        UIEventController.raceCowPrompt += OnCowRacePrompt;
        UIEventController.noRaceCow += OnNoCowRace;
        UIEventController.raceCow += OnCowRace;
        UIEventController.popUpOff += OnPopUpOff;

    }

    private void OnDisable()
    {
        MouseManager.mouseClick -= OnMouseClick;
        RaceManager.leagueClear -= OnClearLeague;

        UIEventController.raceCowPrompt -= OnCowRacePrompt;
        UIEventController.noRaceCow -= OnNoCowRace;
        UIEventController.raceCow -= OnCowRace;
        UIEventController.popUpOff -= OnPopUpOff;
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
        //Debug.Log("init home");
    }
    private void InitCows()
    {
        //Debug.Log("initting from home");
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.InitCurCows(SaveSystem.LoadGameData());
    }

    private void InitFood()
    {
        foodManager.setCurFoods(false);
        foodManager.InitFood(SaveSystem.LoadGameData());
    }

    private void InitRaces()
    {
        raceManager.InitRaces(SaveSystem.LoadGameData());
    }
    
    private void InitCoins()
    {
        coinManager.InitCoins(SaveSystem.LoadGameData());
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
        SaveSystem.SaveGameData(cowManager, foodManager, coinManager, raceManager);
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

    private void OnMouseClick(GameObject theObject)
    {
        if (!getListening())
            return;


        else if (theObject.CompareTag("Cow"))
        {
            if (playerMouse.getCurCow() == theObject)
            {
                playerMouse.setCurCow(null);
                theObject.GetComponent<Cow>().setSelected(false);
            }
            else
            {
                if (playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                }
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelected(true);
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
        Debug.Log("cow race");
        raceUI.UpdateRaceUI(playerMouse.getCurCow().GetComponent<Cow>(), raceManager.getCurRace());
        raceUI.UICleanUp();
        //uiManager.UpdateRaceUI(playerMouse.getCurCow().GetComponent<Cow>(), raceManager.getCurRace());
    }

    private void OnNoCowRace()
    {
        raceUI.setUIGroup("Active", false);
        Debug.Log("toggling off race");
    }

    private void OnCowRace()
    {
        RaceReward theWin = raceManager.RaceCow(playerMouse.getCurCow().GetComponent<Cow>());
        
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
                    foodManager.UnlockFood();
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won! Got new food");
                    raceUI.UICleanUp();
                    break;
                case RaceReward.PATTERN:
                    raceUI.setUIGroup("Active", false);
                    raceUI.setUIGroup("Pop Up", true);
                    raceUI.UpdatePopUpUI("You won! Got new pattern");
                    raceUI.UICleanUp();
                    break;
                case RaceReward.COIN:


                    //Calculation for coins granted
                    int coinAmount = cowManager.curGeneration * 100;
                    coinManager.addCoins(coinAmount);
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

    private void OnClearLeague(RaceReward clearReward)
    {
       
        switch (clearReward)
        {
            case RaceReward.NONE:
                break;
            case RaceReward.FOOD:
                foodManager.UnlockFood();
                Debug.Log(playerMouse.getCurCow());
                //particleManager.SpawnTextParticleAt("Power Increase", "Unlocked New Food!", playerMouse.getCurCow().gameObject.transform.position);
                break;
            case RaceReward.PATTERN:
                //particleManager.SpawnTextParticleAt("Power Increase", "Unlocked New Pattern!", playerMouse.getCurCow().gameObject.transform.position);
                break;
        }

    }
}
