using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{

    [SerializeField] private GameObject Ragdoll;
    [SerializeField] private GameObject MainCharacterModel;
    [SerializeField] private GameObject MainCharacter;

    //[SerializeField] private List<GameObject> RagDollParts = new List<GameObject>();
    [SerializeField] private Transform[] RagDollParts;
    //[SerializeField] private List<GameObject> MainCharParts = new List<GameObject>();
    [SerializeField] private Transform[] MainCharParts;

    // Start is called before the first frame update
    void Start()
    {
        RagDollParts = Ragdoll.GetComponentsInChildren<Transform>();
        MainCharParts = MainCharacterModel.GetComponentsInChildren<Transform>();

        Ragdoll.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RagdollActive();
            print("hello");
        }
    }

    private void RagdollActive()
    {
        Ragdoll.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < RagDollParts.Length - 1; i++)
        {
            RagDollParts[i].position = MainCharParts[i].position;
            RagDollParts[i].rotation = MainCharParts[i].rotation;
        }
        MainCharacter.SetActive(false);
    }
}
