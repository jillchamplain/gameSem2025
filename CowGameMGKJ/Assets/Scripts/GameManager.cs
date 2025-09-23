using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GlobalData globalData;
    [SerializeField] FoodManager foodManager;
    [SerializeField] CowManager cowManager;
    [SerializeField] TrainManager trainManager;
    [SerializeField] RaceManager raceManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] ParticleManager particleManager;

    [SerializeField] PlayerMouse playerMouse;
    [SerializeField] BoxCollider2D spawnZone;
    public static GameManager instance;
    public static GameManager getInstance() { return instance; }
    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;
        Cow.cowMaxLevel += OnCowMaxLevel;
        Cow.cowLevelUp += OnCowLevelUp;
        Cow.cowRetire += OnCowRetire;

        CowManager.cowSpawned += OnCowSpawned;

        PlayerMouse.mouseClick += OnMouseClickOn;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;
        Cow.cowLevelUp += OnCowLevelUp;
        Cow.cowRetire += OnCowRetire;

        CowManager.cowSpawned -= OnCowSpawned;

        PlayerMouse.mouseClick -= OnMouseClickOn;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }
    private void Start()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this.gameObject);
        SetUpGame();
    }

    void SetUpGame()
    {
        cowManager.SpawnCows(3);
    }

    void UpdateCowUI()
    {
        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            uiManager.UpdateCowUI(cowManager.getCowAt(i));
        }
    }

    void OnCowSpawned(Cow theCow)
    {
        globalData.AddCow(theCow);
        UpdateCowUI();
    }

    void OnTrainCow()
    {
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
        uiManager.UpdateCowUI(theCow);
        //Debug.Log("updating");
    }

    public void OnRaceCow()
    {
        raceManager.RaceCow(playerMouse.getCurCow().GetComponent<Cow>());
    }

    void OnCowEat(Cow theCow, Food theFood)
    {

        playerMouse.setCurCow(theCow.gameObject);
        theCow.setPower(theCow.getPower() + theFood.getPower());
        string powerIncreaseText = "+ " + theFood.getPower();
        foodManager.DeleteFood(theFood.gameObject);
        //Debug.Log("the cow is " + theCow.gameObject);

        if (!(theCow.getPower() >= theCow.getMaxPower())) //Prevents feeding from overwriting deletion UI
        {
            Debug.Log("Cow power: " + theCow.getPower() + " max power: " + theCow.getMaxPower());
            theCow.PlayAnimation(Cow.CowAnims.FEED);
            particleManager.SpawnTextParticleAt("Power Increase", powerIncreaseText, theCow.gameObject.transform.position);
            uiManager.UpdateCowUI(theCow);
        }
            uiManager.SetUIGroup("Train", false);
    }

    void OnCowMaxLevel(Cow theCow)
    {
        //Debug.Log(theCow.gameObject.name + " reached max level!");
        UpdateCowUI();
        theCow.setSelected(false);
        theCow.PlayAnimation(Cow.CowAnims.RETIRE); //Hook up so deleting waits for animationt to play
        
    }

    void OnCowRetire(Cow theCow)
    {
        uiManager.SetUIGroup("Train", false);
        playerMouse.setCurCow(null);
        cowManager.DeleteCow(theCow.gameObject);
        cowManager.SpawnCow();
    }

    void OnCowLevelUp(Cow theCow)
    {
        if (theCow.getMaxPower() == theCow.getPower())
            return;
        Vector3 spawn = new Vector3(theCow.transform.position.x, theCow.transform.position.y + 0.75f,0);
        particleManager.SpawnTextParticleAt("Power Increase", "Level Up!", spawn);
        
    }

    void OnMouseClickOn(GameObject theObject)
    {
        //Debug.Log("mouse clicked something, process event");
        if(theObject.CompareTag("Food"))
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

        else if(theObject.CompareTag("Cow"))
        {
            if(playerMouse.getCurCow() == theObject)
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

    void OnMouseRelease()
    {
        //renable collision of curFood if any
        if (!playerMouse.getCurFood())
            return;
        List<GameObject> theCows = cowManager.getCows();
        for(int i = 0; i < theCows.Count; i++)
        {
            Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<BoxCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
        }
        playerMouse.setCurFood(null);
        
    }

    public Vector2 SelectRandomSpawn(GameObject theObject) //NEED TO!!!!!!!!!!!!! CHECK FOR OVERLAP WITH OTHER FOOD AND COWS STILL DOESN'T WORK
    {
        bool canSelectSpawn = false;
        Vector2 theSpawn = Vector2.zero;
        theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);

        int attempt = 0;
        int numAttempts = 200;

        while (!canSelectSpawn && attempt < numAttempts)
        {
            if (spawnZone.bounds.Contains(theSpawn))
            {
                canSelectSpawn = true;
            }
            //Check if overlapping Cows
            /*for(int i = 0; i < cowManager.getCows().Count; i++)
            {
                if (cowManager.getCowAt(i).gameObject.GetComponent<BoxCollider2D>().bounds.Contains(theSpawn))
                {
                    canSelectSpawn = false;
                    //Debug.Log("Overlapped with Cow");
                }
                
            }

            //Check if overlapping Food
            for(int i = 0; i < foodManager.getCurFoods().Count; i++)
            {
                if(foodManager.getCurFoodAt(i).gameObject.GetComponent<BoxCollider2D>().bounds.Contains(theSpawn))
                {
                    canSelectSpawn = false;
                    //Debug.Log("Overlapped with Food");
                }
            }
            */

            Collider2D[] colliders = Physics2D.OverlapCircleAll(theSpawn, theObject.transform.localScale.x * 2f);
            foreach(Collider2D collider in colliders)
            {
                canSelectSpawn = false;
            }

            if (!canSelectSpawn)
            {
                //Debug.Log("spawned Wrong");
                theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
                theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
                attempt++;
            }
        }
        if(attempt >= numAttempts)
        {
            return Vector2.zero;
        }
        //Debug.Log("spawning at " + theSpawn);

        return theSpawn;
    }

}
