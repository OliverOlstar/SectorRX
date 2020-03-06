using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseStat : MonoBehaviour
{
    [SerializeField] private PlayerCollectibles.Upgrades statType;

    [Space]
    private float _CanCollectDelay = 0.1f;
    private bool _CanCollect = false;

    private void Start()
    {
        StartCoroutine("EnableCollectDelay");
    }

    private IEnumerator EnableCollectDelay()
    {
        yield return new WaitForSeconds(_CanCollectDelay);
        _CanCollect = true;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (_CanCollect && other.gameObject.tag == "Player")
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