using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class KillPlayerScript : MonoBehaviour {

    [SerializeField] private HealthUI healthBar;
    float health = 1f;

    void Update ()
    {
        if (health == 0)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        FunctionPeriodic.Create(() =>
        {
            if (collision.gameObject.layer == 9)
            {
                while (health > 0)
                {
                    health--;
                    healthBar.SetSize(health);
                }
            }
        }, .01f);
    }
}
