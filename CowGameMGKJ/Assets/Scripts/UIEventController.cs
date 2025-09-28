using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventController : MonoBehaviour
{
    // Takes UI clicks and fires Events
    public delegate void SwitchLogic(int state);
    public static event SwitchLogic switchLogic;

    public delegate void RaceCow();
    public static event RaceCow raceCow;

    public void OnSwitchLogic(int state)
    {
        switchLogic?.Invoke(state);
    }

    public void OnSaveReset()
    {
        SaveSystem.ResetGameData();
    }

    public void OnRaceButton()
    {
        raceCow?.Invoke();
    }
}
