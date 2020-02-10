using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCollect : MonoBehaviour
{
    HUDManager playerHUD;
    public GameObject core;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Vector3 coreOriginalPos = playerHUD.coreUI.transform.position;

        //if (collision.gameObject.tag == "Core")
        //{
        //    playerHUD.coreUIOn = true;
        //    Destroy(collision.gameObject);
        //    playerHUD.coreUI.SetActive(true);
        //    playerHUD.coreCounter = playerHUD.coreCounter + 1;
        //    playerHUD.SetCoreCount();
        //}
    }
}
