using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManagement : MonoBehaviour
{
    private Decision _decision;

    [SerializeField] private float _fRadius;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _fScanVision = 30;

    // Start is called before the first frame update
    void Start()
    {
        _decision = GetComponent<Decision>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_decision.target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _fRadius, _playerLayer);

            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; ++i)
                {
                    if (_IsPlayerInRange(colliders[i].gameObject.transform))
                    {
                        _decision.UpdateTarget(colliders[i].gameObject.transform);
                        break;
                    }
                }
            }
        }
    }

    private bool _IsPlayerInRange(Transform pTarget)
    {
        if (Vector3.Angle(transform.forward, pTarget.position - transform.position) < _fScanVision)
            return true;

        return false;
    }
}
