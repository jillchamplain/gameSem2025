using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : UIController
{


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
        UIContainer container = cowGroup.getContainer("Cow" + (index + 1)); //COW UI
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
