using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] FoodManager foodManager;
    [SerializeField] CowManager cowManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerMouse playerMouse;
    private void OnEnable()
    {
        Cow.cowEat += OnCowEat;

        PlayerMouse.mouseClickOn += OnMouseClickOn;
        PlayerMouse.mouseRelease += OnMouseRelease;
    }

    private void OnDisable()
    {
        Cow.cowEat -= OnCowEat;

        PlayerMouse.mouseClickOn -= OnMouseClickOn;
        PlayerMouse.mouseRelease -= OnMouseRelease;
    }

    void OnCowEat(Cow theCow, Food theFood)
    {
        theCow.setPower(theCow.getPower() + theFood.getPower());
        foodManager.DeleteFood(theFood.gameObject);
        //Debug.Log("the cow is " + theCow.gameObject);
        uiManager.UpdateCowUI(theCow);
    }

    void OnMouseClickOn(GameObject theObject)
    {
        //Debug.Log("mouse clicked something, process event");
        if(theObject.CompareTag("Food"))
        {
            if (playerMouse.getCurCow())
              playerMouse.setCurCow(null);

            playerMouse.setCurFood(theObject);
            List<GameObject> theCows = cowManager.getCurCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<CapsuleCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        else if(theObject.CompareTag("Cow"))
        {
            playerMouse.setCurCow(theObject);
            uiManager.ToggleUIGroup("Train");
        }
    }

    void OnMouseRelease()
    {
        //renable collision of curFood if any
        if (!playerMouse.getCurFood())
            return;
        List<GameObject> theCows = cowManager.getCurCows();
        for(int i = 0; i < theCows.Count; i++)
        {
            Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<CapsuleCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), false);
        }
        playerMouse.setCurFood(null);
        
    }

}
