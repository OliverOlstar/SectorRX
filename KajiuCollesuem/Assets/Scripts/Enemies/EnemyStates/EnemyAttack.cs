using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : BaseState
{
    EnemyStateController stateController;

    public EnemyAttack(EnemyStateController controller) : base (controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        throw new NotImplementedException();
    }
}
