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

    [Space]
    public float inputInfluenceGrounded = 1.0f;
    public float inputInfluenceInAir = 0.7f;

    [Space]
    [SerializeField] private float downForceRate = 6f;
    [SerializeField] private float downForceTerminal = 4f;
    private float downForce = 0;

    private PlayerStateController _stateController;
    private Rigidbody _rb;

    private void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Falling Force (Add extra force to falling to make falling feel better)
        //if (_stateController._movementComponent.disableMovement == false)
            FallingForce();

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
                _stateController._movementComponent.inputInfluence = 0;
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
        //If falling do damage
        if (damageOnLanding == false && transform.position.y - respawnYOffset - lastPoint.y <= -isFallingYDifference)
        {
            //If distance to ground is to far teleport player back to their last point OnGround
            if (Physics.Raycast(transform.position, Vector3.down, fallGroundCheckDis) == false)
            {
                _rb.velocity = Vector3.zero;
                transform.position = lastPoint + new Vector3(0, respawnYOffset, 0);
            }
            damageOnLanding = true;
        }
    }

    public void FallingForce()
    {
        //Change the amount of influence player input has on the player movement based on wether he is grounded or not
        if (_stateController._movementComponent.OnGround)
        {
            _stateController._movementComponent.targetInputInfluence = inputInfluenceGrounded;
            downForce = 0;
        }
        else
        {
            _stateController._movementComponent.targetInputInfluence = inputInfluenceInAir;
            //Add force downwards which adds ontop of gravity
            if (downForce < downForceTerminal)
                downForce += downForceRate * Time.deltaTime;
            else
                downForce = downForceTerminal;
        }

        _stateController._rb.AddForce(Vector3.down * Mathf.Pow(downForce, 2) * _stateController._rb.mass);
    }
}
