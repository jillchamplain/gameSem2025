using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticle : MonoBehaviour
{
    public void AnimationOver()
    {
        Destroy(this.gameObject);
    }
}
