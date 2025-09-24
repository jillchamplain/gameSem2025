using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcEventController : MonoBehaviour
{
    //Takes UI Button Inputs and sends events to arc controller to switch logic

    public delegate void SwitchLogic(int state);
    public static event SwitchLogic switchLogic;

    public void OnSceneSwitch(int state)
    {
        switchLogic?.Invoke(state);
    }
}
