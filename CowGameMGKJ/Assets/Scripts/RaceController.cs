using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : LogicController
{
    public static RaceController inst;

    [Header("Refs")]
    [SerializeField] UIManager uiManager; //Different UIs > Need to change

    [SerializeField] CowManager cowManager;

    [SerializeField] RaceManager raceManager;
    private void Awake()
    {
        setGameState(GameState.RACE);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (inst == null)
            inst = this;
    }
}
