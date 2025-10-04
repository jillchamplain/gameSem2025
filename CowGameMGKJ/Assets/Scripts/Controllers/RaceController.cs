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

        UIEventController.raceCow += OnCowRace;
    }

    private void OnDisable()
    {
        MouseManager.mouseClick -= OnMouseClick;

        UIEventController.raceCow -= OnCowRace;
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
        SaveSystem.SaveGameData(cowManager.curGeneration, cowManager.getCowAt(0), cowManager.getCowAt(1), cowManager.getCowAt(2), foodManager.getUnlockedFoods().Count);
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

    public void OnCowRace()
    {
        if (raceManager.RaceCow(playerMouse.getCurCow().GetComponent<Cow>()))
        {
            foodManager.UnlockFood();
            particleManager.SpawnTextParticleAt("Power Increase", "You Win!", playerMouse.getCurCow().gameObject.transform.position);
            SaveData();

        }
        else
        {
            particleManager.SpawnTextParticleAt("Power Increase", "You Lose!", playerMouse.getCurCow().gameObject.transform.position);
            SaveData();
        }
    }
}
