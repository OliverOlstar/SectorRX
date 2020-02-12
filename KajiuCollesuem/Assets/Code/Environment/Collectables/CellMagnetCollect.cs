using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellMagnetCollect : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public bool inrange = false;
    private Rigidbody _rb;

    [SerializeField] private float _suckInitialVelocity = 10;
    [SerializeField] private float _suckAcceleration = 10;
    [SerializeField] private Vector3 _suckOffset;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) < 1)
            {
                player.GetComponentInParent<PlayerCollectibles>().CollectedCell();
                Destroy(this.gameObject);
                //player.GetComponent<ParticleSystem>().Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            _rb.AddForce((player.position - transform.position).normalized * Time.deltaTime * _suckAcceleration);
        }
    }

    public void StartSuckUp(Transform pPlayer)
    {
        player = pPlayer;
        _rb.useGravity = false;
        _rb.velocity = ((player.position + _suckOffset) - transform.position).normalized * _suckInitialVelocity;
    }
}
