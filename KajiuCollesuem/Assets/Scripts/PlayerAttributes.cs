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

    const int BAR_HEIGHT = 20;

    public Slider healthSlider;
    public Slider shieldSlider;
    public Slider powerSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        powerGuage = 0;

        modifyMaxHealth(maxHealth);
        modifyMaxDefense(maxShield);
        modifyMaxPower(maxPowerGuage);
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

        powerSlider.value = powerGuage;
    }

    public void modifyMaxHealth(float maxHealth)
    {
        //health.sizeDelta.Set(healthVal, BAR_HEIGHT);
        RectTransform healthRect = healthSlider.gameObject.GetComponent<RectTransform>();
        healthRect.sizeDelta = new Vector2(maxHealth, BAR_HEIGHT);
    }

    public void modifyMaxDefense(int maxShield)
    {
        //defense.sizeDelta.Set(defenseVal, BAR_HEIGHT);
        RectTransform shieldRect = shieldSlider.gameObject.GetComponent<RectTransform>();
        shieldRect.sizeDelta = new Vector2(maxShield, BAR_HEIGHT);
    }

    public void modifyMaxPower(int maxPowerGuage)
    {
        //power.sizeDelta.Set(powerVal, BAR_HEIGHT);
        RectTransform powerRect = powerSlider.gameObject.GetComponent<RectTransform>();
        powerRect.sizeDelta = new Vector2(maxPowerGuage, BAR_HEIGHT);
    }
}
