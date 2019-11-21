using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerAttributes _attributes;
    [SerializeField] private PlayerHitbox _hitbox;

    [Space]
    public SOStats[] Stats;
    private int[] _statLevels;

    [Space]
    public SOPowers[] Powers;
    private int[] _powerLevels;

    [SerializeField] private int luck = 0;

    private void Start()
    {
        _statLevels = new int[Stats.Length];
        for (int i = 0; i < Stats.Length; i++)
        {
            LevelUp(i, 0);
        }

        _powerLevels = new int[Powers.Length];
        for (int i = 0; i < Powers.Length; i++)
        {
            //LevelUp(i);
        }
    }

    public bool LevelUp(int pIndex, int pLevel)
    {
        //Return false if trying to upgrade that is not possible
        if (_statLevels[pIndex] != pLevel)
            return false;

        //Make Upgrade Happen
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
        return true;
    }

    public bool PowerUpgrade(int pIndex, int pLevel)
    {
        //Return false if trying to upgrade that is not possible
        if (_statLevels[pIndex] != pLevel)
            return false;

        switch (Powers[pIndex].dislayName[0])
        {
            //Magma
            case 'M':
                //_attributes.setMaxHealth(Powers[pIndex].values[_statLevels[pIndex]]);
                break;
        }

        _powerLevels[pIndex]++;
        return true;
    }
}
