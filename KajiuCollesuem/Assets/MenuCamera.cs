using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private Vector3[] _rotationEulers = new Vector3[2];
    private Quaternion[] _rotations;
    [SerializeField] private Vector3[] _postions = new Vector3[2];
    [SerializeField] private float _rotationDampening = 10.0f;
    [SerializeField] private float _positionDampening = 10.0f;

    private int curIndex;

    private void Awake()
    {
        // Pre convert eulers to quaternions
        _rotations = new Quaternion[_rotationEulers.Length];

        for (int i = 0; i < _rotationEulers.Length; i++)
        {
            _rotations[i] = Quaternion.Euler(_rotationEulers[i]);
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _postions[curIndex], Time.deltaTime * _positionDampening);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotations[curIndex], Time.deltaTime * _rotationDampening);
    }

    public void ToggleCamera(int pIndex) => curIndex = pIndex;
}
