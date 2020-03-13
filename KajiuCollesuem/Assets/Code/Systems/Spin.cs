using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private Vector3 _Axis;
    [SerializeField] private float _Speed;

    // Start is called before the first frame update
    void Start()
    {
        _Axis.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(_Axis * _Speed * Time.deltaTime);
    }
}
