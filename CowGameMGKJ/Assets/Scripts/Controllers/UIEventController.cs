using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class UIEventController : MonoBehaviour
{
    // Takes UI clicks and fires Events
    public delegate void SwitchLogic(int state);
    public static event SwitchLogic switchLogic;

    public delegate void PopUpOff();
    public static event PopUpOff popUpOff;

    public delegate void RaceCowPrompt();
    public static event RaceCowPrompt raceCowPrompt;

    public delegate void NoRaceCow();
    public static event NoRaceCow noRaceCow;

    public delegate void RaceCow();
    public static event RaceCow raceCow;

    public delegate void TrainCow();
    public static event TrainCow trainCow;


    public delegate void AddCoin(int coins);
    public static event AddCoin addCoin;

    public delegate void TakeCoin(int coins);
    public static event TakeCoin takeCoin;

    public delegate void RetireCow();
    public static event RetireCow retireCow;

    public delegate void RenameCow(TextMeshProUGUI tf);
    public static event RenameCow renameCow;

    public delegate void RenameCowOff();
    public static event RenameCowOff renameCowOff;

    public void OnSwitchLogic(int state)
    {
        switchLogic?.Invoke(state);
    }

    public void OnSaveReset()
    {
        SaveSystem.ResetGameData();
    }


    public void OnPopUpButton()
    {
        popUpOff?.Invoke();
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



    public void OnCowNameYesButton(TextMeshProUGUI tf)
    {
        renameCow?.Invoke(tf);
    }

    public void OnCowNameNoButton()
    {
        renameCowOff.Invoke();
    }

    public void OnRetireButton()
    {
        retireCow?.Invoke();
    }



    public void OnTrainButton() //Runs warning about missing script behavior for race button
    {
        trainCow?.Invoke();
    }





    //SHOP
    public void OnCoinButton(int coins)
    {
        addCoin?.Invoke(coins);
    }

    public void OnPurchaseButton(int index)
    {
        takeCoin?.Invoke(index);
    }
}
