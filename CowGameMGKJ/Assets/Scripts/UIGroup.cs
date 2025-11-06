using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGroup : MonoBehaviour
{
    [SerializeField] string groupName;
    public string getGroupName() { return groupName; }
    [SerializeField] CanvasGroup canvasGroup;
    public CanvasGroup getCanvasGroup() { return canvasGroup; }
    public void setCanvasGroup(bool value)
    {
        //Debug.Log(gameObject + "is setting elements to" + value);
        foreach (UIContainer container in uiContainers)
        {
            setContainer(container.getContainerName(), value);
        }
        gameObject.SetActive(value);
    }


    [SerializeField] List<UIContainer> uiContainers;

    public List<UIContainer> getContainers() { return uiContainers; }
    public UIContainer getContainer(string name)
    {
        foreach (UIContainer container in uiContainers)
        {
            if (container.getContainerName() == name)
            {
                return container;
            }
        }
        return null;
    }

    public UIContainer getContainer(int index)
    {
        for (int i = 0; i < uiContainers.Count; i++)
        {
            if (i == index)
                return uiContainers[i];
            //Debug.Log(uiElements[i]);
        }
        return null;
    }
    public void setContainer(string name, bool value)
    {
        foreach(UIContainer container in uiContainers)
        {
            if (container.getContainerName() == name)
                container.gameObject.SetActive(value);
        }
    }
}
