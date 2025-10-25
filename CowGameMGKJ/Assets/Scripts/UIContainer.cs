using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{
    [SerializeField] string containerName;
    public string getContainerName() { return containerName; }

    public virtual void setContainer(Cow theCow) { }


    //CANVAS GROUPS > More complicated ui elements i dont wanna deal with 
    [SerializeField] List<CanvasElement> canvasElements;
    public List<CanvasElement> getCanvasElements() { return canvasElements; }
    public CanvasGroup getCanvasElement(string ID)
    {
        foreach(CanvasElement element in canvasElements)
        {
            if (element.getElementName() == ID)
                return element.getCanvasGroup();
        }
        return null;
    }
    public void setCanvasElement(string ID, bool value)
    {
        foreach(CanvasElement element in canvasElements)
        {
            if(element.getElementName() == ID)
            {
                element.getCanvasGroup().interactable = value;
                element.getCanvasGroup().blocksRaycasts = value;
                if (value)
                    element.getCanvasGroup().alpha = 1.0f;
                else
                    element.getCanvasGroup().alpha = 0.0f;
            }
        }
    }

    [Serializable]
    public struct CanvasElement
    {
        [SerializeField] CanvasGroup canvasGroup;
        public CanvasGroup getCanvasGroup() { return canvasGroup; }
        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }

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

    public void setImageElement(string ID, Sprite newImage)
    {
        foreach (ImageElement element in imageElements)
        {
            if (element.getElementName() == ID)
                element.getImage().sprite = newImage;
        }

    }

    [Serializable]
    public struct ImageElement
    {
        [SerializeField] Image image;
        public Image getImage() { return image; }
        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }

    //BUTTONS
    [SerializeField] List<ButtonElement> buttonElements;
    public List<ButtonElement> getButtonElements() { return buttonElements; }
    public Button getButtonElement(string ID)
    {
        foreach (ButtonElement element in buttonElements)
        {
            if (element.getElementName() == ID)
                return element.getButton();
        }

        return null;
    }

    public void setButtonElement(string ID, string newButtonText)
    {
        foreach (ButtonElement element in buttonElements)
        {
            if (element.getElementName() == ID)
            {
                element.getButton().GetComponentInChildren<Text>().text = newButtonText;
                element.setButtonTF(newButtonText);
            }
        }

    }



    [Serializable]
    public struct ButtonElement
    {
        [SerializeField] Button button;
        public Button getButton() { return button; }


        [SerializeField] TextMeshProUGUI buttonTF;
        public TextMeshProUGUI getButtonTF() { return buttonTF; }
        public void setButtonTF(string newButtonText) { buttonTF.text = newButtonText; }


        [SerializeField] string elementName;
        public string getElementName() { return elementName; }
    }

    //ANIMATIONS
    public void PopAnimation()
    {
        
        this.gameObject.GetComponent<RectTransform>().DOPunchScale(new Vector3(0.1f, 0.1f, 0.0f), 0.5f, 1);
    }
}
