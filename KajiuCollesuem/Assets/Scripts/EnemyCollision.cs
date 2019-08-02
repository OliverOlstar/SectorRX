using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour
{
    private PlayerAttributes playerVars;
    private Transform _Enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        playerVars = GetComponent<PlayerAttributes>();
        _Enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerVars.TakeDamage(10);
        }
    }
}
