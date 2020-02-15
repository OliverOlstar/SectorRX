using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerEntry : MonoBehaviour
{
    [SerializeField] private Panels _AddPlayer;

    void Update()
    {   
        //if (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        //{
        //    _AddPlayer.PlayerJoins();
        //    Debug.Log("First Player Added");
        //}
    }
}
