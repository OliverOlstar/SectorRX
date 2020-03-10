using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Plasma Breath Ability for Lizzy Scale (Godzilla Character)*/ 

public class PlasmaBall : MonoBehaviour, IAbility
{
    private PlayerStateController _stateController;
    private GameObject _ballPrefab;
    private Transform _muzzle;
    private SOAbilities _AbilitySO;
    private Transform _modelTransform;

    private float _rotTime = 0;
    private float _spawnBallTime = 0;

    private bool _ballSpawned = false;
    private float _attackMult = 1.0f;

    public void Init(PlayerStateController pStateController, Transform pMuzzle, GameObject pPrefab)
    {
        _stateController = pStateController;
        _muzzle = pMuzzle;
        _ballPrefab = pPrefab;

        _modelTransform = _stateController._modelController.transform;
        _AbilitySO = _stateController._modelController.abilitySO;
    }

    public void Upgrade(float pValue)
    {
        _attackMult = pValue;
    }

    public void Pressed()
    {
        _stateController._movementComponent.disableMovement = true;

        _stateController._lockOnComponent.ToggleScopedIn(0.05f);
        _stateController._modelController.TransitionToAbility();
        _stateController._modelController.PlayAbility();

        _rotTime = Time.time + _AbilitySO.holdStartPosTime;
        _spawnBallTime = Time.time + _AbilitySO.hitBoxAppearTime;
        _ballSpawned = false;

        _stateController._playerAttributes.modifyAbility(-_stateController._modelController.abilitySO.powerRequired);
    }

    public void Exit()
    {
        _stateController._movementComponent.disableMovement = false;
        _stateController._lockOnComponent.ToggleScopedIn(1.0f);
        _stateController._modelController.DoneAbility();
    }

    public void Tick()
    {
        if (Time.time >= _rotTime)
            _modelTransform.forward = Vector3.Slerp(Horizontalize(_modelTransform.forward), Horizontalize(_stateController._Camera.forward), Time.deltaTime * _AbilitySO.rotationDampening);

        if (_ballSpawned == false && _spawnBallTime <= Time.time)
        {
            SpawnBall();
            _stateController._Sound.PlasmaBallSound();
            _stateController._CameraShake.PlayShake(6.0f, 6.0f, 0.1f, 0.2f);
            _ballSpawned = true;
        }
    }

    private void SpawnBall()
    {
        // Setup Hitbox / Visuals
        GameObject ball = Instantiate(_ballPrefab);
        ball.transform.position = _muzzle.position;
        ball.transform.rotation = _muzzle.rotation;

        ball.GetComponentInChildren<PlayerBallHitbox>().Init(GetComponent<PlayerAttributes>(), gameObject, _attackMult, _AbilitySO.hitBoxStayTime);
        ball.GetComponent<Rigidbody>().AddForce(_muzzle.forward * _AbilitySO.projectileSpeed, ForceMode.Impulse);
    }

    //Get a normalized horizontal Vector
    private Vector3 Horizontalize(Vector3 pVector)
    {
        pVector.y = 0;
        pVector.Normalize();
        return pVector;
    }
}
