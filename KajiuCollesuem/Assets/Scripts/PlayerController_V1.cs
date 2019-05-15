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
        float translation = Input.GetAxis("Vertical");
        float straffe = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(straffe, 0, translation).normalized * Time.deltaTime * speed;

        transform.Translate(move);
    }
}
