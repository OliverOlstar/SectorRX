using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    public float _ShotForce = 10.0f;
    public Vector3 _BulletSize = new Vector3(.3f, .3f, .3f);

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //transform.localScale = _BulletSize;
        
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnEnable()
    {
        StopCoroutine(Disable());
        _rb.AddForce(transform.forward * _ShotForce, ForceMode.Impulse);
        StartCoroutine(Disable());
    }

    public void SetShotForce(float force)
    {
        _ShotForce = force;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        transform.parent = GameObject.Find("Magazine").GetComponent<Transform>();
    }
}
