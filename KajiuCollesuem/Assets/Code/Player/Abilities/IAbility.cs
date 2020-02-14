using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Pressed();
    void Released();
    void Exit();
    void Upgrade();
}
