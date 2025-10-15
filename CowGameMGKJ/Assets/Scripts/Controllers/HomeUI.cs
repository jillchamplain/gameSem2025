using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : UIController
{
    public UIGroup getUIGroup(string name)
    {
        foreach (UIGroup ui in uiGroups)
        {
            if (ui.getGroupName() == name)
            {
                return ui;
            }
        }
        return null;
    }

    public void SetUIGroup(string theGroupName, bool isOn)
    {
        if (uiGroups.Count <= 0)
            return;
        for (int i = 0; i < uiGroups.Count; i++)
        {
            if (theGroupName == uiGroups[i].getGroupName())
            {
                UIGroup theGroup = uiGroups[i];
                theGroup.getCanvasGroup().interactable = isOn;
                theGroup.getCanvasGroup().blocksRaycasts = isOn;
                if (theGroup.getCanvasGroup().interactable)
                    theGroup.getCanvasGroup().alpha = 1f;
                else if (!theGroup.getCanvasGroup().interactable)
                {
                    theGroup.getCanvasGroup().alpha = 0f;
                }
            }
        }
    }

    //For COWS
    public void UpdateCowUI(GameObject theCowObject, int index)
    {

        Cow theCow = theCowObject.GetComponent<Cow>();
        if (theCow == null)
            return;
        UIGroup cowGroup = getUIGroup("Cow");
        if (cowGroup == null)
            return;

        //Debug.Log("the index is " + index);
        UIContainer container = cowGroup.getContainer(index); //COW UI
        //Debug.Log("Returning " + container);
        if (container == null)
            return;
        //If Cow not changing skip

        container.PopAnimation();
        container.setTextElement("Name", theCow.getName());
        container.setTextElement("Gen", theCow.getGen().ToString());
        container.setTextElement("Level", theCow.getLevel().ToString());
        container.setTextElement("Power", theCow.getPower().ToString());

        container.setSliderElementMax("Power", theCow.getMaxPower());
        container.getSliderElement("Power").DOValue(theCow.getPower(), 1.0f);
    }

    //For POP UPS
    public void UpdatePopUpUI(string textInfo)
    {
        UIContainer container = getUIGroup("Pop Up").getContainer("Pop Up");
        if (container == null)
            return;
        container.PopAnimation();
        container.setTextElement("Pop Up Info", textInfo);
    }

}
