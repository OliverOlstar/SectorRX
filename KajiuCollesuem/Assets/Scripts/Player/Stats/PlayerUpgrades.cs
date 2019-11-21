using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerAttributes _attributes;
    [SerializeField] private PlayerHitbox _hitbox;

    [Space]
    [SerializeField] private SOStats[] Stats;
    private int[] _statLevels;

    [Space]
    [SerializeField] private SOStats[] Powers;
    private int[] _powerLevels;

    [SerializeField] private int luck = 0;

    private void Start()
    {
        _statLevels = new int[Stats.Length];
        for (int i = 0; i < Stats.Length; i++)
        {
            LevelUp(i);
        }

        _powerLevels = new int[Powers.Length];
        for (int i = 0; i < Powers.Length; i++)
        {
            //LevelUp(i);
        }
    }

    public void LevelUp(int pIndex)
    {
        switch(Stats[pIndex].dislayName[0])
        {
            //Health
            case 'H':
                _attributes.setMaxHealth(Stats[pIndex].values[_statLevels[pIndex]]);
                break;

            //Shield
            case 'S':
                _attributes.setMaxDefense(Stats[pIndex].values[_statLevels[pIndex]]);
                break;

            //Power
            case 'P':
                _attributes.setMaxPower(Stats[pIndex].values[_statLevels[pIndex]]);
                break;

            //Muscle
            case 'M':
                _hitbox.SetDamageMultiplier(Stats[pIndex].values[_statLevels[pIndex]] / 100);
                break;

            //Luck
            case 'L':
                luck = Stats[pIndex].values[_statLevels[pIndex]];
                break;
        }

        _statLevels[pIndex]++;
    }

    public void PowerUpgrade(int pIndex)
    {
        switch (Stats[pIndex].dislayName[0])
        {
            //Magma
            case 'M':
                //_attributes.setMaxHealth(Powers[pIndex].values[_statLevels[pIndex]]);
                break;
        }

        _powerLevels[pIndex]++;
    }
}
