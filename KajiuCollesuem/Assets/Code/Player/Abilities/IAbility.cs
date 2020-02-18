using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Pressed(AbilityState pState);
    void Released();
    void Tick();
    void Exit();
    void Upgrade();
}
