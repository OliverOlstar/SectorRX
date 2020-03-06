using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallHitbox : MonoBehaviour
{
    [SerializeField] private float _halfPlayerHeight;
    [SerializeField] private GameObject _plasmaBallField;

    /* I was thinking we could have a class variable for damage, and if the plasma ball hits the enemy directly, we multiply the
     * damage variable for twice the damage, and pass that into the TakeDamage method. If not we just pass in the damage value as
     * it is.
     * */
    public static float damage;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnGround())
        {
            _plasmaBallField.SetActive(true);
            Debug.Log("Burn everything within collision sphere");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
            Destroy(this.gameObject);
    }
}
