using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour
{
    // start here
    private Quaternion _lookRotation;

    private Vector3 _direction;

    public float speed;

    //public float angleSpeed = 5;

    public enum SharkStates
    {
        jumping,
        roaming,
        fin
    }

    public SharkStates currentState;

    public float BreachTime;

    public Transform player;

    public float rand;

    private JumpScript jump;

    public Transform jumpStart;

    public Transform jumpHeight;

    public Transform jumpEnd;

    public Transform cameraPos;

    public Transform finFront;

    public Transform finBack;

    private Vector3 toUpPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        jump = GetComponent<JumpScript>();
        BreachTime = Random.Range(8, 15);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            jumpStart.position = new Vector3(player.position.x, player.position.y, player.position.z);

            jumpHeight.position = new Vector3(player.position.x, player.position.y + 5, player.position.z + 15);

            jumpEnd.position = new Vector3(player.position.x, player.position.y, player.position.z + 30);

            jump.FollowParabola();
        }

        switch (currentState)
        {
            case SharkStates.roaming:
                Roaming();
                break;

            case SharkStates.jumping:
                Jumping();
                break;

            case SharkStates.fin:
                Fin();
                break;
        }
    }

    public void Roaming()
    {
        if (BreachTime <= 0)
        {
            rand = Random.Range(0, 2);

            if (rand == 1) //Jump
            {
                jumpStart.position = new Vector3(player.position.x, player.position.y, player.position.z);

                jumpHeight.position = new Vector3(player.position.x, player.position.y + 5, player.position.z + 15);

                jumpEnd.position = new Vector3(cameraPos.position.x, cameraPos.position.y - 5, cameraPos.position.z + 30);

                jump.FollowParabola();

                currentState = SharkStates.jumping;
            }
            else
            {
                currentState = SharkStates.fin;
            }
        }
        else
        {
            BreachTime -= 1 * Time.deltaTime;
        }
    }

    public void Jumping()
    {

    }

    public void Fin()
    {

        //Moves the Shark Forward Along X Axis while staying on the terrains Y
        transform.Translate(transform.forward * Time.deltaTime);

        Vector3 pos = transform.position;

        pos.y = Terrain.activeTerrain.SampleHeight(transform.position);

        transform.position = pos;

        //Rotation

        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            //Get y position
            pos.y = (hit.point + Vector3.up * 2).y;

            //Get rotation
            toUpPos = hit.normal;
        }

        //Assign rotation/axis of the Object
        transform.up = Vector3.Slerp(transform.up, toUpPos, 5 * Time.deltaTime);

    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }
}