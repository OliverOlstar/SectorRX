using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManagement : MonoBehaviour
{
    private Decision _decision;
    [SerializeField] private float _fRadius;
    [SerializeField] private LayerMask _playerLayer;
    public float fScanVision = 30;

    // Start is called before the first frame update
    void Start()
    {
        _decision = GetComponent<Decision>();
    }

    // Update is called once per frame
    void Update()
    {
        bool retribution = GetComponent<AlwaysSeek>().retribution;

        if (_decision.target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _fRadius, _playerLayer);
            //float distance = Vector3.Distance(transform.position, _decision.target.position);

            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i].gameObject.tag.Equals("Player")
                        && colliders[i].gameObject.transform != _decision.target
                        && !retribution)
                    {
                        _decision.target = colliders[i].gameObject.transform;

                        if (!_IsPlayerInRange())
                            _decision.target = null;
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

                    if (_decision.target == null && dot > 0.9f && !retribution)
                    {
                        _decision.target = players[i].transform;
                        Debug.Log("Target in range");
                        break;
                    }
                }
            }
            //Debug.Log(GameObject.Find("TestPlayer") + " " + Vector3.Dot(transform.TransformDirection(Vector3.forward).normalized, (GameObject.Find("TestPlayer").transform.position - transform.position).normalized));

            /*if (_decision.target == null)
            {
                for (int i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i].gameObject.tag.Equals("Player"))
                    {
                        _decision.target = colliders[i].gameObject.transform;
                        break;
                    }
                }
            }*/

            /*else if (_decision.target == null && Physics.Raycast(transform.position, transform.forward, out hit, 16))
            {
                _decision.target = hit.collider.gameObject.tag.Equals("Player") ? hit.collider.gameObject.transform : null;
                _raycastHit = true;
            }*/

            //if (colliders.Length > 0 && !retribution)
            //{
                /*if (Vector3.Angle(transform.forward, _decision.target.position - transform.position) > fScanVision * 2)
                    rotSpeed = 5;
                else
                    rotSpeed = 3;*/
            //}
        }

        if (_decision.target != null && !retribution)
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(_decision.target.position - transform.position),
                Time.deltaTime * 5);
    }

    private bool _IsPlayerInRange()
    {
        if (Vector3.Angle(transform.forward, _decision.target.position - transform.position) < fScanVision)
            return true;

        return false;
    }
}
