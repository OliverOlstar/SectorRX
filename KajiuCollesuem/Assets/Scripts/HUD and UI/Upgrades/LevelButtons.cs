using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private int level;
    private PowerUpgrade powerUpgrade;

    // Start is called before the first frame update
    void Start()
    {
        powerUpgrade = transform.parent.parent.GetComponent<PowerUpgrade>();
    }

    public void HoverButton()
    {
        powerUpgrade.HoverButton(index, level, transform.position.y);
    }

    public void HoverExit()
    {
        powerUpgrade.HoverExit();
    }
}
