using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererAsset : MonoBehaviour
{
    [SerializeField] string ID;
    public string getID() { return ID; }
    [SerializeField] SpriteRenderer renderer;
    public SpriteRenderer getSpriteRenderer() { return renderer; }
}
