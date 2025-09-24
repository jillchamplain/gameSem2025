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
    [SerializeField] UIContainer[] cowUIs = new UIContainer[3];
    [SerializeField] UIGroup[] uiGroups = new UIGroup[4];

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
        if (theCow == null)
            return;
        //Debug.Log("the Cow is " + theCow.gameObject);
        //Debug.Log("cow index is " + theCow.getUIIndex());
        //Debug.Log("cow UIs are " + cowUIs.Length);
        for (int i = 0; i < cowUIs.Length; i++)
        {
            if (theCow.getUIIndex() == i)
            {
                UIContainer theUI = cowUIs[i];
                if (theUI.getPowerSlider().value < theCow.getPower())
                    theUI.PopAnimation();
                theUI.setContainer(theCow);
            }
        }
    }

}
