using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellMagnetCollect : MonoBehaviour
{
    [HideInInspector] public Transform player;
    [HideInInspector] public bool inrange = false;
    private Rigidbody _rb;
    private NavMeshAgent _agent;
    private ParticleSystem _pSystem;
    private ParticleSystem.EmissionModule _emission;
    private ParticleSystem.ShapeModule _shape;
    private ParticleSystem.LimitVelocityOverLifetimeModule _lvot;

    [SerializeField] private float _suckInitialVelocity = 10;
    [SerializeField] private float _suckAcceleration = 10;
    [SerializeField] private Vector3 _suckOffset;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _pSystem = GetComponent<ParticleSystem>();
        _emission = _pSystem.emission;
        _shape = _pSystem.shape;
        _lvot = _pSystem.limitVelocityOverLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
            /*if (inrange)
            {
                _agent.SetDestination(player.position);
            }*/

            if (Vector3.Distance(transform.position, player.position) < 1)
            {
                player.GetComponentInParent<PlayerCollectibles>().CollectedCell();
                /*_shape.shapeType = ParticleSystemShapeType.Sphere;
                _shape.radius = 0.5f;
                ParticleSystem.Burst burst = new ParticleSystem.Burst(0, 50, 50);
                _emission.SetBurst(0, burst);
                _emission.rateOverTime = 0;
                _emission.rateOverDistance = 0;
                _lvot.enabled = true;
                _lvot.dampen = 0.4f;
                _pSystem.Play();*/
                Destroy(this.gameObject);
                player.GetComponent<ParticleSystem>().Play();
            }

            /*if (Vector3.Angle(transform.forward, player.position - transform.position) > 1)
                transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(player.position - transform.position),
                Time.deltaTime * 30);*/
        }
    }

    private void FixedUpdate()
    {
        float current = Time.time, previous = Time.deltaTime;
        float velocity = (current - previous) / Time.deltaTime;

        if (player != null)
        {
            /*if (Vector3.Angle(transform.forward, player.position - transform.position) < 1)
            {
                if (transform.position.y < 1)
                    _rb.AddForce(transform.up * Time.time);
                _rb.AddForce(transform.forward * Time.time);
            }*/
            _rb.AddForce((player.position - transform.position).normalized * Time.deltaTime * _suckAcceleration);
        }
    }

    public void StartSuckUp(Transform pPlayer)
    {
        player = pPlayer;
        _rb.useGravity = false;
        _rb.velocity = (((player.position + _suckOffset) - transform.position).normalized * _suckInitialVelocity);
    }
}
