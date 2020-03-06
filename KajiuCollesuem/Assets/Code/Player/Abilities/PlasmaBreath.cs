using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Plasma Breath Ability for Lizzy Scale (Godzilla Character)*/ 

public class PlasmaBreath : MonoBehaviour, IAbility
{
    private PlayerStateController _stateController;
    private Transform _modelTransform;
    private float _nextSubStateTime = 0;
    private bool _charging = false;

    private GameObject _SpawnedLaser;

    // SET IN CODE VARS
    private float _RotateDampening = 3.0f;

    private AbilityState _MyState;

    public void Init(PlayerStateController pStateController, Transform pMuzzle, GameObject pPrefab)
    {
        _stateController = pStateController;

        // Get model
        _modelTransform = _stateController._modelController.transform;

        // Setup Hitbox / Visuals
        _SpawnedLaser = Instantiate(pPrefab, pMuzzle);
        _SpawnedLaser.transform.SetParent(pMuzzle);
        _SpawnedLaser.transform.localPosition = Vector3.zero;
        _SpawnedLaser.transform.localRotation = Quaternion.identity;
        _SpawnedLaser.SetActive(false);
    }

    public void Pressed(AbilityState pState)
    {
        _MyState = pState;

        _stateController._movementComponent.disableMovement = true;

        _charging = true;
        _nextSubStateTime = Time.time + 1;
        _stateController._lockOnComponent.ToggleScopedIn(0.05f);
        _stateController._modelController.TransitionToAbility(0);
        _stateController._modelController.PlayAbility(0);
    }

    public void Released()
    {
        Debug.Log("PlasmaBreath: Released");

        // If released while still charging, timer resets to zero.
        //if (_charging)
        //{
        //    _MyState.RequestExitState();
        //}
    }

    public void Exit()
    {
        Debug.Log("PlasmaBreath: Exit");
        _stateController._movementComponent.disableMovement = false;
        _stateController._lockOnComponent.ToggleScopedIn(1.0f);
        _stateController._modelController.DoneAbility(0);
        _SpawnedLaser.SetActive(false);
    }

    public void Tick()
    {
        _modelTransform.forward = Vector3.Slerp(Horizontalize(_modelTransform.forward), Horizontalize(_stateController._Camera.forward), Time.deltaTime * _RotateDampening);

        // Once timer reaches 2, attacki begins, enabling the beam particle with a hitbox.
        if (_nextSubStateTime <= Time.time)
        {
            ToggleBreath();
        }
    }

    //Sets laser prefab to being active.
    private void ToggleBreath()
    {
        _SpawnedLaser.SetActive(_charging);
        if (_charging == true)
            _nextSubStateTime = Time.time + _stateController._modelController.abilities[0].maxChargeTime;

        _charging = !_charging;
    }

    //Get a normalized horizontal Vector
    private Vector3 Horizontalize(Vector3 pVector)
    {
        pVector.y = 0;
        pVector.Normalize();
        return pVector;
    }
}
