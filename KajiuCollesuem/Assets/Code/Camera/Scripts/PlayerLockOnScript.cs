 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RootMotion.FinalIK;

public class PlayerLockOnScript : MonoBehaviour
{
    // OLIVER - This script tells the PlayerCamera Script wether to be locked on or not and who to lock on too.

    private PlayerStateController _stateController;
    private PlayerCamera _playerCamera;

    [SerializeField] private float cameraTransSpeed = 1;

    [Header("Aiming")]
    private bool _aiming = false;

    [Header("Idle")]
    [SerializeField] private float TimeUntilIdle = 20f;
    
    [Header("LookAt")]
    [SerializeField] private LookAtIK _lookAt;

    public void Start()
    {
        _stateController = GetComponent<PlayerStateController>();
        _playerCamera = _stateController._playerCamera;
    }

    void Update()
    {
        if (_stateController._playerCamera == null) return;

        // Toggle Idle Camera
        if (Time.time - _stateController.LastInputTime > TimeUntilIdle)
        {
            if (_playerCamera.targetIdle == false)
            {
                _playerCamera.targetIdle = true;
                _playerCamera.SwitchToIdleCamera(cameraTransSpeed);
            }
        }
        else if (_playerCamera.targetIdle == true)
        {
            // Leave Idle Camera
            _playerCamera.targetIdle = false;
            if (_aiming)
                _playerCamera.SwitchToAimingCamera(cameraTransSpeed);
            else
                _playerCamera.ReturnToDefaultPlayerCamera(cameraTransSpeed);
        }
    }

    public void ToggleScopedIn(float pTransSpeed)
    {
        _aiming = !_aiming;

        if (_aiming)
        {
            _playerCamera.SwitchToAimingCamera(cameraTransSpeed);
        }
        else
        {
            _playerCamera.ReturnToDefaultPlayerCamera(pTransSpeed);
        }

    }

    public void SwitchToDeadCamera()
    {
        _lookAt.solver.target = null;
        _playerCamera.targetDead = true;
    }
}
