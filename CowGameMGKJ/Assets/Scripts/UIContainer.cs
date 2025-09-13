using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
}
