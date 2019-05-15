using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float searchZone = 10.0f;
    Rigidbody rb;
    [SerializeField] Transform player;
    [SerializeField] Transform raycastPoint;
    RaycastHit hit;

    Vector3 rotator = new Vector3(0, 1);

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 mover = new Vector3(0, 0, moveSpeed * Time.deltaTime);

        Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward) * searchZone, Color.red);

        if (Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward), out hit, searchZone))
        {
            Debug.Log("HIT");
            //player.TransformDirection(Vector3.forward * moveSpeed);
            //rb.AddForce(mover * moveSpeed);
            player.Translate(mover, Space.Self);
        }
        else
        {
            player.Rotate(rotator);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
