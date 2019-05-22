using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour
{
    private PlayerAttributes playerVars;

    // Start is called before the first frame update
    void Start()
    {
        playerVars = GetComponent<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
