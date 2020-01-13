using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAttackTest : MonoBehaviour
{
    [SerializeField] private float SecBeforeJump = 1;
    [SerializeField] [Range(0, 1)] private float PushOnCol_DirToVelocityRatio = 1;
    [SerializeField] private float JumpForce = 600;
    [SerializeField] private float InitialPushForce = 1;
    [SerializeField] private float moveUpSpeed;
    [SerializeField] private float RollAttackTime = 4;
    [SerializeField] private float PushBackForce = 100;
    [SerializeField] private int Damage = 10;
    [SerializeField] private int RotateDamping = 10;
    [SerializeField] private LayerMask DestroyableLayer;
    [SerializeField] private GameObject RollVisual;
    [SerializeField] private GameObject JumpToDirection;
    private bool MoveUp;
    private bool AttackActive;
    private float SecBeforeJumpTimer;
    private float RollAttackTimer;
    Rigidbody rb;
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
    }

    // Update is called once per frame
    void Update()
    {
        //if button pressed start attack
        if (Input.GetKey(KeyCode.Space))
        {
            RollAttackStart();
        }

        if (Input.GetKey(KeyCode.B))
        {
            StandUp();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            print(new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized);
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
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
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
                rb.AddForce((JumpToDirection.transform.position - transform.position).normalized * JumpForce);
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
        //rb.isKinematic = true;
        RollVisual.SetActive(false);
        AttackActive = false;
        standUp = true;
        rb.freezeRotation = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<SphereCollider>().enabled = false;
        //transform.forward = rb.velocity;
        //rb.velocity = Vector3.zero;
        y = rb.velocity;
        print(new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized);
        
    }

    private void StandUp()
    {
        if (transform.rotation != desiredRotQ)
        {
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(rb.velocity.x, 0, rb.velocity.z).normalized, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            rb.freezeRotation = false;
            transform.rotation = desiredRotQ;
            standUp = false;
            print("pls");
        }
    }

    private Vector3 calcPushBackDir(GameObject enemy)
    {
        Vector3 dirRatio = ((enemy.gameObject.transform.position - transform.position).normalized * PushOnCol_DirToVelocityRatio);
        Vector3 VelocityRatio = (rb.velocity.normalized * (1 - PushOnCol_DirToVelocityRatio));
        Vector3 final = dirRatio + VelocityRatio;
        return final;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //if collided object is in destroyable layer
        if(collision.gameObject.layer == DestroyableLayer)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(calcPushBackDir(collision.gameObject) * PushBackForce);
            IAttributes cIA = collision.gameObject.GetComponent<IAttributes>();
            //if object has IAttributes do damage
            if(cIA != null)
            {
                cIA.TakeDamage(Damage, false);
            }
        }
    }

}
