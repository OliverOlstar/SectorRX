using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{

    [SerializeField] private GameObject Ragdoll;
    [SerializeField] private GameObject MainCharacterModel;
    [SerializeField] private GameObject MainCharacter;

    private Transform[] RagDollParts;
    private Transform[] MainCharParts;

    void Start()
    {
        RagDollParts = Ragdoll.GetComponentsInChildren<Transform>();
        MainCharParts = MainCharacterModel.GetComponentsInChildren<Transform>();

        Ragdoll.transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator ragdollDelay()
    {
        yield return new WaitForSeconds(0.1f);

        Ragdoll.transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 1; i < RagDollParts.Length - 1; i++)
        {
            RagDollParts[i].position = MainCharParts[i].position;
            RagDollParts[i].rotation = MainCharParts[i].rotation;
        }
        MainCharacter.SetActive(false);
    }

    public void SwitchToRagdoll()
    {
        StartCoroutine("ragdollDelay");
    }
}
