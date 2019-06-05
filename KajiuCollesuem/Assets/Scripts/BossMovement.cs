using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    private NavMeshAgent boss;
    public GameObject player;

    public Vector3 radius;
    public float radiusMagnitude;
    public float centerAngle = 0.0f;
    public float innerAngle = 0.0f;
    public float innerAngeleRadians = 0.0f;
    public float movementAngle = 0.0f;

    public float outsideLength = 0.0f;

    public float xComponent;
    public float zComponent;

    public Vector3 x;
    public Vector3 z;

    public Vector3 destination;
       

    // Start is called before the first frame update
    void Start()
    {
        boss = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //ensure rotation is  towards player???
        transform.LookAt(player.transform.position);


        //if no destination for straffe, then set
        if(!boss.hasPath)
        {
            Straffe_1();
        }        
    }

    //doesn't work
    void Straffe_1()
    {
        //get vector between player boss
        radius = player.transform.position - transform.position;
        radiusMagnitude = radius.magnitude;

        // 1 divided by magnitude of vector gives center angle
        centerAngle = (1 / radiusMagnitude) * 90;
        // isoceles means outside angles are equal
        innerAngle = (180 - centerAngle) / 2;
        innerAngeleRadians = (innerAngle * Mathf.PI) / 180.0f;
        movementAngle = 90 - innerAngle;

        //determine magnitude of outside length
        outsideLength = 2 * radiusMagnitude * Mathf.Cos(innerAngeleRadians);

        //double outsideLength for full isosceles triangle
        //outsideLength = outsideLength * 2;

        //determine the components
        xComponent = outsideLength * Mathf.Cos(90 - innerAngle);
        zComponent = outsideLength * Mathf.Sin(90 - innerAngle);

        //x = new Vector3(xComponent, 0, 0);
        //z = new Vector3(0, 0, zComponent);

        destination = transform.position + (-transform.right * xComponent) + (transform.forward * zComponent);

        boss.SetDestination(destination);
    }   
}
