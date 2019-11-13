using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    [SerializeField] private float isGroundedCheckDistance = 1.0f;
    [SerializeField] private float respawnYOffset = 1;
    private Vector3 lastPoint = new Vector3(0,0,0);

    [Header("Fall Damage")]
    [SerializeField] private float fallMaxTime = 2;
    [SerializeField] private float fallDamageStartTime = 1;
    [SerializeField] private int fallDamageMax = 40;
    [SerializeField] private int fallDamageMin = 20;
    private float terminalFallingTimer = 0;

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

        //Check if on the ground
        CheckGrounded();

        //Damage player if they fall for too long and teleport them back to ground
        CheckFellTeleport();
    }
    
    private void CheckGrounded()
    {
        //Raycast to check for if grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, isGroundedCheckDistance))
        {
            _stateController.OnGround = true;
            lastPoint = hit.point;

            CheckFellLanding();
            terminalFallingTimer = 0;
        }
        else
        {
            _stateController.OnGround = false;

            if (downForce >= downForceTerminal)
                terminalFallingTimer += Time.deltaTime;
        }
    }

    private void CheckFellTeleport()
    {
        //If falling for max time, teleport back to last place on ground
        if (terminalFallingTimer >= fallMaxTime)
        {
            _rb.velocity = Vector3.zero;
            transform.position = lastPoint + new Vector3(0, respawnYOffset, 0);
            _stateController._playerAttributes.modifyHealth(-fallDamageMax);
            terminalFallingTimer = 0;
        }
    }

    private void CheckFellLanding()
    {
        if (terminalFallingTimer >= fallDamageStartTime)
        {
            float fallPercent = (terminalFallingTimer - fallDamageStartTime) / (fallMaxTime - fallDamageStartTime);
            int fallDamage = Mathf.RoundToInt(fallDamageMax * fallPercent + fallDamageMin * (1 - fallPercent));
            //Debug.Log("Percent: " + fallPercent + " | Damage: " + fallDamage + " | Timer: " + terminalFallingTimer);

            _stateController._playerAttributes.modifyHealth(-fallDamage);
            _stateController._movementComponent.inputInfluence = 0;
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
