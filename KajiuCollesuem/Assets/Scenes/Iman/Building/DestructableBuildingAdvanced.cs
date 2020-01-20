using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBuildingAdvanced : MonoBehaviour
{
    [SerializeField] private List<GameObject> Original = new List<GameObject>();
    [SerializeField] private List<GameObject> Broken = new List<GameObject>();
    [SerializeField] private GameObject BrokenBuilding;

    [SerializeField] private float PushForce;
    private int PiecesDestroyed;
    [SerializeField] private int HitsTillDestroyed;
    private int PiecesDestroyedEachHit;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            Original.Add(t.gameObject);
        }

        foreach (Transform t in BrokenBuilding.transform)
        {
            t.gameObject.SetActive(false);
            Broken.Add(t.gameObject);
        }
        CalculatePiecesToDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyBuilding();
        }
    }

    private void DestroyBuilding()
    {
        if (PiecesDestroyed < Original.Count)
        {
            for (int i = PiecesDestroyed; i < PiecesDestroyedEachHit; i++)
            {
                if (Broken[i] != null)
                {
                    Broken[i].SetActive(true);
                    Broken[i].transform.position = Original[i].transform.position;
                    Original[i].SetActive(false);
                    //Vector3 Direction = -(gameObject.transform.position - Broken[i].transform.position).normalized;
                    //Broken[i].GetComponent<Rigidbody>().AddForce(Direction * PushForce);
                    PiecesDestroyed++;
                }
            }
            PiecesDestroyedEachHit += PiecesDestroyedEachHit;
        }
    }

    private void CalculatePiecesToDestroy()
    {
        PiecesDestroyedEachHit = Original.Count / HitsTillDestroyed;
    }
}
