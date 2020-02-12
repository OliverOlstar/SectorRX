using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCollectibles playerCollectibles = other.GetComponent<PlayerCollectibles>();
            if (playerCollectibles == null)
                playerCollectibles = other.GetComponentInParent<PlayerCollectibles>();

            playerCollectibles.CollectedCell();
            Destroy(transform.parent.gameObject);
        }
    }
}
