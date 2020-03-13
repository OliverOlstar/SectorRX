using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Oliver
3rd Person Camera
*/

public class PlayerCamera : MonoBehaviour
{
    public Transform targetPlayer;
    private PlayerStateController _StateController;
    private Camera _MyCamera;

    private Transform _ParentTransform;
    private Vector3 _LocalRotation;
    private Vector3 _TargetLocalPosition;

    public bool inverted = false;

    [Header("Idle")]
    [SerializeField] private float idleSpinSpeed = 1;

    [Header("Camera Collision")]
    [SerializeField] private LayerMask _cameraCollisionLayers = new LayerMask();
    [SerializeField] private float _cameraCollisionDampening = 20;
    [SerializeField] [Range(0, 1)] private float _cameraCollisionMinDisPercent = 0.1f;
    [SerializeField] private float _cameraCollisionOffset = 0.1f;

    [Space]
    [SerializeField] private float _mouseSensitivity = 4f;
    [SerializeField] private SOCamera _startingPreset;
    [SerializeField] private SOCamera _defaultPreset;
    [SerializeField] private SOCamera _idlePreset;
    [SerializeField] private SOCamera _aimingPreset;

    private float _mouseSensitivityMult = 1f;
    private float _turnDampening = 10f;
    private float _offSetLeft = 0f;
    private float _offSetUp = 0.6f;
    private float _cameraDistance = 6f;
    private float _cameraMinHeight = -20f;
    private float _cameraMaxHeight = 90f;

    [Space]
    public bool CameraDisabled = false;
    public bool targetDead = false;
    public bool targetIdle = false;
    
    private Coroutine _transRoutine;

    void Start()
    {
        _MyCamera = GetComponentInChildren<Camera>();

        // Get Input
        if (targetPlayer != null)
            _StateController = targetPlayer.GetComponent<PlayerStateController>();

        // Set Camera to default values
        ResetCameraVars(_startingPreset);
        ReturnToDefaultPlayerCamera(1.75f);

        // Getting Transforms
        _ParentTransform = transform.parent;

        // Maintaining Starting Rotation
        _LocalRotation.x = _ParentTransform.eulerAngles.y;
        _LocalRotation.y = _ParentTransform.eulerAngles.x;

        // Setting camera distance
        _TargetLocalPosition = new Vector3(-_offSetLeft, 0f, _cameraDistance * -1f);
        transform.localPosition = _TargetLocalPosition;
    }

    public void ResetCameraVars(SOCamera pPreset)
    {
        _mouseSensitivityMult = pPreset.SensitivityMult;
        _offSetUp = pPreset.UpOffset;
        _offSetLeft = pPreset.LeftOffset;
        _cameraDistance = pPreset.Distance;
        _cameraMinHeight = pPreset.MinY;
        _cameraMaxHeight = pPreset.MaxY;
    }

    public void SetPlayerCameraPresets(SOCamera pDefault, SOCamera pIdle, SOCamera pAiming)
    {
        _defaultPreset = pDefault;
        _idlePreset = pIdle;
        _aimingPreset = pAiming;
        ResetCameraVars(_defaultPreset);
    }

    void Update()
    {
        if (targetPlayer == null)
            return;

        //Getting Mouse Movement
        if (!CameraDisabled)
        {
            if (targetDead == true)
            {
                DeadCameraMovement();
                return;
            }
            else if (targetIdle == true)
            {
                IdleCameraMovement();
            }
            else
            {
                DefaultCameraMovement();
            }
        }

        //Actual Camera Transformations
        Quaternion TargetQ = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _ParentTransform.rotation = Quaternion.Lerp(_ParentTransform.rotation, TargetQ, Time.deltaTime * _turnDampening);

        //Position the camera pivot on the player
        _ParentTransform.position = targetPlayer.position + (Vector3.up * _offSetUp);

        //Camera Collision
        CameraCollision();
    }

    void DefaultCameraMovement(float pInputModifier = 1f)
    {
        //Rotation of the camera based on mouse movement
        if (_StateController.mouseInput.magnitude != 0)
        {
            _LocalRotation.x += _StateController.mouseInput.x * _mouseSensitivity * _mouseSensitivityMult * pInputModifier;
            _LocalRotation.y -= _StateController.mouseInput.y * _mouseSensitivity * _mouseSensitivityMult * pInputModifier * (inverted ? -1 : 1);

            //Clamping the y rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < _cameraMinHeight)
            {
                _LocalRotation.y = _cameraMinHeight;
            }
            else if (_LocalRotation.y > _cameraMaxHeight)
            {
                _LocalRotation.y = _cameraMaxHeight;
            }
        }
    }

    void IdleCameraMovement()
    {
        //Slowly Rotate
        _LocalRotation.x += idleSpinSpeed * Time.deltaTime;
        _LocalRotation.y = Mathf.Lerp(_LocalRotation.y, (_cameraMaxHeight + _cameraMinHeight) / 2, Time.deltaTime);
    }

