using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    private List<GameObject> Original = new List<GameObject>();
    private List<GameObject> Broken = new List<GameObject>();
    [SerializeField] private GameObject BrokenBuilding;

    [SerializeField] private float DownWardForce;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in transform)
        {
            Original.Add(t.gameObject);
        }

        foreach(Transform t in BrokenBuilding.transform)
        {
            t.gameObject.SetActive(false);
            Broken.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        for(int i = 0; i < Original.Count; i++)
        {
            Broken[i].SetActive(true);
            Broken[i].transform.position = Original[i].transform.position;
            Original[i].SetActive(false);
            //Broken[i].GetComponent<Rigidbody>().AddForce(transform.up * -DownWardForce);
        }
    }
}
