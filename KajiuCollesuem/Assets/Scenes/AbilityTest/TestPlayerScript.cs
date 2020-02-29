using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _charge = false, _space = false, _isTouchingGround = true, _onGroundCharge = false;
    private float _force = 0, _move, _rotate, _inAirCheckTime = 0;
    [SerializeField] private float _moveSpeed = 10, _rotateSpeed = 50, _maxForce, _incForce, _incTime, _halfPlayerHeight = 0.52f, _jumpUpForce = 4,
        _cooldown = 1, _knockbackForce;
    [SerializeField] private int _inflictDamage;
    [SerializeField] private ParticleSystem _ps;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        _rotate = Input.GetAxis("Horizontal");

        //Input for charge hold
        if (Input.GetKey(KeyCode.C))
            _charge = true;
        else
            _charge = false;

        //Input to jump (while charging or not charging)
        if (Input.GetKeyDown(KeyCode.Space) && !_charge)
            _space = true;

        //Runs claculation for charge
        if (!_charge && _force <= 0)
        {
            _ps.Stop();
            transform.Translate(new Vector3(0, 0, _move));
        }
        else if (_charge && _force < _maxForce)
        {
            _force += (_force / _incTime) + _incForce;
            Debug.Log(_force);
            _ps.Play();
            _onGroundCharge = true;
        }
        
        //Rotates player weather charging or not
        transform.Rotate(new Vector3(0, _rotate, 0));
        
        _inAirCheckTime += Time.time;

        if (_inAirCheckTime > 2)
            _inAirCheckTime = 0;

        _isTouchingGround = IsOnGround();

        //Configures jump settings to ensure player stops charging when landing on ground
        if (_inAirCheckTime < 2 && !_isTouchingGround)
            _onGroundCharge = false;

        //Checks if player is on ground after jump to interrupt charge while maintaining momentum
        if (_isTouchingGround && !_onGroundCharge && !_charge)
            InterruptCharge();
    }

    private void FixedUpdate()
    {
        //Simulates charge physics
        if (_force > 0 && !_charge)
        {
            _rb.AddForce(transform.forward * _force, ForceMode.Impulse);

            //Helps with changing direction while adding force to the charge
            if (_rotate > 0 || _rotate < 0)
            {
                Quaternion q = Quaternion.AngleAxis(_rotate, transform.up) * transform.rotation;
                _rb.MoveRotation(q);
                float mag = _rb.velocity.magnitude;
                _rb.velocity = transform.forward * mag;
            }
            --_force;
        }

        //Simulates jump while charging
        if (_space)
        {
            _rb.AddForce((transform.forward * _force) + (Vector3.up * _jumpUpForce), ForceMode.Impulse);
            _space = false;
        }
    }

    private bool IsOnGround()
    {
        // Linecast get two points
        Vector3 lineStart = transform.position;
        Vector3 vectorToSearch = new Vector3(lineStart.x, lineStart.y - _halfPlayerHeight, lineStart.z);

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

    //Stops force when colliding with incoming object or enemy
    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj.name.Equals("Plane"))
        {
            if (obj.tag.Equals("Enemy"))
            {
                Debug.Log("Damage from charge");
                obj.GetComponent<EnemyAttributes>().TakeDamage(_inflictDamage, _rb.transform.forward.normalized * _knockbackForce, null);
            }
            InterruptCharge();
        }
    }

    //Interrupt charge
    private void InterruptCharge()
    {
        _rb.velocity = Vector3.zero;
    }
}
