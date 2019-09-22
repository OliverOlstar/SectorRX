using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEventHandler : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;

    public void DeactivateHitbox() => hitbox.SetActive(false);
    public void ActivateHitbox() => hitbox.SetActive(true);
}
