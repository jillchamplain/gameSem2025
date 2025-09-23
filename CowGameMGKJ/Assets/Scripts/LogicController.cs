using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicController : MonoBehaviour
{
    [SerializeField] ArcTestManager.GameState state;
    public ArcTestManager.GameState getGameState() { return state; }
    public void setGameState(ArcTestManager.GameState newState) { state = newState; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
