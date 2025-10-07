using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class RaceController : LogicController
{
    public static RaceController inst;

    [Header("Refs")]
    [SerializeField] MouseManager playerMouse;
    [SerializeField] UIManager uiManager; //Different UIs > Need to change
    [SerializeField] CowManager cowManager;
    [SerializeField] RaceManager raceManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] FoodManager foodManager; //Need this for unlocking foods
    [SerializeField] SpawnManager spawnManager;//Separate visuals eventually
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
        UpdateUI();
    }

    private void InitCows()
    {
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));

        cowManager.InitCurCows(SaveSystem.LoadGameData());
        foodManager.InitFood(SaveSystem.LoadGameData());
        raceManager.InitRaces(SaveSystem.LoadGameData());
        InitUI();
    }

    private void InitUI()
    {

        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            uiManager.UpdateCowUI(cowManager.getCowAt(i));
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveGameData(cowManager.curGeneration, cowManager.getCowAt(0), cowManager.getCowAt(1), cowManager.getCowAt(2), foodManager.getUnlockedFoods().Count, raceManager.getCurRaceIndex(), raceManager.getCurLeagueIndex());
    }

    private void UpdateUI()
    {
        if (!getListening())
            return;

        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            uiManager.UpdateCowUI(cowManager.getCowAt(i));
        }
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
        uiManager.SetUIGroup("PopUp", false);
    }

    private void OnCowRacePrompt()
    {
        uiManager.SetUIGroup("Race", true);
        uiManager.UpdateRaceUI(playerMouse.getCurCow().GetComponent<Cow>(), raceManager.getCurRace());
    }

    private void OnNoCowRace()
    {
        uiManager.SetUIGroup("Race", false);
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
                    uiManager.SetUIGroup("PopUp", true);
                    uiManager.MakePopUp("You won the race!");
                    Debug.Log("making pop up");
                    break;
                case RaceReward.FOOD:
                    foodManager.UnlockFood();
                    uiManager.SetUIGroup("PopUp", true);
                    uiManager.MakePopUp("You won and unlocked a new food!");
                    break;
                case RaceReward.PATTERN:
                    uiManager.SetUIGroup("PopUp", true);
                    uiManager.MakePopUp("You won and unlocked a new pattern!");
                    break;
            }
            SaveData();
            OnCowRacePrompt();
        }
        else
        {
            uiManager.MakePopUp("You lost the race");
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
