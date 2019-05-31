using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _CameraTansform;
    private Transform _ParentTransform;
    private Vector3 _LocalRotation;

    public float MouseSensitivity = 4f;
    public float TurnDampening = 10f;
    [SerializeField] private float OffSetLeft = 0f;
    [SerializeField] private float CameraDistance = 6f;
    [SerializeField] private float CameraMinHeight = -20f;
    [SerializeField] private float CameraMaxHeight = 90f;

    [SerializeField] private bool CameraDisabled = false;
    
    void Start()
    {
        //Getting Transforms
        _CameraTansform = transform;
        _ParentTransform = transform.parent;

        //Maintaining Starting Rotation
        _LocalRotation.x = _ParentTransform.eulerAngles.y;
        _LocalRotation.y = _ParentTransform.eulerAngles.x;

        //Locking cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Setting camera distance
        _CameraTansform.localPosition = new Vector3(-OffSetLeft, 0f, CameraDistance * -1f);
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

    public void ChangePlayerCamera(float pOffSetLeft, float pMouseSensitivity, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pTransitionSpeed)
    {
        StopCoroutine("OtherCameraVarsTransition");
        StartCoroutine(OtherCameraVarsTransition(pOffSetLeft, pMouseSensitivity, pTurnDampening, pCameraDistance, pCameraMinHeight, pCameraMaxHeight, pTransitionSpeed));
    }

    public IEnumerator OtherCameraVarsTransition(float pOffSetLeft, float pMouseSensitivity, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pTransitionSpeed)
    {
        while (MouseSensitivity != pMouseSensitivity || TurnDampening != pTurnDampening || CameraDistance != pCameraDistance
                || CameraMinHeight != pCameraMinHeight || CameraMaxHeight != pCameraMaxHeight || OffSetLeft != pOffSetLeft)
        {
            //Lerping all of the values
            MouseSensitivity = Mathf.Lerp(MouseSensitivity, pMouseSensitivity, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(MouseSensitivity - pMouseSensitivity) <= 0.01f)
                MouseSensitivity = pMouseSensitivity;

            TurnDampening = Mathf.Lerp(TurnDampening, pTurnDampening, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(TurnDampening - pTurnDampening) <= 0.01f)
                TurnDampening = pTurnDampening;

            CameraDistance = Mathf.Lerp(CameraDistance, pCameraDistance, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(CameraDistance - pCameraDistance) <= 0.01f)
                CameraDistance = pCameraDistance;

            CameraMinHeight = Mathf.Lerp(CameraMinHeight, pCameraMinHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(CameraMinHeight - pCameraMinHeight) <= 0.01f)
                CameraMinHeight = pCameraMinHeight;

            CameraMaxHeight = Mathf.Lerp(CameraMaxHeight, pCameraMaxHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(CameraMaxHeight - pCameraMaxHeight) <= 0.01f)
                CameraMaxHeight = pCameraMaxHeight;

            OffSetLeft = Mathf.Lerp(OffSetLeft, pOffSetLeft, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(OffSetLeft - pOffSetLeft) <= 0.01f)
                OffSetLeft = pOffSetLeft;

            //Setting camera distance
            _CameraTansform.localPosition = new Vector3(-OffSetLeft, 0f, CameraDistance * -1f);

            yield return null;
        }
    }
}
