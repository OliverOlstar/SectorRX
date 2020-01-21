using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;

    [SerializeField] private int numberOfClicks = 0;
    [SerializeField] private float lastClickedTime = 0;
    [SerializeField] private float maxComboDelay = 0.9f;

    private float AttackStateReturnDelayLength = 0.2f;

    private bool onHolding = false;
    public float chargeTimer = 0f;
  


    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        Debug.Log("AttackState: Enter");
        //stateController._hitboxComponent.gameObject.SetActive(true); /* Handled by animation events */

        onHolding = false;
        CheckForAttack();
    }

    public override void Exit()
    {
        Debug.Log("AttackState: Exit");
        //stateController._hitboxComponent.gameObject.SetActive(false); /* Handled by animation events */
        stateController.AttackStateReturnDelay = Time.time + AttackStateReturnDelayLength;
        //stateController._hitboxComponent.gameObject.SetActive(false);
        numberOfClicks = 0;
        stateController._animHandler.ClearAttackBools();
    }

    public override Type Tick()
    {
        if (Time.time - lastClickedTime > maxComboDelay && onHolding == false)
        {
            return typeof(MovementState);
        }

        //CheckForAttack2();
        CheckForAttack();

        //Stunned
        if (stateController.Stunned)
        {
            return typeof(StunnedState);
        }

        //Respawn
        if (stateController.Respawn)
        {
            stateController.Respawn = false;
            stateController._animHandler.Respawn();
            return typeof(MovementState);
        }

        return null;
    }
    public bool attacking = false;
    private bool heldAttack = true;
    float animSpeed = 0f;
    Animation animmmm;
    

    //private void CheckForAttack2()
    //{
    //    // When Heavy Attack has been held for a set duration
    //    if (stateController.ctx.performed)
    //    {
    //        Debug.Log("Fuck");
    //    }
    //    // If Heavy Attack input is withdrawn before reaching duration
    //    else if (stateController.ctx.canceled)
    //    {
    //        Debug.Log("Bitch");
    //    }
    //}

    

    private void CheckForAttack()
    {
        if (numberOfClicks <= 2)
        {
            // On Release Heavy (Called Once)
            if ((stateController.heavyAttackinput == 0 || chargeTimer >= 2) && onHolding == true)
            {
                ClearInputs();
                lastClickedTime = Time.time;

                numberOfClicks++;

                onHolding = false;

                animSpeed = 1f;
            }

            // On Holding Heavy
            else if (onHolding)
            {
                chargeTimer += Time.deltaTime;

                if (chargeTimer >= 0.1f)
                {
                    string animBoolName = "Vertical" + (numberOfClicks + 1).ToString();
                    animmmm[animBoolName].speed = 0f;
                }    
                
                // Animation speed = 0;
            }

            // On Pressed Heavy (Called Once)
            else if (stateController.heavyAttackinput == 1)
            {
                stateController._animHandler.ClearAttackBools();
                string boolName = "Triangle" + (numberOfClicks + 1).ToString();
                stateController._animHandler.StartAttack(boolName);

                chargeTimer = 0;
                onHolding = true;
            }

            // On Pressed Light Attack (Called Once)
            else if (stateController.lightAttackinput == 1)
            {
                lastClickedTime = Time.time;
                numberOfClicks++;


                stateController._animHandler.ClearAttackBools();
                string boolName = "Square" + (numberOfClicks).ToString();
                stateController._animHandler.StartAttack(boolName);
                ClearInputs();
            }
        }
    }


    private void ClearInputs()
    {
        stateController.lightAttackinput = -1.0f;
        stateController.heavyAttackinput = -1.0f;
    }
}
