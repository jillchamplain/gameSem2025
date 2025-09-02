using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] CowInfoUI[] cowUIs = new CowInfoUI[3];
    [SerializeField] UIGroup[] uiGroups = new UIGroup[4];

    [Serializable]
    struct CowInfoUI
    {
        [SerializeField] GameObject gameObjectRef;
        public GameObject getGameObjectRef() { return gameObjectRef; }
        [SerializeField] TextMeshProUGUI nameTF;
        public TextMeshProUGUI getNameTF() { return nameTF; }
        [SerializeField] TextMeshProUGUI genTF;
        public TextMeshProUGUI getGenTF() { return genTF; }
        [SerializeField] TextMeshProUGUI levelTF;
        public TextMeshProUGUI getLevelTF() { return levelTF; }
        [SerializeField] TextMeshProUGUI powerTF;
        public TextMeshProUGUI getPowerTF() { return powerTF; }
    }
    [Serializable]
    struct UIGroup
    {
        [SerializeField] string groupName;
        public string getGroupName() { return groupName; }
        [SerializeField] CanvasGroup canvasGroup;
        public CanvasGroup getCanvasGroup() { return canvasGroup; }
    }

    public void ToggleUIGroup(string theGroupName)
    {
        for(int i = 0; i < uiGroups.Length; i++)
        {
            if(theGroupName == uiGroups[i].getGroupName())
            {
                UIGroup theGroup = uiGroups[i];
                theGroup.getCanvasGroup().interactable = !theGroup.getCanvasGroup().interactable;
                theGroup.getCanvasGroup().blocksRaycasts = !theGroup.getCanvasGroup().blocksRaycasts;
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
        for(int i = 0; i < cowUIs.Length; i++)
        {
            if (theCow.getUIIndex() == i)
            {
                
                CowInfoUI theUI = cowUIs[i];
                //Debug.Log(theUI.getGameObjectRef());
                theUI.getNameTF().text = theCow.getName();
                theUI.getGenTF().text = theCow.getGen().ToString();
                theUI.getLevelTF().text = theCow.getLevel().ToString();
                theUI.getPowerTF().text = theCow.getPower().ToString();
            }
            
        }

    }

}
