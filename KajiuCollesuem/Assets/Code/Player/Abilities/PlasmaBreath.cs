using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Plasma Breath Ability for Lizzy Scale (Godzilla Character)*/ 

public class PlasmaBreath : MonoBehaviour, IAbility
{
    private PlayerStateController _stateController;
    private SOAbilities _AbilitySO;
    private Transform _modelTransform;
    private float _nextSubStateTime = 0;
    private float _rotTime = 0;
    private bool _charging = false;

    private GameObject _SpawnedLaser;
    private PlayerMultHitbox _SpawnedLaserHitbox;

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

        _SpawnedLaserHitbox = _SpawnedLaser.GetComponentInChildren<PlayerMultHitbox>();
        _SpawnedLaserHitbox.attacker = gameObject;

        _SpawnedLaser.SetActive(false);

        _AbilitySO = _stateController._modelController.abilitySO;
    }

    public void Upgrade(float pValue)
    {
        _SpawnedLaserHitbox.attackMult = pValue;
    }

    public void Pressed()
    {
        _stateController._movementComponent.disableMovement = true;

        _charging = true;
        _nextSubStateTime = Time.time + _AbilitySO.hitBoxAppearTime;
        _stateController._lockOnComponent.ToggleScopedIn(0.05f);
        _stateController._modelController.TransitionToAbility();
        _stateController._modelController.PlayAbility();

        _stateController._Sound.PlasmaBreathSound();

        _rotTime = Time.time + _AbilitySO.holdStartPosTime;
    }

    public void Exit()
    {
        _stateController._movementComponent.disableMovement = false;
        _stateController._lockOnComponent.ToggleScopedIn(1.0f);
        _stateController._modelController.DoneAbility();
        _SpawnedLaser.SetActive(false);
    }

    public void Tick()
    {
        if (Time.time >= _rotTime)
            _modelTransform.forward = Vector3.Slerp(Horizontalize(_modelTransform.forward), Horizontalize(_stateController._Camera.forward), Time.deltaTime * _AbilitySO.rotationDampening);
           
        // Once timer reaches 2, attacki begins, enabling the beam particle with a hitbox.
        if (_nextSubStateTime <= Time.time || (_stateController._playerAttributes.getAbility() < 1 && _SpawnedLaser.activeSelf == true))
        {
            ToggleBreath();
        }
    }

    //Sets laser prefab to being active.
    private void ToggleBreath()
    {
        _SpawnedLaser.SetActive(_charging);
        if (_charging == true)
        {
            StartCoroutine("abilityLossRoutine");
            _nextSubStateTime = Time.time + _AbilitySO.hitBoxStayTime;
        }
        else
        {
            StopCoroutine("abilityLossRoutine");
        }

        _charging = !_charging;
    }

    IEnumerator abilityLossRoutine()
    {
        while (_stateController._playerAttributes.getAbility() < 1)
        {
            _stateController._playerAttributes.modifyAbility(-1);
            yield return new WaitForSeconds(0.25f);
        }
    }

    //Get a normalized horizontal Vector
    private Vector3 Horizontalize(Vector3 pVector)
    {
        pVector.y = 0;
        pVector.Normalize();
        return pVector;
    }
}
