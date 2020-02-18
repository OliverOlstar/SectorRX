using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseStat : MonoBehaviour
{
    [SerializeField] private PlayerCollectibles.Upgrades variableName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<PlayerCollectibles>().CollectedItem(variableName);
            Destroy(transform.parent.gameObject);
        }
    }
}