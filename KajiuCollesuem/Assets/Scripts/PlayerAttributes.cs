using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    private readonly int maxHealth = 100;
    private int health;

    private readonly int maxShield = 100;
    private int shield;

    private readonly int maxPowerGuage = 10;
    private int powerGuage;

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        powerGuage = 0;
    }

    //GET SET
    //get current variables
    public int getHealth()
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
    public void gainHealth(int x)
    {
        //make sure can't gain more health than max
        if (health + x >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += x;
            healthSlider.value = health;
            Debug.Log("Health Gained " + x + ", New Health: " + health);
        }
    }

    //lose health
    public void takeDamage(int x)
    {
        if(health - x <= 0)
        {
            //call death function
            Debug.Log("Player Died");
        }
        else
        {
            health -= x;
            healthSlider.value = health;
            Debug.Log("Damage Taken: " + x + ", New Health: " + health);
        }       
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
    }

    public void losePowerGuage(int x)
    {
        if(powerGuage - x < 0)
        {
            powerGuage = 0;
        }
        else
        {
            powerGuage -= x;
            Debug.Log("Powe Guage Lost: " + x + ", New Power Guage: " + health);
        }
    }
}
