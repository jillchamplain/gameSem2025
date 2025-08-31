using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] FoodManager foodManager;
    [SerializeField] CowManager cowManager;
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
        theCow.setCurrentPower(theCow.getCurrentPower() + theFood.getPower());
        foodManager.DeleteFood(theFood.gameObject);
    }

    void OnMouseClickOn(GameObject theObject)
    {
        //Debug.Log("mouse clicked something, process event");
        if(theObject.CompareTag("Food"))
        {
            playerMouse.setCurFood(theObject);
            List<GameObject> theCows = cowManager.getCurCows();
            for (int i = 0; i < theCows.Count; i++)
            {
                Physics2D.IgnoreCollision(playerMouse.getCurFood().GetComponent<CapsuleCollider2D>(), theCows[i].GetComponent<BoxCollider2D>(), true);
            }
        }

        else if(theObject.CompareTag("Cow"))
        {
            Debug.Log("Pet the cow!");
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
