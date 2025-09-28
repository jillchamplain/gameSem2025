using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class LogicController : MonoBehaviour
{
    [SerializeField] GameState state;
    public GameState getGameState() { return state; }
    public void setGameState(GameState newState) { state = newState; }

    [SerializeField] bool isListening;
    public bool getListening() { return isListening; }
    public void setListening(bool value) { isListening = value; }

    [SerializeField] List<Manager> managers;
    public Manager getManager(ManagerType theType) //MAKE MANAGERS NOT KNOW WHAT THEY DO
    {
        foreach(Manager manager in managers)
        {
            if (manager.type == theType)
            {
                return manager;
            }
        }
        return null;
    }

    public void ToggleManagers(bool value)
    {
        foreach(Manager manager in managers)
        {
            manager.gameObject.SetActive(value);
            //Debug.Log("Setting " + manager.gameObject + " to " + value);
        }
    }
    abstract public void Reset();
    abstract public void Init();
}
