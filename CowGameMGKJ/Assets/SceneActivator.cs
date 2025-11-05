using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneActivator : MonoBehaviour
{
    [SerializeField] GameState stateActivation;
    private void Start()
    {
        ArcController.inst.setGameState(stateActivation);
    }
}
