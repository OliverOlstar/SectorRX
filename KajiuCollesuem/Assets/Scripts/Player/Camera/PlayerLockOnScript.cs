using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOnScript : MonoBehaviour
{
    // OLIVER - This script tells the PlayerCamera Script weather to be locked on or not and who to lock on too.

    private PlayerStateController _stateController;
    private PlayerCamera _cameraScript;
    private CameraPivot _cameraPivotScript;

    [SerializeField] private SOCamera defaultPreset;
    [SerializeField] private float cameraTransSpeed = 1;

    [Header("LockOn")]
    [SerializeField] private SOCamera lockOnPreset;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float lockOnRange = 10.0f;
    
    [HideInInspector] public bool focusedOnScreen = false;
    [HideInInspector] public bool unfocusedOnScreen = false;

    [Header("Idle")]
    [SerializeField] private SOCamera idlePreset;
    [SerializeField] private float TimeUntilIdle = 20f;
    
    void Start()
    {
        _cameraScript = Camera.main.GetComponent<PlayerCamera>();
        _cameraPivotScript = _cameraScript.transform.parent.GetComponent<CameraPivot>();
        _stateController = GetComponent<PlayerStateController>();
    }
    
    void Update()
    {
        // TODO make work with pause screen
        //Toggle Idle Camera
        if (Time.time - _stateController.LastInputTime > TimeUntilIdle)
        {
            if (_cameraScript.Idle == false && _cameraScript.lockOnTarget == null)
            {
                _cameraScript.Idle = true;
                _cameraPivotScript.ChangePlayerCamera(idlePreset, cameraTransSpeed);
            }
        }
        else if (_cameraScript.Idle == true && _cameraScript.lockOnTarget == null)
        {
            _cameraScript.Idle = false;
            _cameraPivotScript.ChangePlayerCamera(defaultPreset, cameraTransSpeed);
        }

        //Toggle LockOn Camera
        if (_stateController.lockOnInput == true)
        {
            _stateController.lockOnInput = false;

            if (_cameraScript.lockOnTarget == null)
            {
                Transform target = pickNewTarget();

                if (target != null)
                {
                    _cameraScript.lockOnTarget = target;
                    _cameraPivotScript.ChangePlayerCamera(lockOnPreset, cameraTransSpeed);
                }
            }
            else
            {
                _cameraScript.lockOnTarget = null;
                _cameraPivotScript.ChangePlayerCamera(defaultPreset, cameraTransSpeed);
            }
            //else if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.05f && cameraScript.lockOnTarget != null)
            //{
            //cameraScript.lockOnTarget = pickNewTarget();
            //}
        }
    }

    Transform pickNewTarget()
    {
        Collider[] possibleTargets = Physics.OverlapSphere(transform.position, lockOnRange, enemiesLayer);

        if (possibleTargets.Length == 0)
            return null;

        int currentClosest = 0;
        int secondClosest = 0;

        //Finds the two closest options
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            float view = Camera.main.WorldToViewportPoint(possibleTargets[i].transform.position).x;
            float bestView = Camera.main.WorldToViewportPoint(possibleTargets[currentClosest].transform.position).x;

            if (Mathf.Abs(view - 0.5f) < Mathf.Abs(bestView - 0.5f))
            {
                //if (Vector3.Distance(transform.forward * 5, possibleTargets[i].transform.position) < Vector3.Distance(transform.forward * 5, possibleTargets[currentClosest].transform.position))
                secondClosest = currentClosest;
                currentClosest = i;
            }
        }
        
        if (_cameraScript.lockOnTarget == possibleTargets[currentClosest].gameObject)
        {
            return possibleTargets[secondClosest].gameObject.transform;
        }

        return possibleTargets[currentClosest].gameObject.transform;
    }
}
