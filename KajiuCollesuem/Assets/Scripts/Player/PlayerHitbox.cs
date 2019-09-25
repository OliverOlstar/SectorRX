using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private int lightAttackDamage_S1 = 1;
    [SerializeField] private int lightAttackDamage_S2 = 1;
    [SerializeField] private int lightAttackDamage_S3 = 1;
    [SerializeField] private int heavyAttackDamage = 2;
    private int damage;

    [SerializeField] private int powerRecivedOnHit = 20;

    private PlayerAttributes playerAttributes;

    private void Start()
    {
        playerAttributes = GetComponentInParent<PlayerAttributes>();
    }

    private void OnTriggerEnter (Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(damage, true);
            playerAttributes.RecivePower(powerRecivedOnHit);
        }
    }

    public void SetDamage(int pIndex)
    {
        switch (pIndex)
        {
            case 1:
                damage = lightAttackDamage_S1;
                break;

            case 2:
                damage = lightAttackDamage_S2;
                break;

            case 3:
                damage = lightAttackDamage_S3;
                break;

            case 4:
                damage = heavyAttackDamage;
                break;
        }
    }
}
