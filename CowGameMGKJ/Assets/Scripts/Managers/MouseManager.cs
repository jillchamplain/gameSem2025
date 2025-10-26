using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MouseManager : Manager
{
    [Header("Refs")]
    [SerializeField] GameObject curFood = null;
    public GameObject getCurFood() { return curFood; }
    public void setCurFood(GameObject theFood) { curFood = theFood; }

    [SerializeField] GameObject curCow = null;
    public GameObject getCurCow() { return curCow; }
    public void setCurCow(GameObject theCow) { curCow = theCow; }


    //EVENTS
    public delegate void MouseClick(GameObject theObject, ClickType click);
    public static event MouseClick mouseClick;

    public delegate void MouseRelease();
    public static event MouseRelease mouseRelease;
    // Start is called before the first frame update
    void Start()
    {
        curFood = null;
        curCow = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickMouse(ClickType.LEFT);
        else if (Input.GetMouseButtonDown(1))
            ClickMouse(ClickType.RIGHT);
        if (curFood && Input.GetMouseButton(0)) //Move this to the food manager script
            curFood.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(Camera.main.transform.position.z)));
        else if (Input.GetMouseButtonUp(0))
        {
            mouseRelease?.Invoke();
        }
    }

    void ClickMouse(ClickType type)
    {
        //Debug.Log("click");
        RaycastHit2D hit = (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero));
        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject);

            mouseClick?.Invoke(hit.collider.gameObject, type);
        }
    }

}
