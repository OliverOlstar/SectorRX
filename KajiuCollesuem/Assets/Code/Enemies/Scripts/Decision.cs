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

    [HideInInspector] public Transform target;
    [SerializeField] private LayerMask _playerLayer;

    public float fScanVision = 30;
    [SerializeField] private float _fRadius;
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
        CheckAndUpdateTarget();
    }

    private void Update()
    {
        if (target != null)
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
                if ((state.CanEnter(distance) && _IsPlayerInRange()) /*|| retribution*/)
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
    private void CheckAndUpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _fRadius, _playerLayer);
        RaycastHit hit;
        //bool retribution = GetComponent<AlwaysSeek>().retribution;

        if (colliders.Length == 2)
            target = null;
        if (target != null 
            && Vector3.Dot(transform.forward.normalized, (target.position - transform.position).normalized) < 0.9f
            && !_currentState.Equals(GetComponent<Stunned>())
            )//&& !retribution)
            target = null;

        if (colliders.Length == 3)
        {
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (colliders[i].gameObject.tag.Equals("Player") 
                    && colliders[i].gameObject.transform != target
                    )//&& !retribution)
                {
                    target = colliders[i].gameObject.transform;

                    if (!_IsPlayerInRange())
                        target = null;
                    else
                        break;
                }
            }
        }

        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < players.Length; ++i)
            {
                float dot = Vector3.Dot(transform.forward.normalized, 
                    (players[i].transform.position - transform.position).normalized);

                if (target == null && dot > 0.9f /*&& !retribution*/)
                {
                    target = players[i].transform;
                    Debug.Log("Target in range");
                    break;
                }
            }
        }
        //Debug.Log(GameObject.Find("TestPlayer") + " " + Vector3.Dot(transform.TransformDirection(Vector3.forward).normalized, (GameObject.Find("TestPlayer").transform.position - transform.position).normalized));

        /*if (target == null)
        {
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (colliders[i].gameObject.tag.Equals("Player"))
                {
                    target = colliders[i].gameObject.transform;
                    break;
                }
            }
        }*/

        /*else if (target == null && Physics.Raycast(transform.position, transform.forward, out hit, 16))
        {
            target = hit.collider.gameObject.tag.Equals("Player") ? hit.collider.gameObject.transform : null;
            _raycastHit = true;
        }*/

        if (target != null && colliders.Length > 2 /* && !retribution */)
        {
            /*if (Vector3.Angle(transform.forward, target.position - transform.position) > fScanVision * 2)
                rotSpeed = 5;
            else
                rotSpeed = 3;*/

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(target.position - transform.position),
                Time.deltaTime * 5);
        }
        SetupStates();

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
                Time.deltaTime * 5);*/
    }

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

    private bool _IsPlayerInRange()
    {
        if (target != null && Vector3.Angle(transform.forward, target.position - transform.position) < fScanVision)
            return true;

        return false;
    }
}
