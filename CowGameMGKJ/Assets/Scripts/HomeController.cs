using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : LogicController
{
    public static HomeController inst;

    [Header("Refs")]
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
}
