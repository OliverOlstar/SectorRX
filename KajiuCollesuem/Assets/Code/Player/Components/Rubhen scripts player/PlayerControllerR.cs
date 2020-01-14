using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerR : MonoBehaviour
{
    //Combos related
    public Animator anim;
    public int numberOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 0.9f;

    void Update()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            ClearAttackBools();
        }


        if (numberOfClicks <= 2)
        {
            //anim.speed = 2.0f;

            if (Input.GetMouseButtonUp(0))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                ClearAttackBools();
                string boolName = "Square" + (numberOfClicks).ToString();
                anim.SetBool(boolName, true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                lastClickedTime = Time.time;
                numberOfClicks++;

                ClearAttackBools();
                string boolName = "Triangle" + (numberOfClicks).ToString();
                anim.SetBool(boolName, true);
            }
        }
    }

    void ClearAttackBools()
    {
        anim.SetBool("Square1", false);
        anim.SetBool("Square2", false);
        anim.SetBool("Square3", false);
        anim.SetBool("Triangle1", false);
        anim.SetBool("Triangle2", false);
        anim.SetBool("Triangle3", false);
    }
}