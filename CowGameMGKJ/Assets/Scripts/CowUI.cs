using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowUI : UIContainer
{ 
    public void setContainer(Cow theCow)
    {

        setTextElement("Name", theCow.getName());
        setTextElement("Gen", theCow.getGen().ToString());
        setTextElement("Level", theCow.getLevel().ToString());
        setTextElement("Power", theCow.getPower().ToString());

        setSliderElementMax("Power", theCow.getMaxPower());
        getSliderElement("Power").DOValue(theCow.getPower(), 1.0f);

    }


    public void PopAnimation()
    {
        DOTween.CompleteAll();
        this.gameObject.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.1f, 0.1f, 0.0f), 0.5f, 1);

    }
}
