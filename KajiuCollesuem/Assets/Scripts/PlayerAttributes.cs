﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    private int maxHealth = 100;
    private float health;

    private int maxShield = 100;
    private int shield;

    private int maxPowerGuage = 20;
    private int powerGuage;

    const int BAR_HEIGHT = 20;

    public float barLengthMultiplier = 1.5f;

    public Slider healthSlider;
    public Slider shieldSlider;
    public Slider powerSlider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        powerGuage = 0;

        RectTransform powerRect = powerSlider.gameObject.GetComponent<RectTransform>();
        //X: 1039.6, Y: 58.9, Z: 0.0
        Debug.Log(powerRect.transform.position);
        powerRect.position = new Vector3(1040, 59.4f, 0);
        Debug.Log(powerRect.position);

        setBarsLength();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            takeDamage(10);
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

    //lose Shield
    public void takeDamage(int x)
    {
        if(shield > 0)
        {
            if((shield - x) >= 0)
            {
                shield = shield - x;
            }
            else
            {
                int shieldOverflow = shield - x;
                shield = 0;
                health -= shieldOverflow;
                
            }
            //Start to take away player health
            Debug.Log("Player Health");
        }
        else
        {
            health -= x;
            Debug.Log("Damage Taken: " + x + ", New Health: " + health);
        }
        shieldSlider.value = shield;
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
    //shield over time
    private int curShipShield = 100;

    void update()
    {
        StartCoroutine(addShield());
    }

    IEnumerator addShield()
    {
        while (true)
        { // loops forever
            if (curShipShield < 100)
            { // if Shield < 100
                curShipShield += 1; // increase shield and wait the specified time
                yield return new WaitForSeconds(1);
            }
            else
            { // if Shield >= 100, just yield 
                yield return null;
            }
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

    public void modifyMaxHealth(int pMaxHealth)
    {
        maxHealth += pMaxHealth;

        //health.sizeDelta.Set(healthVal, BAR_HEIGHT);
        RectTransform healthRect = healthSlider.gameObject.GetComponent<RectTransform>();
        healthRect.sizeDelta = new Vector2(maxHealth * barLengthMultiplier, BAR_HEIGHT);
    }

    public void modifyMaxDefense(int pMaxShield)
    {
        maxShield += pMaxShield;

        //defense.sizeDelta.Set(defenseVal, BAR_HEIGHT);
        RectTransform shieldRect = shieldSlider.gameObject.GetComponent<RectTransform>();
        shieldRect.sizeDelta = new Vector2(maxShield * barLengthMultiplier, BAR_HEIGHT);
    }

    public void modifyMaxPower(int pMaxPowerGuage)
    {
        maxPowerGuage += pMaxPowerGuage;

        //power.sizeDelta.Set(powerVal, BAR_HEIGHT);
        RectTransform powerRect = powerSlider.gameObject.GetComponent<RectTransform>();
        powerRect.sizeDelta = new Vector2(maxPowerGuage * barLengthMultiplier, BAR_HEIGHT);
    }

    private void setBarsLength()
    {
        //Used to set the length of the bars (most for at start)
        RectTransform powerRect = powerSlider.gameObject.GetComponent<RectTransform>();
        powerRect.sizeDelta = new Vector2(maxPowerGuage * barLengthMultiplier, BAR_HEIGHT);

        RectTransform shieldRect = shieldSlider.gameObject.GetComponent<RectTransform>();
        shieldRect.sizeDelta = new Vector2(maxShield * barLengthMultiplier, BAR_HEIGHT);

        RectTransform healthRect = healthSlider.gameObject.GetComponent<RectTransform>();
        healthRect.sizeDelta = new Vector2(maxHealth * barLengthMultiplier, BAR_HEIGHT);
    }
}
