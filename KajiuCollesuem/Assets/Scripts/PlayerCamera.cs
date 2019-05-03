using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _CameraTansform;
    private Transform _ParentTransform;
    private Vector3 _LocalRotation;

    public float CameraDistance = 6f;
    public float MouseSensitivity = 4f;
    public float TurnDampening = 10f;
    public float CameraMinHeight = -20f;
    public float CameraMaxHeight = 90f;

    public bool CameraDisabled = false;


    void Start()
    {
        //Getting Transforms
        _CameraTansform = transform;
        _ParentTransform = transform.parent;

        //Locking cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Setting camera distance
        _CameraTansform.localPosition = new Vector3(0f, 0f, CameraDistance * -1f);
    }


    void Update()
    {
        //Locking and unlocking cursor
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
            CameraDisabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Cursor.lockState = CursorLockMode.Locked;
            CameraDisabled = false;
        }

        //Getting Mouse Movement
        if (!CameraDisabled)
        {
            //Rotation of the camera based on mouse movement
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamping the y rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < CameraMinHeight) {
                    _LocalRotation.y = CameraMinHeight;
                }
                else if (_LocalRotation.y > CameraMaxHeight)
                {
                    _LocalRotation.y = CameraMaxHeight;
                }
            }
        }

        //Actual Camera Transformations
        Quaternion TargetQ = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _ParentTransform.rotation = Quaternion.Lerp(_ParentTransform.rotation, TargetQ, Time.deltaTime * TurnDampening);
    }
}
