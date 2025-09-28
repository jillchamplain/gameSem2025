using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int power;
    public int getPower() { return power; }
    [SerializeField] bool isSelected;
    public bool getSelected() { return isSelected; }
    public void setSelected(bool status) { isSelected = status; }

    [Header("Refs")]
    [SerializeField] SpriteRenderer thisSprite;
}
