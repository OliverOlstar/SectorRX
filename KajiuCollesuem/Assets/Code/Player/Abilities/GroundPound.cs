using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Aerial Slam attack for Lizzy Scales (Godzilla character)*/

public class GroundPound : MonoBehaviour
{
    //Variables to have player go to the ground.
    public bool doGroundPound;
    public bool isGroundPounding;
    private Rigidbody _PlayerRB;
    [SerializeField] private float _StopTime = 0.2f;
    [SerializeField] private float _DropForce = 15.0f;
    MovementComponent _PMovement;
    OnGroundComponent _PFallForce;

    // Start is called before the first frame update
    void Start()
    {
        _PMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>();
        _PFallForce = GameObject.FindGameObjectWithTag("Player").GetComponent<OnGroundComponent>();
        _PlayerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //If key is pressed and not on the ground, begin executing the attack.
        if(Input.GetKeyDown(KeyCode.C) && !isGroundPounding)
        {
            //if(!_PMovement.OnGround)
            //{
            //    doGroundPound = true;
            //    isGroundPounding = true;
            //}
        }
    }

    private void FixedUpdate()
    {
        //Execute attack.
        if(doGroundPound)
        {
            GroundPoundAttack();
            _PMovement.disableMovement = true;
        }
        doGroundPound = false;
        isGroundPounding = false;
        _PMovement.disableMovement = false;
    }

    //Have character stop in mid air, then drop straight down.
    void GroundPoundAttack()
    {
        StopAndSpin();
        StartCoroutine("DropAndSmash");
    }

    //Character waits for a specified time before dropping straight down.
    IEnumerator DropAndSmash()
    {
        yield return new WaitForSeconds(_StopTime);
        _PlayerRB.AddForce(Vector3.down * _DropForce, ForceMode.Impulse);
    }

    //All rigidbody forces are turned off.
    void StopAndSpin()
    {
        ClearForces();
        _PlayerRB.useGravity = false;
        //_PFallForce._downForce = 0;
    }

    //Sets character's velocity and angular velocity to zero.
    void ClearForces()
    {
        _PlayerRB.velocity = Vector3.zero;
        _PlayerRB.angularVelocity = new Vector3 (0, 0, 0);
    }
}