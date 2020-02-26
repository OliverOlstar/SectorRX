using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHitbox : MonoBehaviour
{
    [SerializeField] private float _KnockupForce = 10.0f;
    [SerializeField] private int _Damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(_Damage, Vector3.up * _KnockupForce, null, true);

            // Reset Falling force
            OnGroundComponent otherOnGround = other.GetComponent<OnGroundComponent>();

            if (otherOnGround != null)
                otherOnGround.ResetFallingForce();
        }
    }
}
