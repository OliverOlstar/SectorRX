using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public GameObject playerObject;
    Rigidbody rigid;

    public bool movementActive = false;

    KeyCode UpKey = KeyCode.W;
    KeyCode DownKey = KeyCode.S;
    KeyCode LeftKey = KeyCode.A;
    KeyCode RightKey = KeyCode.D;
    public KeyCode ActionInput = KeyCode.Space;

    public float moveSpeed = 0f;
    public float rotateSpeed = 0f;

    bool forwardInput = false;
    bool backwardInput = false;
    bool leftInput = false;
    bool rightInput = false;
    public bool action = false;



    void Start()
    {
        //playerObject = GameObject.FindGameObjectWithTag("Player");
        rigid = playerObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (movementActive)
        {
            MovePlayerForward();
            //MovePlayerRight();
            //RotatePlayerOnYaxis();
        }
    }

    void GetInput()
    {
        if (movementActive)
        {
            forwardInput = Input.GetKey(UpKey);
            backwardInput = Input.GetKey(DownKey);
            leftInput = Input.GetKey(LeftKey);
            rightInput = Input.GetKey(RightKey);
        }

        action = Input.GetKeyDown(ActionInput);
    }

    void MovePlayerForward()
    {
        float forceToAdd = 0f;
        float horForceToAdd = 0f;
        Vector3 targetPos = Vector3.zero;
        Vector3 forward = Vector3.zero;
        Vector3 right = Vector3.zero;
        Vector3 currentPos = Vector3.zero;

        currentPos = playerObject.transform.position;
        forward = playerObject.transform.forward;
        right = playerObject.transform.right;

        if (forwardInput || backwardInput)
        {
            forceToAdd += Time.fixedDeltaTime;
            forceToAdd = Mathf.Clamp01(forceToAdd);
            if (backwardInput)
            {
                forceToAdd *= -1;
            }
        }

        //targetPos = currentPos + (forward * (forceToAdd * moveSpeed));

        //forceToAdd = 0;
        //currentPos = targetPos;

        if (leftInput || rightInput)
        {
            horForceToAdd += Time.fixedDeltaTime;
            horForceToAdd = Mathf.Clamp01(horForceToAdd);
            if (leftInput)
            {
                horForceToAdd *= -1;

            }
        }

        targetPos = currentPos + ((right * (horForceToAdd * moveSpeed)) + (forward * (forceToAdd * moveSpeed)));

        rigid.MovePosition(targetPos);
    }

    void MovePlayerRight()
    {
        float forceToAdd = 0f;
        Vector3 targetPos = Vector3.zero;
        Vector3 direction = Vector3.zero;
        Vector3 currentPos = Vector3.zero;

        currentPos = playerObject.transform.position;
        direction = playerObject.transform.right;

        if (leftInput || rightInput)
        {
            forceToAdd += Time.fixedDeltaTime;
            forceToAdd = Mathf.Clamp01(forceToAdd);
            if (leftInput)
            {
                forceToAdd *= -1;

            }
        }

        targetPos = currentPos + (direction * (forceToAdd * moveSpeed));

        rigid.MovePosition(targetPos);
    }

    void RotatePlayerOnYaxis()
    {
        float rotationToAdd = 0f;
        Vector3 targetRotation = Vector3.zero;
        Vector3 currentRotation = Vector3.zero;
        Vector3 axis = Vector3.zero;

        currentRotation = playerObject.transform.rotation.eulerAngles;
        axis = playerObject.transform.up;

        if (leftInput || rightInput)
        {
            rotationToAdd += Time.fixedDeltaTime;
            rotationToAdd = Mathf.Clamp01(rotationToAdd);
            if (leftInput)
            {
                rotationToAdd *= -1;
            }
        }

        targetRotation = currentRotation + (axis * (rotationToAdd * rotateSpeed));

        Quaternion target = Quaternion.Euler(targetRotation);

        rigid.MoveRotation(target);
    }
}
