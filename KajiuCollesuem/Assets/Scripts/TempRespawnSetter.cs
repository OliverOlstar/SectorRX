using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRespawnSetter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
        if (playerRespawn)
            playerRespawn.setRespawinPoint(other.transform.position);
    }
}
