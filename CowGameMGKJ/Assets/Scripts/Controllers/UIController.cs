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
            ui.setCanvasGroup(value);
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
