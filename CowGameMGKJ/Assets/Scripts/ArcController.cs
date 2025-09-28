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
    public void ManageState(int state)
    {
        ManageState((GameState)state);
    }
    public void ManageState(GameState state)
    {
        //Toggle OFF other managers
        for(int i = 0; i < logicControllers.Count; i++)
        {
            if (logicControllers[i].getGameState() != state)
            {
                logicControllers[i].setListening(false);
                logicControllers[i].Reset();
                logicControllers[i].ToggleManagers(false);
                logicControllers[i].gameObject.SetActive(false);
            }
        }
        //Toggle ON current managers (even if shared by other game states)
        for (int i = 0; i < logicControllers.Count; i++)
        {
            if (logicControllers[i].getGameState() == state)
            {
                logicControllers[i].Init();
                logicControllers[i].setListening(true);
                logicControllers[i].ToggleManagers(true);
                logicControllers[i].gameObject.SetActive(true);
            }
        }

    }

}
