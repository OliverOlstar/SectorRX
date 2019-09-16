using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
    [HideInInspector] public bool movement, jump, quickAttack, heavyAttack, lockOn, onGround;

    private PlayerStateMachine _stateMachine;

    void Start()
    {
        _stateMachine = GetComponent<PlayerStateMachine>();
        InitializeStateMachine();
    }

    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(EnemyAttack), new EnemyAttack(controller:this) },
            {typeof(EnemyDeath), new EnemyDeath(controller:this) },
            {typeof(EnemyMovement), new EnemyMovement(controller:this) }
        };

        _stateMachine.SetStates(states);
    }
}
