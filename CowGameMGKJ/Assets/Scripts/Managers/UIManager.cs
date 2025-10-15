using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Manager
{
    [Header("Refs")]
    [SerializeField] UIGroup[] uiGroups = new UIGroup[4];
    public UIGroup getUIGroup(string name)
    {
        foreach(UIGroup ui in uiGroups)
        {
            if(ui.getGroupName() == name)
            {
                return ui;
            }
        }
        return null;
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
                }
            }
        }
    }

    //For COWS
    public void UpdateUIGroup(string theGroupName, GameObject theCowObject, int index)
    {

        Cow theCow = theCowObject.GetComponent<Cow>();
        if (theCow == null)
            return;
        UIGroup cowGroup = getUIGroup(theGroupName);
        if (cowGroup == null)
            return;

        //Debug.Log("the index is " + index);
        UIContainer container = cowGroup.getElement(index); //COW UI
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

    //For RACES
    public void UpdateUIGroup(string theGroupName, Cow theCow, Race theRace)
    {
        UIContainer container = getUIGroup(theGroupName).getElement("Race Info");
        if (container == null)
            return;
        container.PopAnimation();
        container.setTextElement("Power", "Power: " + theRace.getPower());
        container.setTextElement("Traits", theRace.getTraitAt(0) + " " + theRace.getTraitAt(1) + " " + theRace.getTraitAt(2));
        container.setTextElement("Prompt", "Race with " + theCow.getName() + "?");
    }

    //For POP UPS
    public void UpdateUIGroup(string theGroupName, string textInfo)
    {
        UIContainer container = getUIGroup(theGroupName).getElement("Pop Up");
        if (container == null)
            return;
        container.PopAnimation();
        container.setTextElement("Pop Up Info", textInfo);
    }

    public void UICleanUp()
    {
        StartCoroutine(UIAnimCleanup());
    }
    IEnumerator UIAnimCleanup()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        DOTween.CompleteAll();
    }

    /*public void UpdateCowUI(Cow theCow)
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
    }*/

    /*public void UpdateRaceUI(Cow theCow, Race theRace)
    {
        //Debug.Log("the Cow is " + theCow.gameObject);
        if (theCow == null)
            return;
        //Debug.Log("cow index is " + theCow.getUIIndex());
        //Debug.Log("cow UIs are " + cowUIs.Length);
        racePrompt.PopAnimation();
        racePrompt.setContainer(theCow, theRace);
    }*/

}
