using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIContainer : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] TextMeshProUGUI nameTF;
    public TextMeshProUGUI getNameTF() { return nameTF; }
    [SerializeField] TextMeshProUGUI genTF;
    public TextMeshProUGUI getGenTF() { return genTF; }
    [SerializeField] TextMeshProUGUI levelTF;
    public TextMeshProUGUI getLevelTF() { return levelTF; }
    [SerializeField] TextMeshProUGUI powerTF;
    public TextMeshProUGUI getPowerTF() { return powerTF; }
    [SerializeField] Slider powerSlider;
    public Slider getPowerSlider() { return powerSlider; }

    [SerializeField] List<Image> traitImages;
    public List<Image> getTraitImages() { return traitImages; }
    public Image getTraitImageAt(int index) { return traitImages[index]; }

    public void setContainer(Cow theCow)
    {
        getNameTF().text = theCow.getName();
        //Debug.Log("name for " + theCow.getName());
        getGenTF().text = theCow.getGen().ToString();
        //Debug.Log("gen for " + theCow.gameObject + " set to " + theUI.getGenTF().text);
        getLevelTF().text = theCow.getLevel().ToString() + "/" + theCow.getMaxLevel();
        //Debug.Log("level for " + theCow.gameObject + " set to " + theUI.getLevelTF().text);
        getPowerTF().text = theCow.getPower().ToString();
        //Debug.Log("power for " + theCow.gameObject + " set to " + theUI.getPowerTF().text);
        getPowerSlider().maxValue = theCow.getMaxPower();
        getPowerSlider().DOValue(theCow.getPower(), 1.0f);

    }


    public void PopAnimation()
    {
        DOTween.CompleteAll();
        this.gameObject.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.1f, 0.1f, 0.0f), 0.5f, 1);

    }
}
