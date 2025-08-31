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
        [SerializeField] bool isOn;
        public bool getOn() { return isOn; }
        public void setOn(bool newOn) { isOn = newOn; }
    }

    public void ToggleUIGroup(string theGroupName)
    {
        for(int i = 0; i < uiGroups.Length; i++)
        {
            if(theGroupName == uiGroups[i].getGroupName())
            {
                UIGroup theGroup = uiGroups[i];
                theGroup.setOn(!theGroup.getOn());
                theGroup.getCanvasGroup().interactable = !theGroup.getCanvasGroup().interactable;
                theGroup.getCanvasGroup().blocksRaycasts = !theGroup.getCanvasGroup().blocksRaycasts;
                if (theGroup.getOn())
                    theGroup.getCanvasGroup().alpha = 1f;
                else if (!theGroup.getOn())
                {
                    theGroup.getCanvasGroup().alpha = 0f;
                    Debug.Log("toggle off");
                }
                
            }
        }
    }

    public void UpdateCowUI(Cow theCow)
    {
        //Debug.Log("cow index is " + theCow.getUIIndex());
        //Debug.Log("cow UIs are " + cowUIs.Length);
        for(int i = 0; i < cowUIs.Length; i++)
        {
            if (theCow.getUIIndex() == i)
            {
                
                CowInfoUI theUI = cowUIs[i];

                theUI.getNameTF().text = theCow.getName();
                theUI.getGenTF().text = theCow.getGen().ToString();
                theUI.getLevelTF().text = theCow.getLevel().ToString();
                theUI.getPowerTF().text = theCow.getPower().ToString();
            }
            
        }

    }

}
