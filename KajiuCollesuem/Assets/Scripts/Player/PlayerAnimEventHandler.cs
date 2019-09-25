using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    private PlayerHitbox playerHitbox;

    [SerializeField] private AnimHandler animHandler;

    private void Start()
    {
        playerHitbox = hitbox.GetComponent<PlayerHitbox>();
    }


    public void DeactivateHitbox()
    {
        hitbox.SetActive(false);
    }

    public void ActivateHitbox()
    {
        hitbox.SetActive(true);
        playerHitbox.SetDamage(animHandler.GetCurrentCombo());
    }


    public void ListenForAttack()
    {
        animHandler.attackState = 1;
        //Debug.Log("AnimEventHandler: ListenForAttack");
    }

    public void LeaveAttacking()
    {
        animHandler.StopAttacking();
    }

    public void LeaveAttackState()
    {
        animHandler.LeaveAttackState();
    }
}
