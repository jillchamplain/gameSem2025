using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class ArcTestManager : MonoBehaviour
{
    //Manages game state and switches off which logic controller needs to be used to listen for events
    [SerializeField] GameState curState;
    public GameState getGameState() { return curState; }
    public void setGameState(GameState state) { curState = state; ManageState(curState); }
    public void setGameState(int state) { curState = (ArcTestManager.GameState)state; ManageState(curState); }


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
   

    public static ArcTestManager inst;

    //STATE ENUM
    //Disables logic controllers?
    public enum GameState
    {
        TITLE = -1,
        HOME = 0,
        RACE = 1
    }




    public void ManageState(GameState state)
    {
        foreach(LogicController controller in logicControllers)
        {
            if (controller.getGameState() != state)
                controller.gameObject.SetActive(false);
            else
                controller.gameObject.SetActive(true);
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
