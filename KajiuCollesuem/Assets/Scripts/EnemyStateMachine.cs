using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public Dictionary<Type, EnemyBaseState> _States;
    public EnemyBaseState CurrentState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = _States.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
        {
            CurrentState.Exit();
            SwitchToNextState(nextState);
            CurrentState.Enter();
        }
    }

    public void SetStates(Dictionary<Type, EnemyBaseState> states)
    {
        _States = states;
    }

    private void SwitchToNextState(Type _nextState)
    {
        CurrentState = _States[_nextState];
    }
}
