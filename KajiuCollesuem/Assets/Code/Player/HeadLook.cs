using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLook : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_anim.enabled)
        {
            if (_target != null && _target.gameObject.activeSelf)
            {
                _anim.SetLookAtWeight(1, 0, 0.5f, 0.5f, 0.7f);
                _anim.SetLookAtPosition(_target.position);
            }
            else
            {
                _anim.SetLookAtWeight(0);
            }
        }
    }
}
