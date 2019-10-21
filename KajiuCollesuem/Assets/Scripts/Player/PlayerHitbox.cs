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

    [Space]
    [SerializeField] private int powerRecivedOnHit = 20;

    [Header("Time Slow")]
    [SerializeField] private float timeSlowSeconds = 1f;
    [SerializeField] private float timeSlowAmount = 1f;

    private PlayerAttributes playerAttributes;
    private PlayerLockOnScript lockOnScript;

    private void Start()
    {
        playerAttributes = GetComponentInParent<PlayerAttributes>();
        lockOnScript = playerAttributes.GetComponent<PlayerLockOnScript>();
    }

    private void OnTriggerEnter (Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null)
        {
            if (otherAttributes.TakeDamage(damage, true))
                lockOnScript.TargetDead(other.transform);

            playerAttributes.RecivePower(powerRecivedOnHit);

            StartCoroutine("SlowTime");
        }
    }

    private IEnumerator SlowTime()
    {
        Time.timeScale = timeSlowAmount;
        yield return new WaitForSecondsRealtime(timeSlowSeconds);
        Time.timeScale = 1;
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
