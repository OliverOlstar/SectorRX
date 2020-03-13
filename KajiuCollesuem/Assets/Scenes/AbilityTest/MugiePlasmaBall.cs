using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugiePlasmaBall : MonoBehaviour
{
    [SerializeField] private Transform _pbSpawnPoint;
    [SerializeField] private Rigidbody _pbPrefab;
    [SerializeField] private float _pbSpeed, _pbScaleIncrementRate, _maxTime;
    private Rigidbody _plasmaBallInstance = null;
    private bool _releasePlasmaBall = false;
    private float _timer = 0;
    private PlasmaBallHitbox _pbHitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_releasePlasmaBall && Input.GetKey(KeyCode.V))
        {
           if (!_plasmaBallInstance) {
                _plasmaBallInstance = Instantiate(_pbPrefab) as Rigidbody;
                _plasmaBallInstance.transform.position = _pbSpawnPoint.position;
                _pbHitbox = _plasmaBallInstance.GetComponent<PlasmaBallHitbox>();
           }

            else
            {
                _plasmaBallInstance.transform.localScale += Vector3.one * _pbScaleIncrementRate;
                _timer += Time.deltaTime;

                if (_timer > _maxTime)
                {
                    _pbHitbox.halfObjHeight += _pbScaleIncrementRate;
                    _timer = 0;
                }
            }

            _releasePlasmaBall = Input.GetKeyUp(KeyCode.V);
        }

        else if (_plasmaBallInstance)
        {
            _plasmaBallInstance.isKinematic = false;
            _plasmaBallInstance.AddForce((transform.forward * _pbSpeed) + Vector3.up, ForceMode.Impulse);
            _plasmaBallInstance = null;
            _releasePlasmaBall = false;
        }
    }
}
