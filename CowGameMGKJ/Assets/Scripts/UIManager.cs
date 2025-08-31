using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] CowInfoUI[] cowUIs = new CowInfoUI[3];

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
