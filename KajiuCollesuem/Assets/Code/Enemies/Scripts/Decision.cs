using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Programmer: Mugiesshan Anandarajah
 * Description: Edited to complete the following tasks:
 *      Task 1: Grunts targeting is updated to allow for switching between targets
 *      Task 2: Grunts have a harder time detecting a player
 *      Task 3: Grunt rotation is smooth
 * */
public class Decision : MonoBehaviour
{
    private IState[] _states;
    private IState _currentState;

    public Transform target;

    private float rotSpeed;

    private bool _targetSwitch = false, _raycastHit = false;

    void Start()
    {
        //Get States
        _states = GetComponents<IState>();

        //Setup States
        SetupStates();

        //Start on least priority State that can be entered
        StartLastState();
    }

    private void StartLastState()
    {
        //Enter least priority State that can be entered
        for (int i = _states.Length - 1; i >= 0; i--)
        {
            //Get distance to target
            float distance = (target == null ? 999999 : Vector3.Distance(transform.position, target.position));

            //Check if can Enter
            if (_states[i].CanEnter(distance))
            {
                _currentState = _states[i];
                _currentState.Enter();
                target = null;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        CheckStates();
        _currentState.UpdateTarget(target);
    }

    private void Update()
    {
        //if (target != null)
            _currentState.Tick();
    }

    public void SetupStates()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        Animator anim = GetComponent<Animator>();

        foreach (IState state in _states)
        {
            state.Setup(target, anim, agent);
        }
    }

    private void CheckStates()
    {
        if (target != null)
        {
            //Get distance to target
            float distance = Vector3.Distance(transform.position, target.position);
            //bool retribution = GetComponent<AlwaysSeek>().retribution;

            //Return if you can't Exit current state
            if (_currentState.CanExit(distance) == false /*&& !retribution*/) return;

            foreach (IState state in _states)
            {
                //Check if can stay in same state
                if (_currentState == state)
                {
                    if (state.CanEnter(distance))
                        break;
                    else
                        continue;
                }

                //Check if state can be entered. Task 2: Grunts have a harder time detecting a player
                if (state.CanEnter(distance) || retribution)
                {
                    SwitchState(state);
                    break;
                }
            }
            //Debug.Log(distance);
        }
    }

    /*Calculate the distance between itself and the player, and updates its target to the nearest player,
    and update the target setup
    Task 1: Grunts targeting is updated to allow for switching of targets*/
    /*private void CheckAndUpdateTarget()
    {
        /*if (_currentState.CanEnter(smallest_distance))
        {
            if (target != _players[index].transform)
            {
                GetComponent<Strafe>().Pause();
                _targetSwitch = true;
            }
            target = _players[index].transform;
            SetupStates();
        }
        
        if (Vector3.Angle(transform.forward, target.position - transform.position) < 1
            && _targetSwitch)
        {
            GetComponent<Strafe>().Resume();
            _targetSwitch = false;
        }
        else if (_targetSwitch)
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(target.position - transform.position),
                Time.deltaTime * 5);
    }*/

    //Exit old state and Enter new state
    private void SwitchState(IState pState)
    {
        _currentState.Exit();
        pState.Enter();
        _currentState = pState;
    }

    // Public Functions //////////////////////////////
    //Called when needing to switch to a state that is not a state the AI wants to be in (stunned, hurt, etc.)
    public void ForceStateSwitch(IState pState)
    {
        SwitchState(pState);
    }

    //Resets the AI for respawning
    public void Respawn()
    {
        _currentState.Exit();
        StartLastState();
    }
}
