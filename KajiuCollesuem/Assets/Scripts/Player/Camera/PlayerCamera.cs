using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _CameraTansform;
    private Transform _ParentTransform;
    private Vector3 _LocalRotation;

    public Transform lockOnTarget;
    private PlayerLockOnScript lockOnScript;

    [SerializeField] private bool Inverted = false;
    
    [Space]
    [SerializeField] private float lockOnXOffset = 0;
    [SerializeField] private float lockOnInputInfluence = 0.2f;
    private float timeToChangeTarget = 0.0f;
    [SerializeField] private float lockOnChangeDelay = 1.0f;
    [SerializeField] private float lockOnChangeAmount_KB = 10.0f;
    [SerializeField] private float lockOnChangeAmount_GP = 1.5f;

    public float MouseSensitivity = 4f;
    public float TurnDampening = 10f;
    [SerializeField] private float OffSetLeft = 0f;
    [SerializeField] private float CameraDistance = 6f;
    [SerializeField] private float CameraMinHeight = -20f;
    [SerializeField] private float CameraMaxHeight = 90f;

    public bool CameraDisabled = false;
    
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

    public void GiveLockOnScript(PlayerLockOnScript pScript) => lockOnScript = pScript;

    void Update()
    {
        //Getting Mouse Movement
        if (!CameraDisabled)
        {
            if (lockOnTarget == null)
            {
                DefaultCameraMovement();
            }
            else
            {
                LockOnCameraMovement();
            }
        }

        //Actual Camera Transformations
        Quaternion TargetQ = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _ParentTransform.rotation = Quaternion.Lerp(_ParentTransform.rotation, TargetQ, Time.deltaTime * TurnDampening);
    }

    void DefaultCameraMovement(float pInputModifier = 1f)
    {
        //Rotation of the camera based on mouse movement
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity * pInputModifier;
            _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity * pInputModifier * (Inverted ? -1 : 1);

            //Clamping the y rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < CameraMinHeight)
            {
                _LocalRotation.y = CameraMinHeight;
            }
            else if (_LocalRotation.y > CameraMaxHeight)
            {
                _LocalRotation.y = CameraMaxHeight;
            }
        }
    }

    void LockOnCameraMovement()
    {
        //Locked onto Target
        Vector2 direction = new Vector2(lockOnTarget.position.z, lockOnTarget.position.x)  - new Vector2(_ParentTransform.position.z, _ParentTransform.position.x) ;
        _LocalRotation.x = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + (lockOnXOffset * OffSetLeft); // Add distance into this line potentially
        _LocalRotation.y = _ParentTransform.position.y - lockOnTarget.position.y;

        Vector3 _RotTarget = _LocalRotation;

        DefaultCameraMovement(lockOnInputInfluence);

        //Change Target
        float RequiredPushAmount = ((Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) ? lockOnChangeAmount_KB : lockOnChangeAmount_GP);
        if ((_LocalRotation - _RotTarget).magnitude >= RequiredPushAmount * MouseSensitivity && timeToChangeTarget <= Time.time)
        {
            timeToChangeTarget = Time.time + lockOnChangeDelay;
            Vector2 inputVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            lockOnTarget = lockOnScript.changeTarget(inputVector); //Vector tagent to camera forward but facing mouse input direction
        }
    }

    void IdleCameraMovement()
    {
        //Slowly Rotate
        _LocalRotation.x += idleSpinSpeed * Time.deltaTime;
        _LocalRotation.y = Mathf.Lerp(_LocalRotation.y, idleSpinY, Time.deltaTime);
    }





    // Camera Collision //////////////
    void CameraCollision()
    {
        RaycastHit hit;
        Physics.Raycast(_ParentTransform.position, (_CameraTansform.position - _ParentTransform.position).normalized, out hit, CameraDistance, cameraCollisionLayers);

        if (hit.point != Vector3.zero)
        {
            hit.point -= _ParentTransform.position;
            _CameraTansform.localPosition = Vector3.Lerp(_CameraTansform.localPosition, _TargetLocalPosition * Mathf.Clamp((hit.point.magnitude / _TargetLocalPosition.magnitude), cameraCollisionMinDisPercent, 0.5f), Time.deltaTime * cameraCollisionDampening);
            //Debug.Log(hit.point.magnitude / _TargetLocalPosition.magnitude * 2 * 100 + "%");
            //Debug.DrawLine(_ParentTransform.position, hit.point + _ParentTransform.position, Color.red, 0.1f);
        }
        else
        {
            _CameraTansform.localPosition = Vector3.Lerp(_CameraTansform.localPosition, _TargetLocalPosition * 0.5f, Time.deltaTime * cameraCollisionDampening);
        }
    }




    // Camera Transition ////////////// Lerp camera variables
    public void ChangePlayerCamera(float pOffSetLeft, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pTransitionSpeed)
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
            TurnDampening = Mathf.Lerp(TurnDampening, pTurnDampening, pTransitionSpeed * Time.deltaTime * 10);
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
