using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER");

        if (other.CompareTag("Player"))
        {
            Debug.Log("TRIGGER PLAYER");
            PlayerCollectibles playerCollectibles = other.GetComponent<PlayerCollectibles>();
            if (playerCollectibles == null)
                playerCollectibles = other.GetComponentInParent<PlayerCollectibles>();

            playerCollectibles.CollectedCell();
            Destroy(transform.parent.gameObject);
        }
    }
}
