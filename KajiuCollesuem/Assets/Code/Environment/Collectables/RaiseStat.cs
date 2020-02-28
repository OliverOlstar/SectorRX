using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseStat : MonoBehaviour
{
    [SerializeField] private PlayerCollectibles.Upgrades statType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerCollectibles otherCollectibles = other.gameObject.GetComponentInParent<PlayerCollectibles>();

            if (otherCollectibles != null)
            {
                otherCollectibles.CollectedItem(statType);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}