using System.Collections;
using System.Collections.Generic;
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
        raceManager.RaceCow(playerMouse.getCurCow().GetComponent<Cow>());
    }
}
