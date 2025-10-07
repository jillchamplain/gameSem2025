using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
public class UIManager : Manager
{
    [Header("Refs")]
    [SerializeField] CowUI[] cowUIs = new CowUI[3];
    [SerializeField] UIGroup[] uiGroups = new UIGroup[4];
    [SerializeField] RaceUI racePrompt;

    [Serializable] 
    struct UIGroup
    {
        [SerializeField] string groupName;
        public string getGroupName() { return groupName; }
        [SerializeField] CanvasGroup canvasGroup;
        public CanvasGroup getCanvasGroup() { return canvasGroup; }
    }

    public void SetUIGroup(string theGroupName, bool isOn)
    {
        if (uiGroups.Length <= 0)
            return;
        for(int i = 0; i < uiGroups.Length; i++)
        {
            if(theGroupName == uiGroups[i].getGroupName())
            {
                UIGroup theGroup = uiGroups[i];
                theGroup.getCanvasGroup().interactable = isOn;
                theGroup.getCanvasGroup().blocksRaycasts = isOn;
                if (theGroup.getCanvasGroup().interactable)
                    theGroup.getCanvasGroup().alpha = 1f;
                else if (!theGroup.getCanvasGroup().interactable)
                {
                    theGroup.getCanvasGroup().alpha = 0f;
                    //Debug.Log(theGroup.getCanvasGroup().alpha);
                    //Debug.Log("toggle off");
                }
                
            }
        }
    }

    public void UpdateCowUI(Cow theCow)
    {
        //Debug.Log("the Cow is " + theCow.gameObject);
        if (theCow == null)
            return;
        //Debug.Log("cow index is " + theCow.getUIIndex());
        //Debug.Log("cow UIs are " + cowUIs.Length);
        for (int i = 0; i < cowUIs.Length; i++)
        {
            if (theCow.getUIIndex() == i)
            {
                CowUI theUI = cowUIs[i];
                if (theUI.getSliderElement("Power").value < theCow.getPower())
                    theUI.PopAnimation();
                theUI.setContainer(theCow);
            }
        }
    }

    public void UpdateRaceUI(Cow theCow, Race theRace)
    {
        //Debug.Log("the Cow is " + theCow.gameObject);
        if (theCow == null)
            return;
        //Debug.Log("cow index is " + theCow.getUIIndex());
        //Debug.Log("cow UIs are " + cowUIs.Length);
        racePrompt.PopAnimation();
        racePrompt.setContainer(theCow, theRace);
    }

}
