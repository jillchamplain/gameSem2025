using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] FoodManager foodManager;
    [SerializeField] CowManager cowManager;
    [SerializeField] TrainManager trainManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerMouse playerMouse;
    [SerializeField] BoxCollider2D spawnZone;
    public static GameManager instance;
    public static GameManager getInstance() { return instance; }
    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;
        Cow.cowMaxLevel += OnCowMaxLevel;

        CowManager.cowSpawned += OnCowSpawned;

        PlayerMouse.mouseClickOn += OnMouseClickOn;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;

        CowManager.cowSpawned -= OnCowSpawned;

        PlayerMouse.mouseClickOn -= OnMouseClickOn;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }
    private void Start()
    {
        if (instance == null)
            instance = this;
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
        UpdateCowUI();
    }

    void OnTrainCow()
    {
        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        TrainManager.TrainRegimen theRegimen = trainManager.SelectRandomTraining();
        theCow.setPower(theCow.getPower() + trainManager.RollTrainingSuccess(theRegimen));
        if (theCow.getPower() >= theCow.getMaxPower())
        {
            uiManager.SetUIGroup("Train", false);
            return;
        }
        uiManager.UpdateCowUI(theCow);
        //Debug.Log("updating");
    }

    void OnCowEat(Cow theCow, Food theFood)
    {

        playerMouse.setCurCow(theCow.gameObject);
        theCow.setPower(theCow.getPower() + theFood.getPower());
        foodManager.DeleteFood(theFood.gameObject);
        //Debug.Log("the cow is " + theCow.gameObject);
        theCow.PlayAnimation(Cow.CowAnims.FEED);
        if(!(theCow.getPower() >= theCow.getMaxPower())) //Prevents feeding from overwriting deletion UI
            uiManager.UpdateCowUI(theCow);
        uiManager.SetUIGroup("Train", false);
    }

    void OnCowMaxLevel(Cow theCow)
    {
        Debug.Log(theCow.gameObject.name + " reached max level!");
        theCow.PlayAnimation(Cow.CowAnims.RETIRE); //Hook up so deleting waits for animationt to play
        cowManager.DeleteCow(theCow.gameObject);
        cowManager.SpawnCow();
    }

    void OnMouseClickOn(GameObject theObject)
    {
        //Debug.Log("mouse clicked something, process event");
        if(theObject.CompareTag("Food"))
        {
            if (playerMouse.getCurCow())
            {
                playerMouse.setCurCow(null);
                theObject.GetComponent<Cow>().setSelection(false);
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
                theObject.GetComponent<Cow>().setSelection(false);
                uiManager.SetUIGroup("Train", false);
            }
            else
            {
                playerMouse.setCurCow(theObject);
                theObject.GetComponent<Cow>().setSelection(true);
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

    public Vector2 SelectRandomSpawn() //NEED TO!!!!!!!!!!!!! CHECK FOR OVERLAP WITH OTHER FOOD AND COWS STILL DOESN'T WORK
    {
        bool canSelectSpawn = false;
        Vector2 theSpawn = Vector2.zero;
        theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        while (!canSelectSpawn)
        {
            if (spawnZone.bounds.Contains(theSpawn))
            {
                canSelectSpawn = true;
            }
            //Check if overlapping Cows
            for(int i = 0; i < cowManager.getCows().Count; i++)
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
            if (!canSelectSpawn)
            {
                //Debug.Log("spawned Wrong");
                theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
                theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
            }
        }
        //Debug.Log(theSpawn);

        return theSpawn;
    }

}
