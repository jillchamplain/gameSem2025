using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //Toggle Off UI Groups according to Logic Scene
    [SerializeField] public List<UIGroup> uiGroups;

    public void ToggleUIGroups(bool value)
    {
       
        foreach(UIGroup ui in uiGroups)
        {
            //Debug.Log("setting " + ui.gameObject + " to " + value);
            ui.setCanvasGroup(value);
        }
        
    }

    public UIGroup getUIGroup(string name)
    {
        foreach (UIGroup ui in uiGroups)
        {
            if (ui.getGroupName() == name)
            {
                return ui;
            }
        }
        return null;
    }

    public void setUIGroup(string theGroupName, bool isOn)
    {
        if (uiGroups.Count <= 0)
            return;
        for (int i = 0; i < uiGroups.Count; i++)
        {
            if (theGroupName == uiGroups[i].getGroupName())
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


    public void UICleanUp()
    {
        StartCoroutine(UIAnimCleanup());
    }
    IEnumerator UIAnimCleanup()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        DOTween.CompleteAll();
    }

}
