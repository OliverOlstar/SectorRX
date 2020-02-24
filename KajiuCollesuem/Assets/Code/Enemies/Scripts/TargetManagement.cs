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
    [SerializeField] private float _triggerDistance = 5;
    [HideInInspector] public PlayerAttributes playerAttributes;

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
                    playerAttributes = colliders[i].gameObject.GetComponent<PlayerAttributes>();
                    Transform playerTransform = colliders[i].gameObject.transform;
                    float distance = Vector3.Distance(transform.position, playerTransform.position);

                    if ((_IsPlayerInRange(playerTransform) || distance < _triggerDistance) && !playerAttributes.IsDead())
                    {
                        _decision.UpdateTarget(playerTransform);
                        break;
                    }
                }
            }
        }

        else if (playerAttributes.IsDead())
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
