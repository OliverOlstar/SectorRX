using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Allows each player to collect their own cache of cells for upgrading*/

public class PlayerCollectibles : MonoBehaviour
{
    [SerializeField] private HUDManager _PlayerHUD;

    private void Start()
    {
        //playerHUD = transform.parent.GetComponentInChildren<HUDManager>();
    }

    public void CollectedCell()
    {
        _PlayerHUD.cellUIOn = true;
        _PlayerHUD.cellUI.SetActive(true);
        _PlayerHUD.cellCounter = _PlayerHUD.cellCounter + 100;
        _PlayerHUD.SetCellCount();
    }
}
