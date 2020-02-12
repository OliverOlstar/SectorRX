using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmoothRotation : MonoBehaviour
{
    private Decision _decision;
    [SerializeField] private float _rotationSpeed;

    void Awake()
    {
        _decision = GetComponent<Decision>();
        _decision.enemyRotation = this;
    }

    void Update()
    {
        Quaternion targetQ = Quaternion.LookRotation(_decision.target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQ, Time.deltaTime * _rotationSpeed);
    }
}
