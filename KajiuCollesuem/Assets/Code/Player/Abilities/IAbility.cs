using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Pressed();
    void Tick();
    void Exit();
    void Upgrade(float pValue);
}
