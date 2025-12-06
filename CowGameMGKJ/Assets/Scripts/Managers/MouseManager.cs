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

    [SerializeField] GameObject curCosmetic = null;
    public GameObject getCurCosmetic() { return curCosmetic; }

    public void setCurCosmetic(GameObject theCosmetic) { curCosmetic = theCosmetic; }

    [SerializeField] MouseState curMouseState;
    public MouseState getCurMouseState() { return curMouseState; }

    public void setCurMouseState(MouseState state) {curMouseState = state; }

    [SerializeField] bool isHolding;
    public bool getIsHolding() { return isHolding; }
    public void setIsHolding(bool value) { isHolding = value; }

    //EVENTS
    public delegate void MouseClick(GameObject theObject, ClickType click);
    public static event MouseClick mouseClick;

    public delegate void MouseRelease();
    public static event MouseRelease mouseRelease;

    public delegate void MouseDeselect();
    public static event MouseDeselect mouseDeselect;
    // Start is called before the first frame update
    void Start()
    {
        curFood = null;
        curCow = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (curMouseState == MouseState.HOLD)
        {
            if (curFood && Input.GetMouseButton(0)) //Move this to the food manager script
                curFood.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(Camera.main.transform.position.z)));
            else if (curCosmetic && Input.GetMouseButton(0))
                curCosmetic.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(Camera.main.transform.position.z)));
            else if (curCow && Input.GetMouseButton(0))
            {
                Debug.Log("dragging cow");
                curCow.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(Camera.main.transform.position.z)));
            }
        }

        //Sending Click Types

        if (Input.GetMouseButtonDown(0))
            ClickMouse(ClickType.LEFT);
        else if (Input.GetMouseButtonDown(1))
            ClickMouse(ClickType.RIGHT);
       
        if(Input.GetMouseButton(0))
        {
            ClickMouse(ClickType.HOLD);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ClickMouse(ClickType.RELEASE);
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
        else
        {
            mouseDeselect?.Invoke();
        }
    }


    public IEnumerator HoldTimer()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (isHolding)
        {
            setCurMouseState(MouseState.HOLD);
        }
        else
        {
            setCurMouseState(MouseState.FREE);
        }
    }

}
