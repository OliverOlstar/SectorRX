using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Plasma Breath Ability for Lizzy Scale (Godzilla Character)*/ 

public class PlasmaBreath : MonoBehaviour, IAbility
{
    private PlayerStateController _stateController;
    private float _nextSubStateTime = 0;
    private bool _charging = false;

    private Transform _SpawnedLaser;

    // SET IN CODE VARS
    private float _RotateDampening = 3.0f;

    public void Init(PlayerStateController pStateController, Transform pMuzzle, GameObject pPrefab)
    {
        _stateController = pStateController;

        // Setup Hitbox / Visuals
        _SpawnedLaser = Instantiate(pPrefab, pMuzzle).transform;
        _SpawnedLaser.SetParent(pMuzzle);
        _SpawnedLaser.localPosition = Vector3.zero;
        _SpawnedLaser.localRotation = Quaternion.identity;
        _SpawnedLaser.gameObject.SetActive(false);
    }

    public void Pressed()
    {
        this.enabled = true;

        _charging = true;
        _nextSubStateTime = Time.time + 5;
        _stateController._lockOnComponent.ToggleScopedIn(0.2f);
    }

    public void Released()
    {
        // If released while still charging, timer resets to zero.
        if (_charging)
        {
            _nextSubStateTime = 0;
        }
    }

    public void Exit()
    {
        Debug.Log("PlasmaBreath: Exit");
        _stateController._lockOnComponent.ToggleScopedIn(1.0f);
        this.enabled = false;
    }

    public void Upgrade()
    {

    }

    void Update()
    {
        transform.GetChild(1).GetChild(0).forward = Vector3.Slerp(Horizontalize(transform.GetChild(1).GetChild(0).forward), Horizontalize(_stateController._Camera.forward), Time.deltaTime * _RotateDampening);

        // Once timer reaches 2, attacki begins, enabling the beam particle with a hitbox.
        if (_nextSubStateTime <= Time.time)
        {
            ToggleBreath();
        }
    }

    //Sets laser prefab to being active.
    void ToggleBreath()
    {
        _SpawnedLaser.gameObject.SetActive(_charging);
        if (_charging == true)
            _nextSubStateTime = Time.time + 4;
        else
            this.enabled = false;

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
