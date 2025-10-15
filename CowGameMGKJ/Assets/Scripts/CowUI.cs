using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowUI : UIContainer
{ 
    public override void setContainer(Cow theCow)
    {

        setTextElement("Name", theCow.getName());
        setTextElement("Gen", theCow.getGen().ToString());
        setTextElement("Level", theCow.getLevel().ToString());
        setTextElement("Power", theCow.getPower().ToString());

        setSliderElementMax("Power", theCow.getMaxPower());
        getSliderElement("Power").DOValue(theCow.getPower(), 1.0f);

    }

}
