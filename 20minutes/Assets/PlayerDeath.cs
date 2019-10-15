using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject restart;
    [SerializeField] private PlayerCamera cam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            Destroy(this.gameObject);
            restart.SetActive(true);
            cam.CameraDisabled = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
