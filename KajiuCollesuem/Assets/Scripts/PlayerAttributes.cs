using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    private readonly float maxHealth = 100;
    private float health;

    private readonly int maxShield = 100;
    private int shield;

    private readonly int maxPowerGuage = 10;
    private int powerGuage;

    public Slider healthSlider;
    public Slider shieldSlider;
    public Slider powerSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        powerGuage = 0;
    }

    //GET SET
    //get current variables
    public float getHealth()
    {
        return health;
    }

    public int getShield()
    {
        return shield;
    }

    public int getPowerGuage()
    {
        return powerGuage;
    }

    //METHODS
    //gain health
    public void gainHealth(float x)
    {
        //make sure can't gain more health than max
        if (health + x >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += x;
            Debug.Log("Health Gained " + x + ", New Health: " + health);
        }

        healthSlider.value = health;
    }

    //lose health
    public void takeDamage(float x)
    {
        if(health - x <= 0)
        {
            //call death function
            Debug.Log("Player Died");
        }
        else
        {
            health -= x;
            Debug.Log("Damage Taken: " + x + ", New Health: " + health);
        }

        healthSlider.value = health;
    }
        

    //gain shield
    public void gainShield(int x)
    {
        if(shield + x >= maxShield)
        {
            shield = maxShield;
        }
        else
        {
            shield += x;
            Debug.Log("Shield Gained: " + x + ", New Shield: " + health);
        }

        shieldSlider.value = shield;
    }

    //reduce shield
    public void loseShield(int x)
    {
        if(shield - x < 0)
        {
            shield = 0;
        }
        else
        {
            shield -= x;
            Debug.Log("Shield Lost: " + x + ", New Shield: " + health);
        }

        shieldSlider.value = shield;
    }

    public void gainPowerGuage(int x)
    {
        if(powerGuage + x > maxPowerGuage)
        {
            powerGuage = maxPowerGuage;
        }
        else
        {
            powerGuage += x;
            Debug.Log("Power Guage Gained: " + x + ", New Power Guage: " + health);
        }

        powerSlider.value = powerGuage;
    }

    public void losePowerGuage(int x, int j)
    {
        if(powerGuage - x < 0)
        {
            powerGuage = 0;
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                while (powerGuage > 0)
                {
                    StartCoroutine(powerLoss(x, j));
                }
                StartCoroutine(powerRestore(x, j));
            }
            Debug.Log("" + "Power Guage Lost: " + x + ", New Power Guage: " + health);
        }

        powerSlider.value = powerGuage;
    }

    IEnumerator powerLoss(int x, int j)
    {
        StopCoroutine(powerRestore(x, j));
        yield return new WaitForSeconds(1);
        powerGuage -= x;
        yield return new WaitForSeconds(1);
    }

    IEnumerator powerRestore(int x, int j)
    {
        StopCoroutine(powerLoss(x, j));
        yield return new WaitForSeconds(10);
        powerGuage += j;
    }
}

