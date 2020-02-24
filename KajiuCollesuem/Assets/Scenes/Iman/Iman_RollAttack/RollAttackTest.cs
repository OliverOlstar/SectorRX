using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttackTest : MonoBehaviour
{
    [SerializeField] private float SecBeforeJump = 1;
    [SerializeField] [Range(0, 1)] private float PushOnCol_DirToVelocityRatio = 1;
    [SerializeField] private float InitialPushForce = 600;
    [SerializeField] private float RollAttackTime = 4;
    [SerializeField] private float PushBackForce = 100;
    [SerializeField] private int Damage = 10;
    [SerializeField] private GameObject RollVisual;
    [SerializeField] private GameObject JumpToDirection;
    private bool MoveUp;
    private bool AttackActive;
    private float SecBeforeJumpTimer;
    private float RollAttackTimer;
    Rigidbody rb;
    CapsuleCollider capsule;
    SphereCollider sphere;
    private bool standUp = false;
    private Quaternion desiredRotQ;

    private Vector3 y;
    float singleStep;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        sphere = GetComponent<SphereCollider>();
        RollVisual.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if button pressed start attack
        if (Input.GetKey(KeyCode.Space))
        {
            RollAttackStart();
        }

        if (AttackActive)
        {
            InRollAttack();
        }

        if(standUp)
        {
            StandUp();
        }

        singleStep = 4 * Time.deltaTime;
    }
    //Initializing varieables and getting everything ready for start
    public void RollAttackStart()
    {
        MoveUp = true;
        AttackActive = true;
        SecBeforeJumpTimer = Time.time + SecBeforeJump;
        RollAttackTimer = Time.time + RollAttackTime;
        capsule.enabled = false;
        sphere.enabled = true;
        RollVisual.SetActive(true);
    }
    //while roll attack is active
    private void InRollAttack()
    {
        if (MoveUp)
        {
            //if seconds before jump is up jump
            if (SecBeforeJumpTimer <= Time.time)
            {
                MoveUp = false;
                //jump in the calculated direction with set force
                rb.AddForce((JumpToDirection.transform.position - transform.position).normalized * InitialPushForce);
            }
        }
        //check if the Attack is over or not
        if (RollAttackTimer <= Time.time)
        {
                RollAttackEnd();
        }
    }
    //function to call when ending attack
    private void RollAttackEnd()
    {
        RollVisual.SetActive(false);
        AttackActive = false;
        standUp = true;
        rb.freezeRotation = true;
        capsule.enabled = true;
        sphere.enabled = false;
    }

    private void StandUp()
    {
        
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized, singleStep,0);
        transform.rotation = Quaternion.LookRotation(newDirection);
        if(transform.forward == new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized)
        {
            rb.freezeRotation = false;
            rb.velocity = Vector3.zero;
            standUp = false;
        }
    }

    private Vector3 calcPushBackDir(GameObject enemy)
    {
        //calculate direction based on where enemy hit the player
        Vector3 dirRatio = ((enemy.gameObject.transform.position - transform.position).normalized * PushOnCol_DirToVelocityRatio);
        //calculate direction based on players movement direction
        Vector3 VelocityRatio = (rb.velocity.normalized * (1 - PushOnCol_DirToVelocityRatio));
        //add the 2 ratios together
        return dirRatio + VelocityRatio;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        //if collided object is in destroyable layer
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            IAttributes otherAttributes = other.gameObject.GetComponent<IAttributes>();

            //if object has IAttributes do damage
            if(otherAttributes != null)
            {
                otherAttributes.TakeDamage(Damage, calcPushBackDir(other.gameObject) * PushBackForce, this.gameObject);
            }
        }
    }

}
