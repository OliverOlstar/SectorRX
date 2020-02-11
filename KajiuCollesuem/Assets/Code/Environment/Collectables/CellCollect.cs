using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollect : MonoBehaviour
{
    //private HUDManager _playerHUD;
    //[SerializeField] private float _fMaxHeight;
    //private bool _maxHeightReached = false;

    //private void Start()
    //{
        //_playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    //}

    // Update is called once per frame
    //void Update()
    //{
        //if (transform.position.y < _fMaxHeight && !_maxHeightReached)
        //{
        //    transform.Translate(Vector3.back * 0.5f);
        //}
        //else
        //{
        //    _maxHeightReached = true;
        //}
    //}

    private void OnTriggerEnter(Collider collision)
    {
        //Vector3 cellOriginalPos = _playerHUD.cellUI.transform.position;
        //Debug.Log("HERE");
        CellMagnetCollect cmc = GetComponentInParent<CellMagnetCollect>();
        cmc.inrange = true;

        if (collision.gameObject.tag == "Player")
        {
            cmc.player = collision.transform;

            //collision.GetComponent<PlayerCollectibles>().CollectedCell();
            //Destroy(this.gameObject);
        }
    }
}
