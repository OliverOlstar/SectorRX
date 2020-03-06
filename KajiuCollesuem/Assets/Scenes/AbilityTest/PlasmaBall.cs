using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBall : MonoBehaviour
{
    [SerializeField] private Transform _pbSpawnPoint;
    [SerializeField] private Rigidbody _pbPrefab;
    [SerializeField] private float _pbSpeed, _pbScaleIncrementRate;
    private Rigidbody _plasmaBallInstance = null;
    private bool _releasePlasmaBall = false;

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
           }

            else
                _plasmaBallInstance.transform.localScale += Vector3.one * _pbScaleIncrementRate;

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
