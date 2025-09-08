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

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;
        Cow.cowMaxLevel += OnCowMaxLevel;

        PlayerMouse.mouseClickOn += OnMouseClickOn;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;

        PlayerMouse.mouseClickOn -= OnMouseClickOn;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }

    public void OnTrainCow()
    {
        Cow theCow = playerMouse.getCurCow().GetComponent<Cow>();
        TrainManager.TrainRegimen theRegimen = trainManager.SelectRandomTraining();
        theCow.setPower(theCow.getPower() + trainManager.RollTrainingSuccess(theRegimen));
        uiManager.UpdateCowUI(theCow);
        Debug.Log("updating");
    }

    void OnCowEat(Cow theCow, Food theFood)
    {
        playerMouse.setCurCow(theCow.gameObject);
        theCow.setPower(theCow.getPower() + theFood.getPower());
        foodManager.DeleteFood(theFood.gameObject);
        //Debug.Log("the cow is " + theCow.gameObject);
        uiManager.UpdateCowUI(theCow);
        uiManager.SetUIGroup("Train", false);
    }

    void OnCowMaxLevel(Cow theCow)
    {
        Debug.Log(theCow.gameObject.name + " reached max level!");
    }

    void OnMouseClickOn(GameObject theObject)
    {
        //Debug.Log("mouse clicked something, process event");
        if(theObject.CompareTag("Food"))
        {
            if (playerMouse.getCurCow())
            {
                playerMouse.setCurCow(null);
                uiManager.SetUIGroup("Train", false);
            }

                playerMouse.setCurFood(theObject);
            List<GameObject> theCows = cowManager.getCurCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<CapsuleCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        else if(theObject.CompareTag("Cow"))
        {
            if(playerMouse.getCurCow() == theObject)
            {
                playerMouse.setCurCow(null);
                uiManager.SetUIGroup("Train", false);
            }
            else
            {
                playerMouse.setCurCow(theObject);
                uiManager.SetUIGroup("Train", true);
            }
        }
    }

    void OnMouseRelease()
    {
        //renable collision of curFood if any
        if (!playerMouse.getCurFood())
            return;
        List<GameObject> theCows = cowManager.getCurCows();
        for(int i = 0; i < theCows.Count; i++)
        {
            Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<CapsuleCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
        }
        playerMouse.setCurFood(null);
        
    }

    public Vector2 SelectRandomSpawn() //NEED TO!!!!!!!!!!!!! CHECK FOR OVERLAP WITH OTHER FOOD AND COWS 
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
            else
                Debug.Log("spawned Wrong");
            theSpawn.x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
            theSpawn.y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);
        }
        //Debug.Log(theSpawn);

        return theSpawn;
    }

}
