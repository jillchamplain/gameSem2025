using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [SerializeField] string groupName;
    public string getGroupName() { return groupName; }
    [SerializeField] CanvasGroup canvasGroup;
    public CanvasGroup getCanvasGroup() { return canvasGroup; }

    [SerializeField] List<UIContainer> uiElements;
    public UIContainer getElement(string name)
    {
        foreach (UIContainer ui in uiElements)
        {
            if (ui.getContainerName() == name)
            {
                return ui;
            }
        }
        return null;
    }

    public UIContainer getElement(int index)
    {
        for(int i = 0; i < uiElements.Count; i++)
        {
            if (i == index)
                return uiElements[i];
            //Debug.Log(uiElements[i]);
        }
        return null;
    }
}
