using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChase : MonoBehaviour
{
    public enum SharkStates
    {
        jumping,
        chase,
        fin
    }

    public SharkStates currentState;

    public GameObject sharkModel;
    public Transform raycastPoint;

    public float hoverHeight = 1.0f;
    public float speed = 20.0f;
    private float terrainHeight;

    [Range(-2.2f, 2.2f)]
    public float rotationAmount;

    private RaycastHit hit;

    private Vector3 pos;
    private Vector3 forwardDirection;

    public Transform player;

    Vector3 target;

    public float Turn;
    public float rotationSpeed = 1;
    public float diag;

    [SerializeField] private float rotBuffer;

    //public LayerMask mask;
    int mask = 1 << 10;

    private void Start()
    {
      
    }

    void Update()
    {
        switch (currentState)
        {
            case SharkStates.chase:
                Chase();
                break;

            case SharkStates.jumping:
                
                break;

            case SharkStates.fin:
               
                break;
        }
        
    }



    public void Chase()
    {
        // Keep at specific height above terrain
        pos = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        transform.position = new Vector3(pos.x, terrainHeight + hoverHeight, pos.z);

        // Rotate to align with terrain
        RaycastHit hit;

        if (Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask))
        {
            transform.up -= (transform.up - hit.normal) * 0.1f;
        }

        // Rotate with input

        //rotationAmount = Input.GetAxis("Horizontal") * 120.0f;
        //rotationAmount *= Time.deltaTime;

        //sharkModel.transform.Rotate(0.0f, rotationAmount, 0.0f);


        Turn = Vector3.Cross(sharkModel.transform.forward, player.position - sharkModel.transform.position).y;

        //sharkModel.transform.rotation = Quaternion.Lerp(sharkModel.transform.rotation, Quaternion.LookRotation(player.transform.position - sharkModel.transform.position), 5 * Time.deltaTime);

        //float this = Quaternion.Euler(Quaternion.Lerp(sharkModel.transform.rotation, Quaternion.LookRotation(player.transform.position - sharkModel.transform.position), 5 * Time.deltaTime).y;

        if (Turn < rotBuffer)
        {
            sharkModel.transform.Rotate(0.0f, -rotationSpeed * Time.deltaTime, 0.0f);
        }
        else if (Turn > rotBuffer)
        {
            sharkModel.transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f);
        }
        else
        {
            Quaternion rot = Quaternion.LookRotation(transform.position - player.position);
            rot *= Quaternion.Euler(0, 1, 0);
            sharkModel.transform.rotation = rot;
            Debug.Log("this");
        }
        //Debug.Log(Turn);

        // Move forward
        forwardDirection = sharkModel.transform.forward;
        transform.position += forwardDirection * Time.deltaTime * speed;
    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
}