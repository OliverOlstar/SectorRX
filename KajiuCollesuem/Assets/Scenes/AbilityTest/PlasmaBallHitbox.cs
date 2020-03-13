using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallHitbox : MonoBehaviour
{
    public float halfObjHeight;
    [SerializeField] private GameObject _plasmaBallField;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _radius, _maxTime;
    [SerializeField] private int _damage;
    private Rigidbody _rb;
    private bool _enableTimer = false, _enablePBField = false;
    private float _timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnGround() && !_enableTimer)
        {
            _rb.isKinematic = true;
            //_plasmaBallField.SetActive(true);
            _enableTimer = true;
            _enablePBField = true;
        }

        else if (_enableTimer)
        {
            _timer += Time.deltaTime;

            if (_timer > _maxTime)
                Destroy(this.gameObject);
        }

        if (_enablePBField)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _enemyLayer);

            if (colliders.Length > 0)
            {
                foreach (Collider col in colliders)
                {
                    col.gameObject.GetComponent<EnemyAttributes>().TakeDamage(_damage, Vector3.zero, this.gameObject);
                }
            }
            _enablePBField = false;
        }
    }

    private bool IsOnGround()
    {
        // Linecast get two points
        Vector3 lineStart = transform.position;
        Vector3 vectorToSearch = new Vector3(lineStart.x, lineStart.y - halfObjHeight, lineStart.z);

        // Debug Line
        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(lineStart, vectorToSearch, color);

        // Linecast
        RaycastHit hitInfo;
        if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
        {
            // On Ground
            return true;
        }

        // Off Ground
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
            other.gameObject.GetComponent<EnemyAttributes>().TakeDamage(_damage * 2, Vector3.zero, this.gameObject);
    }
}
