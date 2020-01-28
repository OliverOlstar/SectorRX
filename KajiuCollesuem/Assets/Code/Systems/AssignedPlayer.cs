using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignedPlayer : MonoBehaviour
{
    public PlayerInput player;
    public bool hasInput;

    private void Start()
    {
        hasInput = true;
    }
}