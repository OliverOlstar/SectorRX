using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollect : MonoBehaviour
{
    HUDManager playerHUD;
    public GameObject cell;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        Vector3 cellOriginalPos = playerHUD.cellUI.transform.position;

        if (collision.gameObject.tag == "Player")
        {
            playerHUD.cellUIOn = true;
            Destroy(cell);
            playerHUD.cellUI.SetActive(true);
            playerHUD.cellCounter = playerHUD.cellCounter + 1;
            playerHUD.SetCellCount();
        }
    }
}
