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
        CheckIfInRange();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Slider slide = null;

        if (collision.gameObject.tag == "Enemy")
        {
            //slide = GameObject.Find("PlayerSlider").GetComponent<Slider>();
            playerVars.takeDamage(10);
        }

        /*
        else if (collision.gameObject.name.Contains("Player"))
        {
            slide = GameObject.Find("AISlider").GetComponent<Slider>();
        }
        
        if (slide != null)
        {
            slide.value -= 10;
        }
        */
    }

    void CheckIfInRange()
    {
        float distance = Vector3.Distance(transform.position, _Enemy.position);

        if(distance < 5)
        {
            playerVars.takeDamage(0.01f);
        }
    }
}
