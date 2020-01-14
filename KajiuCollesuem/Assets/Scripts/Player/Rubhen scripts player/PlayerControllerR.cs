using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerR : MonoBehaviour
{
    //movement variables
    //private Vector3 moveVector = Vector3.zero;
    //private Vector3 lastMove;
    //public float speed = 8;

    //jumping variables
    //public float jumpForce = 8;
    //public float gravity = 25;
    //private float verticalVelocity;
    //private Vector3 normal;
    //private Vector3 reflection;
    //private Vector3 velocity;

    //character controller for the player
    //public CharacterController myController;

    //Combos related
    public Animator anim;
    public int numberOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;

    //public Transform modelPivot;


    // Start is called before the first frame update
    void Start()
    {
        //anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //movement based on axis
        //moveVector = transform.TransformDirection(moveVector); //transforming direction
        //transform.localRotation = Quaternion.Euler(moveVector);
        //moveVector = Vector3.ClampMagnitude(moveVector, 1f); //making the character move same speed in diagonals

        //if (myController.isGrounded)
        //{
        //    verticalVelocity = -1f; //constant force down y to keep object forced to ground during inclined planes

        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        verticalVelocity = jumpForce; //verticalVelocity is only affected by the jumpForce value
        //    }
        //}

        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; //at all other times gravity is constantly being applied to the y
        //    moveVector = lastMove; //setting new vector value every frame if no jump
        //}

        //moveVector.y = 0; //setting the y to zero
        //moveVector.Normalize(); //normalizing the vector as a whole but only caring about x and z
        //moveVector *= speed; //speed is applied to this vector to move it
        //moveVector.y = verticalVelocity; //vertical velocity affect y but is dependent on the jump

        //if (numberOfClicks > 0)
        //{
        //    moveVector = Vector3.zero;
        //}

        //myController.Move(moveVector * Time.deltaTime); //using the move function on character controller
        
        
        //lastMove = moveVector;

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            anim.SetBool("Square1", false);
            anim.SetBool("Square2", false);
            anim.SetBool("Square3", false);
            anim.SetBool("Triangle1", false);
            anim.SetBool("Triangle2", false);
            anim.SetBool("Triangle3", false);
        }


        if (numberOfClicks == 0)
        {
            //anim.speed = 2.0f;

            if (Input.GetMouseButtonUp(0))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", true);
                anim.SetBool("Square2", false);
                anim.SetBool("Square3", false);
                anim.SetBool("Triangle1", false);
                anim.SetBool("Triangle2", false);
                anim.SetBool("Triangle3", false);
            }

            else if (Input.GetMouseButtonUp(1))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", false);
                anim.SetBool("Square2", false);
                anim.SetBool("Square3", false);
                anim.SetBool("Triangle1", true);
                anim.SetBool("Triangle2", false);
                anim.SetBool("Triangle3", false);
            }
        }

        else if (numberOfClicks == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", false);
                anim.SetBool("Square2", true);
                anim.SetBool("Square3", false);
                anim.SetBool("Triangle1", false);
                anim.SetBool("Triangle2", false);
                anim.SetBool("Triangle3", false);
            }

            else if (Input.GetMouseButtonUp(1))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", false);
                anim.SetBool("Square2", false);
                anim.SetBool("Square3", false);
                anim.SetBool("Triangle1", false);
                anim.SetBool("Triangle2", true);
                anim.SetBool("Triangle3", false);
            }
        }
        else if (numberOfClicks == 2)
        {
            if (Input.GetMouseButtonUp(0))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", false);
                anim.SetBool("Square2", false);
                anim.SetBool("Square3", true);
                anim.SetBool("Triangle1", false);
                anim.SetBool("Triangle2", false);
                anim.SetBool("Triangle3", false);
            }

            else if (Input.GetMouseButtonUp(1))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                anim.SetBool("Square1", false);
                anim.SetBool("Square2", false);
                anim.SetBool("Square3", false);
                anim.SetBool("Triangle1", false);
                anim.SetBool("Triangle2", false);
                anim.SetBool("Triangle3", true);
            }
        }

            //numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);
            
            //anim.speed = 1.0f;
            
        }
            //if (numberOfClicks >= 2)
            //{
            //    anim.SetBool("Square2", true);
            //}

            //else
            //{
            //    anim.SetBool("Square1", false);
            //    numberOfClicks = 0;
            //}

            //if (numberOfClicks >= 3)
            //{
            //    anim.SetBool("Square3", true);
            //}

            //else
            //{
            //    anim.SetBool("Square2", false);
            //    numberOfClicks = 0;
            //}

            //anim.SetBool("Square1", false);
            //anim.SetBool("Square2", false);
            //anim.SetBool("Square3", false);
            //numberOfClicks = 0;
    }

    //public void returnSquare1()
    //{
    //    if(numberOfClicks >= 2)
    //    {
    //        anim.SetBool("Square2", true);
    //    }
        
    //    else
    //    {
    //        anim.SetBool("Square1", false);
    //        numberOfClicks = 0;
    //    }
    //}

    //public void returnSquare2()
    //{
    //    if (numberOfClicks >= 3)
    //    {
    //        anim.SetBool("Square3", true);
    //    }

    //    else
    //    {
    //        anim.SetBool("Square2", false);
    //        numberOfClicks = 0;
    //    }
    //}

    //public void returnSquare3()
    //{
    //    anim.SetBool("Square1", false);
    //    anim.SetBool("Square2", false);
    //    anim.SetBool("Square3", false);
    //    numberOfClicks = 0;
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    normal = hit.normal; //setting the hit normal as a variable
    //    Vector3 reflection = Vector3.Reflect(moveVector, normal);

    //    if (!myController.isGrounded && normal.y < 0.1f)
    //    {
    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            verticalVelocity = jumpForce * 0.75f; //75% of original ground jump so not too powerful off the wall
    //            moveVector = reflection * 3 * speed; //jumping on the direction of the normal to achieve wall jump
    //        }
    //    }

    //}


