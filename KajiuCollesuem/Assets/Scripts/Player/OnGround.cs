using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    [SerializeField] private float isGroundedCheckDistance = 1.0f;
    [SerializeField] private float respawnYOffset = 1;
    private Vector3 lastPoint = new Vector3(0,0,0);

    [Header("Damage")]
    [SerializeField] private float isFallingYDifference = 1.0f;
    [SerializeField] private int fallGroundCheckDis = 1;
    [SerializeField] private int fallDamage = 30;
    private bool damageOnLanding = false;

    private PlayerStateController _stateController;
    private Rigidbody _rb;

    private void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        CheckFell();
    }
    
    private void CheckGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 0.1f, false);

        //Raycast to check for if grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, isGroundedCheckDistance))
        {
            _stateController.OnGround = true;
            lastPoint = hit.point;

             if (damageOnLanding)
            {
                _stateController._playerAttributes.modifyHealth(-fallDamage);
                damageOnLanding = false;
            }
        }
        else
        {
            _stateController.OnGround = false;
        }
    }

    private void CheckFell()
    {
        if (damageOnLanding == false && transform.position.y - respawnYOffset - lastPoint.y <= -isFallingYDifference)
        {
            if (Physics.Raycast(transform.position, Vector3.down, fallGroundCheckDis) == false)
            {
                _rb.velocity = Vector3.zero;
                transform.position = lastPoint + new Vector3(0, respawnYOffset, 0);
            }
            damageOnLanding = true;
        }
    }
}
