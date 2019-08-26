using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    [SerializeField] private float isGroundedCheckDistance = 1.0f;
    [SerializeField] private float isFallingYDifference = 1.0f;
    [SerializeField] private float respawnYOffset = 1;
    private Vector3 lastPoint = new Vector3(0,0,0);

    [Space]
    [SerializeField] private int fallDamage = 30;

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
        }
        else
        {
            _stateController.OnGround = false;
        }
    }

    private void CheckFell()
    {
        if (transform.position.y - respawnYOffset - lastPoint.y <= -isFallingYDifference)
        {
            _rb.velocity = Vector3.zero;
            transform.position = lastPoint + new Vector3(0, respawnYOffset, 0);
            _stateController._playerAttributes.modifyHealth(-fallDamage);
        }
    }
}
