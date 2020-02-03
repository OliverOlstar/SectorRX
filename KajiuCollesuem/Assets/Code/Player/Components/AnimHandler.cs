using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandler : MonoBehaviour
{
    ////Animation Handling for the player model - Oliver

    //[HideInInspector] public PlayerStateController _stateController;
    //public Animator _anim;

    //[HideInInspector] public int attackState = 0;

    //[SerializeField] private float _rotationDampening = 11f;
    //[SerializeField] private float _animSpeedDampening = 5f;

    //private float offGroundTimer = 0;

    //void Awake()
    //{
    //    _stateController = GetComponentInParent<PlayerStateController>();
    //    _anim = GetComponentInChildren<Animator>();

    //    // TODO Temp (animation's origins are slightly off (my b))
    //    transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
    //}
    
    //void Update()
    //{
    //    Vector3 moveDirection = new Vector3(_stateController.LastMoveDirection.x, 0, _stateController.LastMoveDirection.y);

    //    //If Movement isn't disabled
    //    if (_stateController._movementComponent.disableMovement == false)
    //    {
    //        //If Movement Inputed
    //        if (moveDirection != Vector3.zero)
    //        {
    //            //Lerp Player Model Rotation
    //            Vector3 dir = moveDirection;

    //            if (transform.forward.normalized == -moveDirection.normalized)
    //                dir += new Vector3(Random.value / 15f, 0, Random.value / 15f);

    //            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * _rotationDampening);
    //        }

    //        //Set Anim
    //        _anim.SetFloat("Speed", _stateController._rb.velocity.magnitude / _stateController._movementComponent.maxSpeed);
    //    }
    //    else
    //    {
    //        //Set Anim Speed Stopping
    //        _anim.SetFloat("Speed", Mathf.Lerp(_anim.GetFloat("Speed"), 0.1f, Time.deltaTime * _animSpeedDampening));
    //    }

    //    //Falling & OnGround
    //    _anim.SetBool("OnGround", _stateController.OnGround);

    //    if (_stateController.OnGround)
    //    {
    //        _anim.SetBool("Falling", false);
    //        offGroundTimer = 0;
    //    }
    //    else
    //    {
    //        offGroundTimer += Time.deltaTime;
    //        if (offGroundTimer >= 0.4f)
    //            _anim.SetBool("Falling", true);
    //    }
    //}

    //public void StartDodge(/*Vector3 pFacing*/)
    //{
    //    //transform.forward = pFacing;
    //    _anim.SetBool("Dodge", true);
    //    _anim.SetFloat("Speed", 0);
    //}

    //public void StopDodge()
    //{
    //    _anim.SetBool("Dodge", false);
    //}

    //public void StartJump(/*Vector3 pFacing*/)
    //{
    //    //transform.forward = pFacing;
    //    _anim.SetTrigger("Jump");
    //    _anim.SetFloat("Speed", 0);
    //}

    //public void ResetJump()
    //{
    //    _anim.ResetTrigger("Jump");
    //}

    //public void SetOnGround(bool pOnGround)
    //{
    //    _anim.SetBool("OnGround", pOnGround);
    //}

    //public void Dead()
    //{
    //    _anim.SetBool("IsDead", true);
    //    _anim.SetTrigger("Died");
    //}

    //public void StartAttack(string pBoolName)
    //{
    //    _anim.SetBool(pBoolName, true);
    //}

    //public void StopAttacking()
    //{
    //    if (attackState != 1)
    //        return;

    //    LeaveAttackState();
    //}

    //public void LeaveAttackState()
    //{
    //    attackState = 2;
    //    _anim.SetInteger("Combo", 0);
    //}

    //public void ClearAttackBools()
    //{
    //    _anim.SetBool("Square1", false);
    //    _anim.SetBool("Square2", false);
    //    _anim.SetBool("Square3", false);
    //    _anim.SetBool("Triangle1", false);
    //    _anim.SetBool("Triangle2", false);
    //    _anim.SetBool("Triangle3", false);
    //}

    //public void Stunned(bool pLeft)
    //{
    //    _anim.SetBool("ReactLeft", pLeft);
    //    _anim.SetTrigger("React");
    //    _stateController.Stunned = true;
    //}

    //public void modifyAnimSpeed(float pValue)
    //{
    //    _anim.speed += pValue;
    //}

    //public void setAnimSpeed(float pValue)
    //{
    //    _anim.speed = pValue;
    //}

    //public void Respawn()
    //{
    //    _stateController.Stunned = false;
    //    _anim.SetTrigger("Respawn");
    //    _anim.SetBool("IsDead", false);
    //}
}
