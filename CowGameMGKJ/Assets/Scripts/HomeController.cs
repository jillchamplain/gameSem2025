using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cow;
using static CowManager;
using static PlayerMouse;

public class HomeController : LogicController
{
    public static HomeController inst;

    [Header("Refs")]
    [SerializeField] PlayerMouse playerMouse;

    [SerializeField] UIManager uiManager; //Different UIs > Need to change
    [SerializeField] CowManager cowManager;
    [SerializeField] FoodManager foodManager;
    [SerializeField] TrainManager trainManager;
    private void Awake()
    {
        setGameState(ArcTestManager.GameState.HOME); 
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
        Cow.cowRetire += OnCowRetire;

        CowManager.cowSpawned += OnCowSpawned;

        PlayerMouse.mouseClick += OnMouseClick;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Cow.cowEat -= OnCowEat;
        Cow.cowMaxLevel -= OnCowMaxLevel;
        Cow.cowLevelUp -= OnCowLevelUp;
        Cow.cowRetire -= OnCowRetire;

        CowManager.cowSpawned -= OnCowSpawned;

        PlayerMouse.mouseClick -= OnMouseClick;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }

    private void OnCowSpawned(Cow theCow)
    {
        //Do something
    }

    private void OnCowEat(Cow theCow, Food theFood)
    {
        playerMouse.setCurCow(theCow.gameObject); //Store cow

        theCow.setPower(theCow.getPower() + theFood.getPower()); //Increase cow power
        string powerIncreaseText = "+ " + theFood.getPower();

        if(!(theCow.getPower() >= theCow.getMaxPower())) //Run UI changes if not 
        {

        }

    }

    private void OnCowTrain()
    {

    }

    private void OnCowLevelUp(Cow theCow)
    {

    }

    private void OnCowMaxLevel(Cow theCow)
    {

    }

    private void OnCowRetire(Cow theCow)
    {

    }

    private void OnMouseClick(GameObject theObject)
    {

    }

    private void OnMouseRelease()
    {

    }

}
