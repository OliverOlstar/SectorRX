using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayerScript : MonoBehaviour {

    public GameObject NavMesh;

    void Start()
    {
        NavMesh.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<MenuController>())
        {
            NavMesh.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<MenuController>())
        {
            NavMesh.SetActive(false);
        }
    }
}
