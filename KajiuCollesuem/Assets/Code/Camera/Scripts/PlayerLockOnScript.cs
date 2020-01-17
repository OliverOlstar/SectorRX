﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLockOnScript : MonoBehaviour
{
    // OLIVER - This script tells the PlayerCamera Script wether to be locked on or not and who to lock on too.

    private PlayerStateController _stateController;

    [SerializeField] private float cameraTransSpeed = 1;

    [Header("LockOn")]
    [SerializeField] private SOCamera lockOnPreset;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float lockOnRange = 10.0f;
    [SerializeField] [Range(0, 359.9999f)] private float lockOnAngle = 45;
    
    [HideInInspector] public bool focusedOnScreen = false;
    [HideInInspector] public bool unfocusedOnScreen = false;

    [Header("Idle")]
    [SerializeField] private SOCamera idlePreset;
    [SerializeField] private float TimeUntilIdle = 20f;
    
    void Start()
    {
        _stateController = GetComponent<PlayerStateController>();
    }
    
    void Update()
    {
        if (_stateController._playerCamera == null) return;

        // TODO make work with pause screen
        //Toggle Idle Camera
        if (Time.time - _stateController.LastInputTime > TimeUntilIdle)
        {
            if (_stateController._playerCamera.targetIdle == false && _stateController._playerCamera.lockOnTarget == null)
            {
                _stateController._playerCamera.targetIdle = true;
                _stateController._playerCamera.ChangePlayerCamera(idlePreset, cameraTransSpeed);
            }
        }
        else if (_stateController._playerCamera.targetIdle == true && _stateController._playerCamera.lockOnTarget == null)
        {
            //Leave Idle Camera
            _stateController._playerCamera.targetIdle = false;
            _stateController._playerCamera.ReturnToDefaultPlayerCamera(cameraTransSpeed);
        }

        //Toggle LockOn Camera
        //if (_stateController.lockOnInput == true)
        //{
        //    _stateController.lockOnInput = false;
        //    //Start LockOn
        //    if (_cameraScript.lockOnTarget == null)
        //    {
        //        Transform target = pickTarget();

        //        //If Target Found
        //        if (target != null)
        //        {
        //            _cameraScript.lockOnTarget = target;
        //            _cameraScript.ChangePlayerCamera(lockOnPreset, cameraTransSpeed);
        //        }
        //    }
        //    else
        //    {
        //        //Leave LockOn
        //        _cameraScript.lockOnTarget = null;
        //        _cameraScript.ReturnToDefaultPlayerCamera(cameraTransSpeed);
        //    }
        //}
    }

    //Function To Find Intial Target
    public Transform pickTarget()
    {
        //Get Possible Targets
        List<Collider> possibleTargets = Physics.OverlapSphere(_stateController._playerCamera.transform.position + _stateController._playerCamera.transform.forward * lockOnRange / 2, lockOnRange, enemiesLayer).ToList();

        //Return NULL if no targets
        if (possibleTargets.Count == 0)
            return null;

        Vector3 forwardVector = _stateController._playerCamera.transform.forward;

        //Sort Targets
        possibleTargets.Sort((col1, col2) => (Vector3.Dot(col2.transform.position - _stateController._playerCamera.transform.position, forwardVector)).CompareTo(Vector3.Dot(col1.transform.position - _stateController._playerCamera.transform.position, forwardVector)));

        //for (int i = 0; i < possibleTargets.Count; i++)
        //{
        //    Debug.DrawLine(possibleTargets[i].transform.position, _cameraScript.transform.position, new Color(1 - 0.45f * i, 0, 0), 20);
        //}

        //Return Best Target
        return possibleTargets[0].transform;
    }

    //Function To Find Target to change too
    public Transform changeTarget(Vector2 pInput)
    {
        //Get Possible Targets
        List<Collider> possibleTargets = (Physics.OverlapSphere(_stateController._playerCamera.transform.position, lockOnRange * 2, enemiesLayer)).ToList();

        //No other targets than the one already locked onto
        if (possibleTargets.Count == 1)
            return possibleTargets[0].transform;

        Vector3 forwardVector = _stateController._playerCamera.transform.forward;
        Vector3 InputVector = _stateController._playerCamera.transform.right * pInput.x + _stateController._playerCamera.transform.up * pInput.y;
        InputVector.Normalize();
        //Debug.DrawLine(_cameraScript.transform.position, _cameraScript.transform.forward + _cameraScript.transform.position, Color.blue, 20);
        //Debug.DrawLine(_cameraScript.transform.position, _cameraScript.transform.position + InputVector, Color.green, 20);

        float lockOnRad = lockOnAngle * Mathf.Deg2Rad;
        Vector3 ConeVector = Mathf.Cos(lockOnRad) * _stateController._playerCamera.transform.forward + Mathf.Sin(lockOnRad) * InputVector;
        //Debug.DrawLine(_cameraScript.transform.position, _cameraScript.transform.position + ConeVector, Color.red, 20);

        Vector3 halfwayVector = (ConeVector + _stateController._playerCamera.transform.forward).normalized;
        //Debug.DrawLine(_cameraScript.transform.position, _cameraScript.transform.position + halfwayVector, Color.yellow, 20);

        //Remove Non Options
        for (int i = possibleTargets.Count - 1; i >= 0; i--)
        {
            if (Vector3.Dot((possibleTargets[i].transform.position - _stateController._playerCamera.transform.position).normalized, halfwayVector) < Mathf.Cos(lockOnRad / 2))
                possibleTargets.Remove(possibleTargets[i]);
        }

        //Sort Targets
        possibleTargets.Sort((col1, col2) => (Vector3.Dot(col2.transform.position - _stateController._playerCamera.transform.position, forwardVector)).CompareTo(Vector3.Dot(col1.transform.position - _stateController._playerCamera.transform.position, forwardVector)));

        //Dont Return Same Target
        if (_stateController._playerCamera.lockOnTarget == possibleTargets[0].transform)
            return possibleTargets[1].transform;

        return possibleTargets[0].transform;
    }

    public void TargetDead(Transform pTarget)
    {
        //If LockOn Target dies return to default camera
        if (pTarget == _stateController._playerCamera.lockOnTarget)
        {
            _stateController._playerCamera.lockOnTarget = null;
            _stateController._playerCamera.ReturnToDefaultPlayerCamera(cameraTransSpeed);
        }
    }
}
