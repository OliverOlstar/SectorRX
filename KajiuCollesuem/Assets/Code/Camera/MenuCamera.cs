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

    [Space]
    [SerializeField] private float _ScaleMult = 0.1f;
    [SerializeField] private float _ScaleOffset = 1.0f;
    public float scaleOffset = 1.0f;
    private Camera _Camera;

    private int curIndex;

    private void Awake()
    {
        _Camera = GetComponent<Camera>();

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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_rotationEulers[curIndex]), Time.deltaTime * _rotationDampening);
         
        _Camera.orthographicSize =  Mathf.Abs(Screen.width - Screen.height) * _ScaleMult + _ScaleOffset + scaleOffset;
    }

    public void ToggleCamera(int pIndex) => curIndex = pIndex;
}
