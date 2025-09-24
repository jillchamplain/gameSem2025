using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cow;
using static CowManager;
using static MouseManager;

public class HomeController : LogicController
{
    public static HomeController inst;

    [Header("Refs")]
    [SerializeField] MouseManager playerMouse;
    [SerializeField] CowManager cowManager;
    [SerializeField] FoodManager foodManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] TrainManager trainManager;

    private void Awake()
    {
        setGameState(GameState.HOME);
    }

    private void Start()
    {
        if (inst == null)
            inst = this;

        GameSetUp();
    }

    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;
        Cow.cowMaxLevel += OnCowMaxLevel;
        Cow.cowLevelUp += OnCowLevelUp;
        Cow.cowRetire += OnCowRetire;

        CowManager.cowSpawned += OnCowSpawned;

        FoodManager.foodSpawn += OnFoodSpawned;

        MouseManager.mouseClick += OnMouseClick;
        MouseManager.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Debug.Log("Disabling " + this.gameObject);
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;
        Cow.cowLevelUp -= OnCowLevelUp;
        Cow.cowRetire -= OnCowRetire;

        CowManager.cowSpawned -= OnCowSpawned;

        MouseManager.mouseClick -= OnMouseClick;
        MouseManager.mouseRelease -= OnMouseRelease;
    }

    private void GameSetUp()
    {
        if (!getListening())
            return;

        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
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

    private void OnCowSpawned(Cow theCow)
    {
        if (!getListening())
            return;

        UpdateUI();
    }

    private void OnCowEat(Cow theCow, Food theFood)
    {
        if (!getListening())
            return;

        playerMouse.setCurCow(theCow.gameObject); //Store cow

        foodManager.DeleteFood(theFood.gameObject);

        theCow.setPower(theCow.getPower() + theFood.getPower()); //Increase cow power
        string powerIncreaseText = "+ " + theFood.getPower();

        if(!(theCow.getPower() >= theCow.getMaxPower())) //Run Visual Changes if not retiring
        {
            theCow.PlayAnimation(Cow.CowAnims.FEED); ///MOVE COW VISUALS TO DIFF CLASS
            particleManager.SpawnTextParticleAt("Power Increase", powerIncreaseText, theCow.gameObject.transform.position);
            uiManager.UpdateCowUI(theCow);
        }
        uiManager.SetUIGroup("Train", false);
    }

   public void OnCowTrain()
    {
        if (!getListening())
            return;

        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        TrainManager.TrainRegimen theRegimen = trainManager.SelectRandomTraining();
        int increase = trainManager.RollTrainingSuccess(theRegimen);
        theCow.setPower(theCow.getPower() + increase);
        if (theCow.getPower() >= theCow.getMaxPower())
        {
            uiManager.SetUIGroup("Train", false);
            return;
        }
        theCow.PlayAnimation(Cow.CowAnims.FEED);
        string powerIncreaseText = "+ " + increase;
        particleManager.SpawnTextParticleAt("Power Increase", powerIncreaseText, theCow.gameObject.transform.position);
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
    }

    private void OnCowMaxLevel(Cow theCow)
    {
        if (!getListening())
            return;

        UpdateUI();
        theCow.setSelected(false);
        theCow.PlayAnimation(Cow.CowAnims.RETIRE); //Hook up so deleting waits for animationt to play
    }

    private void OnCowRetire(Cow theCow)
    {
        if (!getListening())
            return;

        uiManager.SetUIGroup("Train", false);
        playerMouse.setCurCow(null);
        cowManager.DeleteCow(theCow.gameObject);
        cowManager.SpawnCow(spawnManager.SelectRandomSpawn(cowManager.cowPrefab));
    }

    private void OnFoodSpawned(GameObject theFood)
    {
        if (!getListening())
            return;

        Debug.Log("food spawned by " + this.gameObject);
        foodManager.SpawnFood(theFood, spawnManager.SelectRandomSpawn(theFood));
    }

    private void OnMouseClick(GameObject theObject)
    {
        if (!getListening())
            return;

        if (theObject.CompareTag("Food"))
        {
            if (playerMouse.getCurCow())
            {
                playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                playerMouse.setCurCow(null);
                uiManager.SetUIGroup("Train", false);
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
            if (playerMouse.getCurCow() == theObject)
            {
                playerMouse.setCurCow(null);
                theObject.GetComponent<Cow>().setSelected(false);
                uiManager.SetUIGroup("Train", false);
            }
            else
            {
                if (playerMouse.getCurCow())
                {
                    playerMouse.getCurCow().GetComponent<Cow>().setSelected(false);
                }
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelected(true);
                uiManager.SetUIGroup("Train", true);
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
