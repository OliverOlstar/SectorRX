using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private int level;
    private StatUpgrades powerUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        powerUpgrade = transform.parent.parent.parent.GetComponent<StatUpgrades>();
    }

    public void ClickStatButton()
    {
        powerUpgrade.ClickButtonStat(index, level);
    }

    public void ClickPowerButton()
    {
        powerUpgrade.ClickButtonPower(index, level);
    }

    public void HoverStatButton()
    {
        powerUpgrade.HoverStatButton(index, level, transform.position.y);
    }

    public void HoverPowerButton()
    {
        powerUpgrade.HoverPowerButton(index, level, transform.position.y);
    }

    public void HoverExit()
    {
        powerUpgrade.HoverExit();
    }
}
