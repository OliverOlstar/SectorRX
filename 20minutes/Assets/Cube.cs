using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float speedIncrease = 1f;

    [SerializeField] private scor scor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Score Increases
            transform.position = new Vector3((Random.value - 0.5f) * 2 * range, 0.5f, (Random.value - 0.5f) * 2 * range);
            StopCoroutine("Colide");
            StartCoroutine("Colide", other.GetComponent<playerMove>());
        }
    }

    IEnumerator Colide(playerMove pOther)
    {
        yield return null;
        pOther.speed += speedIncrease;
        scor.AddScore(1);
    }
}
