using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollect : MonoBehaviour
{
    HUDManager playerHUD;
    public GameObject cell;
    [SerializeField] private float fMaxHeight;
    private bool maxHeightReached = false;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fMaxHeight && !maxHeightReached)
        {
            transform.Translate(Vector3.back * 0.5f);
        }

        else
        {
            maxHeightReached = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Vector3 cellOriginalPos = playerHUD.cellUI.transform.position;

        if (collision.gameObject.tag == "Player")
        {
            playerHUD.cellUIOn = true;
            Destroy(this.gameObject);
            playerHUD.cellUI.SetActive(true);
            playerHUD.cellCounter = playerHUD.cellCounter + 1;
            playerHUD.SetCellCount();
        }
    }
}
