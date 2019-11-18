using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseRootMotion : MonoBehaviour
{
    private Animator _anim;
    void Start() => _anim = GetComponent<Animator>();
    
    void OnAnimatorMove()
    {
        transform.parent.parent.rotation = _anim.rootRotation;
        transform.parent.parent.position += _anim.deltaPosition;
    }
}
