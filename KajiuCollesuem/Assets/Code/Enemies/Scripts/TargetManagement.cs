using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManagement : MonoBehaviour
{
    private Decision _decision;

    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _visionRadius = 15;
    [SerializeField] private float _visionDistance = 30;
    private PlayerAttributes _playerAttributes;

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
            Collider[] colliders = Physics.OverlapSphere(transform.position, _visionRadius, _playerLayer);

            if (colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; ++i)
                {
                    _playerAttributes = colliders[i].gameObject.GetComponent<PlayerAttributes>();

                    if (_IsPlayerInRange(colliders[i].gameObject.transform) && !_playerAttributes.IsDead())
                    {
                        _decision.UpdateTarget(colliders[i].gameObject.transform);
                        break;
                    }
                }
            }
        }

        else if (_playerAttributes.IsDead())
        {
            _decision.UpdateTarget(null);
            _decision.ForceStateSwitch(GetComponent<Guard>());
        }
    }

    private bool _IsPlayerInRange(Transform pTarget)
    {
        if (Vector3.Angle(transform.forward, pTarget.position - transform.position) < _visionDistance)
            return true;

        return false;
    }
}
