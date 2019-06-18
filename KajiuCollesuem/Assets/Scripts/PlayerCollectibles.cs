using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    //Variables to keep track of Cells and Cores
    private int cellCounter;
    private int coreCounter;
    public Text cellCount;
    public Text coreCount;
    public GameObject cellUI;
    public GameObject coreUI;
    public GameObject newUILocation;

    //Booleans to check if Cell UI or Power Core UI are already active when collecting other item
    public bool cellUIOn;
    public bool coreUIOn;

    // Start is called before the first frame update
    void Start()
    {
        cellUI.SetActive(false);
        coreUI.SetActive(false);
        SetCellCount();
        SetCoreCount();
    }

    //Sets count values to text in UI
    public void SetCellCount()
    {
        cellCount.text = cellCounter.ToString();
    }

    public void SetCoreCount()
    {
        coreCount.text = coreCounter.ToString();
    }

    //If player collides with either collectible
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 cellOriginalPos = cellUI.transform.position;
        Vector3 coreOriginalPos = coreUI.transform.position;

        if (collision.gameObject.tag == "Cell")
        {
            cellUIOn = true;
            Destroy(collision.gameObject);
            cellUI.SetActive(true);
            cellCounter = cellCounter + 1;
            SetCellCount();
            //Check if Core UI is already active
            if (coreUIOn == true)
            {
                cellUI.transform.position = newUILocation.transform.position;
                StartCoroutine("CellUIOff");
            }
            else
            {
                cellUI.transform.position = cellOriginalPos;
                StartCoroutine("CellUIOff");
            }
        }

        if (collision.gameObject.tag == "Core")
        {
            coreUIOn = true;
            Destroy(collision.gameObject);
            coreUI.SetActive(true);
            coreCounter = coreCounter + 1;
            SetCoreCount();
            //Check if Cell UI is already active
            if(cellUIOn == true)
            {
                coreUI.transform.position = newUILocation.transform.position;
                StartCoroutine("CoreUIOff");
            }
            else
            {
                coreUI.transform.position = coreOriginalPos;
                StartCoroutine("CoreUIOff");
            }
        }
    }

    //Turn collectible UIs off after a couple seconds have passed
    IEnumerator CellUIOff()
    {
        yield return new WaitForSeconds(2.0f);
        cellUI.SetActive(false);
        cellUIOn = false;
    }

    IEnumerator CoreUIOff()
    {
        yield return new WaitForSeconds(2.0f);
        coreUI.SetActive(false);
        coreUIOn = false;
    }
}
