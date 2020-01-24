using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerEventSystem : MonoBehaviour
{
    private GameObject currentButton;
    private AxisEventData currentAxis;

    //Timer values for controller inputs
    public float timeBetweenInputs = 0.15f; //Delay, in seconds, before another input is allowed
    [Range(0,1)]
    public float deadZone = 0.15f;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            currentAxis = new AxisEventData(EventSystem.current);
            currentButton = EventSystem.current.currentSelectedGameObject;

            //Move Up
            if(Input.GetAxisRaw("Vertical") > deadZone)
            {
                currentAxis.moveDir = MoveDirection.Up;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            //Move Down
            else if(Input.GetAxisRaw("Vertical") > -deadZone)
            {
                currentAxis.moveDir = MoveDirection.Down;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            //Move Right
            else if(Input.GetAxisRaw("Horizontal") > deadZone)
            {
                currentAxis.moveDir = MoveDirection.Right;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            //Move Left
            else if(Input.GetAxisRaw("Horizontal") > -deadZone)
            {
                currentAxis.moveDir = MoveDirection.Left;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            timer = timeBetweenInputs;
        }

        //Countdown timer between inputs
        timer -= Time.fixedDeltaTime;
    }
}
