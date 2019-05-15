using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Slider slide = null;

        if (collision.gameObject.name.Contains("Dummy"))
        {
            slide = GameObject.Find("AISlider").GetComponent<Slider>();
        }

        else if (collision.gameObject.name.Contains("Player"))
        {
            slide = GameObject.Find("PlayerSlider").GetComponent<Slider>();
        }
        
        if (slide != null)
        {
            slide.value -= 10;
        }
    }
}
