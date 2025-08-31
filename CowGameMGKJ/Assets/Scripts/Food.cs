using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int power;
    public int getPower() { return power; }

    [Header("Refs")]
    [SerializeField] SpriteRenderer thisSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
