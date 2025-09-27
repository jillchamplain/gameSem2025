using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ArcController : MonoBehaviour
{
    //Manages game state and switches off which logic controller needs to be used to listen for events
    [SerializeField] GameState curState;
    public GameState getGameState() { return curState; }
    public void setGameState(GameState state) { curState = state; ManageState(curState); }
    public void setGameState(int state) { curState = (GameState)state; ManageState(curState); }


   [SerializeField] List<LogicController> logicControllers;
    public LogicController getLogicController(GameState state)
    {
        foreach(LogicController controller in logicControllers)
        {
            if (controller.getGameState() == state)
                return controller;
        }
        Debug.Log("Could not find controller: " + state);
        LogicController nullController = null;
        return nullController;
    }
   

    public static ArcController inst;

    private void OnEnable()
    {
        UIEventController.switchLogic += ManageState;
    }

    private void OnDisable()
    {
        UIEventController.switchLogic -= ManageState;
    }

    private void Start()
    {
        setGameState(GameState.TITLE);

        if (inst == null)
            inst = this;
    }

    //STATE ENUM
    //Disables logic controllers?

    public void ManageState(int state)
    {
        ManageState((GameState)state);
    }

    public void ManageState(GameState state)
    {
        foreach(LogicController controller in logicControllers)
        {
            if (controller.getGameState() != state)
            {
                controller.Reset();
                controller.setListening(false);
                controller.ToggleManagers(false);
                controller.gameObject.SetActive(false);
            }
            else
            {
                controller.Init();
                controller.setListening(true);
                controller.ToggleManagers(true);
                controller.gameObject.SetActive(true);
            }
        }

        switch (state)
        {
            case GameState.TITLE:
                break;
            case GameState.HOME:
                break;
            case GameState.RACE:
                break;
        }
    }

}
