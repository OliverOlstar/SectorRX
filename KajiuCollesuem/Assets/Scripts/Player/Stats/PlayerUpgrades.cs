using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private PlayerAttributes _attributes;
    [SerializeField] private PlayerHitbox _hitbox;

    [Space]
    [SerializeField] private SOStats[] Stats;
    private int[] Levels;

    [SerializeField] private int luck = 0;

    private void Start()
    {
        Levels = new int[Stats.Length];
        for (int i = 0; i < Stats.Length; i++)
        {
            LevelUp(i);
        }
    }

    public void LevelUp(int pIndex)
    {
        switch(Stats[pIndex].dislayName[0])
        {
            //Health
            case 'H':
                _attributes.setMaxHealth(Stats[pIndex].values[Levels[pIndex]]);
                break;

            //Shield
            case 'S':
                _attributes.setMaxDefense(Stats[pIndex].values[Levels[pIndex]]);
                break;

            //Power
            case 'P':
                _attributes.setMaxPower(Stats[pIndex].values[Levels[pIndex]]);
                break;

            //Muscle
            case 'M':
                _hitbox.SetDamageMultiplier(Stats[pIndex].values[Levels[pIndex]] / 100);
                break;

            //Luck
            case 'L':
                luck = Stats[pIndex].values[Levels[pIndex]];
                break;
        }

        Levels[pIndex]++;
    }
}