    void DeadCameraMovement()
    {
        //Slowly Rotate
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        if (direction.x == 0 && direction.z == 0) return;

        Quaternion TargetQ = Quaternion.LookRotation(direction, Vector3.up);
        _ParentTransform.rotation = Quaternion.Lerp(_ParentTransform.rotation, TargetQ, Time.deltaTime * _turnDampening);
    }

    #region Collision
    // Camera Collision //////////////
    void CameraCollision()
    {
        RaycastHit hit;
        Vector3 rayDirection = (transform.position - _ParentTransform.position).normalized;
        Physics.Raycast(_ParentTransform.position, rayDirection, out hit, _cameraDistance + _cameraCollisionOffset, _cameraCollisionLayers);

        if (hit.point != Vector3.zero)
        {
            hit.point -= _ParentTransform.position + rayDirection * _cameraCollisionOffset;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _TargetLocalPosition * Mathf.Clamp((hit.point.magnitude / _TargetLocalPosition.magnitude), _cameraCollisionMinDisPercent, 0.5f), Time.deltaTime * _cameraCollisionDampening);
            //Debug.Log(hit.point.magnitude / _TargetLocalPosition.magnitude * 2 * 100 + "%");
            //Debug.DrawLine(_ParentTransform.position, hit.point + _ParentTransform.position, Color.red, 0.1f);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _TargetLocalPosition * 0.5f, Time.deltaTime * _cameraCollisionDampening);
        }
    }
    #endregion

    #region Transitions
    // Camera Transition ////////////// Lerp camera variables
    public void ChangePlayerCamera(SOCamera pPreset, float pTransitionSpeed)
    {
        if (_transRoutine != null)
            StopCoroutine(_transRoutine);
        _transRoutine = StartCoroutine(CameraVarsTransition(pPreset.UpOffset, pPreset.LeftOffset, pPreset.TurnDampening, pPreset.Distance, pPreset.MinY, pPreset.MaxY, pPreset.SensitivityMult, pPreset.FOV, pTransitionSpeed));
    }

    public void ReturnToDefaultPlayerCamera(float pTransitionSpeed) => ChangePlayerCamera(_defaultPreset, pTransitionSpeed);
    public void SwitchToIdleCamera(float pTransitionSpeed) => ChangePlayerCamera(_idlePreset, pTransitionSpeed);
    public void SwitchToAimingCamera(float pTransitionSpeed) => ChangePlayerCamera(_aimingPreset, pTransitionSpeed);

    public IEnumerator CameraVarsTransition(float pOffSetUp, float pOffSetLeft, float pTurnDampening, float pCameraDistance, float pCameraMinHeight, float pCameraMaxHeight, float pSensitivityMult, float pFOV, float pTransitionSpeed)
    {
        short done;

        do
        {
            done = 0;

            //Lerping all of the values
            _mouseSensitivityMult = Mathf.Lerp(_mouseSensitivityMult, pSensitivityMult, pTransitionSpeed * Time.deltaTime * 4);
            if (Mathf.Abs(_mouseSensitivityMult - pSensitivityMult) <= 0.01f)
            {
                _mouseSensitivityMult = pSensitivityMult;
                done++;
            }

            _turnDampening = Mathf.Lerp(_turnDampening, pTurnDampening, pTransitionSpeed * Time.deltaTime * 10);
            if (Mathf.Abs(_turnDampening - pTurnDampening) <= 0.01f)
            {
                _turnDampening = pTurnDampening;
                done++;
            }

            _cameraDistance = Mathf.Lerp(_cameraDistance, pCameraDistance, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_cameraDistance - pCameraDistance) <= 0.01f)
            {
                _cameraDistance = pCameraDistance;
                done++;
            }

            _cameraMinHeight = Mathf.Lerp(_cameraMinHeight, pCameraMinHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_cameraMinHeight - pCameraMinHeight) <= 0.01f)
            {
                _cameraMinHeight = pCameraMinHeight;
                done++;
            }

            _cameraMaxHeight = Mathf.Lerp(_cameraMaxHeight, pCameraMaxHeight, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_cameraMaxHeight - pCameraMaxHeight) <= 0.01f)
            {
                _cameraMaxHeight = pCameraMaxHeight;
                done++;
            }

            _offSetLeft = Mathf.Lerp(_offSetLeft, pOffSetLeft, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_offSetLeft - pOffSetLeft) <= 0.01f)
            {
                _offSetLeft = pOffSetLeft;
                done++;
            }

            _offSetUp = Mathf.Lerp(_offSetUp, pOffSetUp, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_offSetUp - pOffSetUp) <= 0.01f)
            {
                _offSetUp = pOffSetUp;
                done++;
            }

            _MyCamera.fieldOfView = Mathf.Lerp(_MyCamera.fieldOfView, pFOV, pTransitionSpeed * Time.deltaTime);
            if (Mathf.Abs(_MyCamera.fieldOfView - pOffSetUp) <= 0.01f)
            {
                _MyCamera.fieldOfView = pFOV;
                done++;
            }

            //Setting camera distance
            _TargetLocalPosition = new Vector3(-_offSetLeft, 0f, _cameraDistance * -1f);

            yield return null;
        }
        while (done != 8);
    }
    #endregion
}
