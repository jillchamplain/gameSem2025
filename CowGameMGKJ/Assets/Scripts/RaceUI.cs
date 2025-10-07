using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceUI : UIContainer
{
    public void setContainer(Cow theCow, Race theRace)
    {
        setTextElement("Power", "Power: " + theRace.getPower());
        setTextElement("Traits", theRace.getTraitAt(0) + " " + theRace.getTraitAt(1) + " " + theRace.getTraitAt(2));
        setTextElement("Prompt", "Race with " + theCow.getName() + "?");
    }
}
