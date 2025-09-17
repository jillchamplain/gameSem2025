using JetBrains.Annotations;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenManager : MonoBehaviour
{
    //Logic for HOME SCREEN
    //Manages Events from Objects and Passes Data over to other Scripts
    [Header("Refs")]
    [SerializeField] CowManager cowManager;
    [SerializeField] FoodManager foodManager;
    [SerializeField] TrainManager trainManager;
    [SerializeField] UIManager uiManager;

    public static HomeScreenManager inst;

    void OnEnable()
    {
        PlayerMouse.mouseClick += OnMouseClick;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        PlayerMouse.mouseClick -= OnMouseClick;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (inst == null)
            inst = this;
        SetUpGame();
    }

    //SET UP
    void SetUpGame()
    {
        cowManager.SpawnCows(3); //Prob give cow manager construct an array?
    }

    //UI
    void UpdateUI()
    {
        for (int i = 0; i < cowManager.getCows().Count; i++)
        {
            uiManager.UpdateCowUI(cowManager.getCowAt(i));
        }
    }



    //COWS
    void OnCowSpawned()
    {
        UpdateUI();
    }



    //MOUSE
    void OnMouseClick(List<GameObject> theSelectionList) //FIX THIS MORE
    {
        GameObject firstSelected = null;
        GameObject lastSelected = null;

        for (int i = 0; i < theSelectionList.Count; i++)
        {
            if (i == 0)
                firstSelected = theSelectionList[i];
            if (i == theSelectionList.Count - 1)
                lastSelected = theSelectionList[i];
        }

        //HANDLE LAST SELECTED OBJECT
        if (lastSelected.GetComponent<Food>())
        {
            lastSelected.GetComponent<Food>().setSelected(true);
        }

        if (lastSelected.GetComponent<Cow>())
        {
            //lastSelected.GetComponent<Cow>().setSelected(true);
        }

        //HANDLE PREVIOUSLY SELECTED OBJECT

    }
    void OnMouseRelease(List<GameObject> theSelectionList) //FIX THIS MORE
    {
        GameObject firstSelected = null;
        GameObject lastSelected = null;

        for (int i = 0; i < theSelectionList.Count; i++)
        {
            if (i == 0)
                firstSelected = theSelectionList[i];
            if (i == theSelectionList.Count - 1)
                lastSelected = theSelectionList[i];
        }

        //HANDLE LAST SELECTED OBJECT
        if (lastSelected.GetComponent<Food>())
        {
            lastSelected.GetComponent<Food>().setSelected(false);
        }


        //What happens when the player isn't clicking?

        //If last selection was food "drop it"


        //If last selection cow 
    }
    

    
}
