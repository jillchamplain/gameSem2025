using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class PlayerMouse : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject curFood = null;
    public GameObject getCurFood() { return curFood; }
    public void setCurFood(GameObject theFood) { curFood = theFood; }

    [SerializeField] GameObject curCow = null;
    public GameObject getCurCow() { return curCow; }
    public void setCurCow(GameObject theCow) { curCow = theCow; }

    //EVENTS
    public delegate void MouseClickOn(GameObject theObject);
    public static event MouseClickOn mouseClickOn;

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
            MouseClick();
        if (curFood && Input.GetMouseButton(0))
            curFood.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(Camera.main.transform.position.z)));
        else if (Input.GetMouseButtonUp(0))
            mouseRelease?.Invoke();
    }

    void MouseClick()
    {
        //Debug.Log("click");
        RaycastHit2D hit = (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero));
        if(hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject);
            mouseClickOn?.Invoke(hit.collider.gameObject);
        }
    }

}
