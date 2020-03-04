using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private float _frequency = 2.0f;
    [SerializeField] private float _magnitudeMult = 2.0f;
    [SerializeField] private float _reductionRate = 2.0f;
    private float _magnitude = 0.0f;
    private Vector3 _offset;
    private Vector3 _direction;
    private Vector3 _startPos;

    // Start is called before the first frame update
    void Start()
    {
        _direction = new Vector3(Random.value, 0.0f, Random.value).normalized;
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_magnitude > 0)
        {
            _offset = (Mathf.Sin(Time.time * _frequency) - 0.5f) * _magnitude * _direction * _magnitudeMult;
            transform.position = new Vector3(_offset.x + _startPos.x, transform.position.y, _offset.z + _startPos.z);
        }
        else
        {
            transform.position = new Vector3(_startPos.x, transform.position.y, _startPos.z);
        }
    }

    public void AddShake(float pMag, float pInTime, float pHoldTime) 
    {
        StopAllCoroutines();
        StartCoroutine(Shake(pMag, pInTime, pHoldTime));
    }

    IEnumerator Shake(float pMag, float pInTime, float pHoldTime)
    {
        while (_magnitude < pMag)
        {
            yield return null;
            _magnitude += pMag / pInTime * Time.deltaTime;
        }

        _magnitude = pMag;
        yield return new WaitForSeconds(pHoldTime);

        while (_magnitude > 0)
        {
            yield return null;
            _magnitude -= _reductionRate * Time.deltaTime;
        }
    }
}
