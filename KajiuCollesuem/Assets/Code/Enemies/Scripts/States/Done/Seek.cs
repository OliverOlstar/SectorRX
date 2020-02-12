using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seek : AlwaysSeek
{
    [SerializeField] private float _agroLostRange = 10;

    public override bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //If in agro range
        if (pDistance <= _agroLostRange)
            return true;
        
        return false;
    }
}
