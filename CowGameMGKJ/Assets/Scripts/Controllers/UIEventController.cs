using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIEventController : MonoBehaviour
{
    // Takes UI clicks and fires Events
    public delegate void SwitchLogic(int state);
    public static event SwitchLogic switchLogic;

    public delegate void RaceCowPrompt();
    public static event RaceCowPrompt raceCowPrompt;

    public delegate void NoRaceCow();
    public static event NoRaceCow noRaceCow;

    public delegate void RaceCow();
    public static event RaceCow raceCow;

    public delegate void TrainCow();
    public static event TrainCow trainCow;

    public void OnSwitchLogic(int state)
    {
        switchLogic?.Invoke(state);
    }

    public void OnSaveReset()
    {
        SaveSystem.ResetGameData();
    }

    public void OnRacePromptButton() //Runs warning about missing script behavior for race button
    {
        raceCowPrompt?.Invoke();
    }

    public void OnRaceButton() //Runs warning about missing script behavior for race button
    {
        raceCow?.Invoke();
    }

    public void OnRaceNoButton() //Runs warning about missing script behavior for race button
    {
        noRaceCow?.Invoke();
    }

    public void OnTrainButton() //Runs warning about missing script behavior for race button
    {
        trainCow?.Invoke();
    }
}
