using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    private GameObject gameObject;
    private Transform transform;

    public EnemyBaseState(GameObject obj)
    {
        this.gameObject = obj;
        this.transform = obj.transform;
    }

    // The update function for this class.
    // Override in subsequent state classes.
    public abstract Type Tick();
    // The start function for this class.
    public abstract void Enter();
    // Function that runs before state exits.
    public abstract void Exit(); 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
