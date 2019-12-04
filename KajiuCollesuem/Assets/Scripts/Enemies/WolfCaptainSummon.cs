using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCaptainSummon : MonoBehaviour
{
    public GameObject summonSpotOne;
    public GameObject summonSpotTwo;
    public GameObject summonSpotThree;

    public GameObject gruntHellhound;
    public bool gruntsSummoned = false;

    WolfieMovement alphaMovement;
    public Animator anim;

    void Start()
    {
        alphaMovement = GetComponent<WolfieMovement>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        /*if (Vector3.Distance(alphaMovement.player.position, this.transform.position) < 20.0f && gruntsSummoned == false)
        {
            SummonGrunts();
            gruntsSummoned = true;
        }*/
    }

    void SummonGrunts()
    {
        Instantiate(gruntHellhound, summonSpotOne.transform.position, summonSpotOne.transform.rotation);
        Instantiate(gruntHellhound, summonSpotTwo.transform.position, summonSpotTwo.transform.rotation);
        Instantiate(gruntHellhound, summonSpotThree.transform.position, summonSpotThree.transform.rotation);
    }
}
