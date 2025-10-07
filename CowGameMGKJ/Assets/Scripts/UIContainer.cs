using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    //TEXT
    [SerializeField] List<TextElement> textElements;
    public List<TextElement> getTextElements(){ return textElements; }
    public TextMeshProUGUI getTextElement(string ID)
    {
        foreach (TextElement element in textElements )
        {
            if (element.getElementName() == ID)
                return element.getTF();
        }

        return null;
    }

    public void setTextElement(string ID, string newText)
    {
        foreach (TextElement element in textElements)
        {
            if (element.getElementName() == ID)
                element.getTF().text = newText;
        }

    }
    [Serializable]
    public struct TextElement
    {
        [SerializeField] TextMeshProUGUI textTF;
        public TextMeshProUGUI getTF() { return textTF; }
        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }



    //SLIDERS
    [SerializeField] List<SliderElement> sliderElements;
    public List<SliderElement> getSliderElements() { return sliderElements; }
    public Slider getSliderElement(string ID)
    {
        foreach (SliderElement element in sliderElements)
        {
            if (element.getElementName() == ID)
                return element.getSlider();
        }

        return null;
    }

    public void setSliderElement(string ID, int newValue)
    {
        foreach (SliderElement element in sliderElements)
        {
            if (element.getElementName() == ID)
                element.getSlider().value = newValue;
        }

    }

    public void setSliderElementMax(string ID, int newMax)
    {
        foreach (SliderElement element in sliderElements)
        {
            if (element.getElementName() == ID)
            {
                element.getSlider().maxValue = newMax;
            }
        }

    }

    [Serializable]
    public struct SliderElement
    {
        [SerializeField] Slider slider;
        public Slider getSlider() { return slider; }
        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }



    //IMAGES
    [SerializeField] List<ImageElement> imageElements;
    public List<ImageElement> getImageElements() { return imageElements; }
    public Image getimageElement(string ID)
    {
        foreach (ImageElement element in imageElements)
        {
            if (element.getElementName() == ID)
                return element.getImage();
        }

        return null;
    }

    /*public void setImageElement(string ID, Image newImage)
    {
        foreach (ImageElement element in imageElements)
        {
            if (element.getElementName() == ID)
                element.getImage() 
        }

    }
    */



    [Serializable]
    public struct ImageElement
    {
        [SerializeField] Image image;
        public Image getImage() { return image; }
        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }

    //ANIMATIONS
    public void PopAnimation()
    {
        DOTween.CompleteAll();
        this.gameObject.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.1f, 0.1f, 0.0f), 0.5f, 1);

    }
}
