using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttributes
{
    bool IsDead();
    bool TakeDamage(int pDamage, Vector3 pKnockback, GameObject pAttacker, string pTag, bool pIgnoreWeight = false);
}
