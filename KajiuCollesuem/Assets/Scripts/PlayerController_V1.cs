using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V1 : MonoBehaviour
{
    public float speed = 1.0f;

    void Start()
    {

    }

    void Update()
    {
        //Getting Input
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");

        //Move Vector
        Vector3 move = new Vector3(straffe, 0, translation);
        move = Camera.main.transform.TransformDirection(move);
        move = new Vector3(move.x, 0, move.z);
        move = move.normalized * Time.deltaTime * speed;

        //Moving the player
        transform.Translate(move);
    }
}
