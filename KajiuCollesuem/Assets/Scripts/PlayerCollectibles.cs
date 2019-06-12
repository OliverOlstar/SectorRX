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
        if (collision.gameObject.tag == "Cell")
        {
            Destroy(collision.gameObject);
            cellUI.SetActive(true);
            cellCounter = cellCounter + 1;
            SetCellCount();
            StartCoroutine("CellUIOff");
        }

        if (collision.gameObject.tag == "Core")
        {
            Destroy(collision.gameObject);
            coreUI.SetActive(true);
            coreCounter = coreCounter + 1;
            SetCoreCount();
            StartCoroutine("CoreUIOff");
        }
    }

    IEnumerator CellUIOff()
    {
        yield return new WaitForSeconds(2.0f);
        cellUI.SetActive(false);
    }

    IEnumerator CoreUIOff()
    {
        yield return new WaitForSeconds(2.0f);
        coreUI.SetActive(false);
    }
}
