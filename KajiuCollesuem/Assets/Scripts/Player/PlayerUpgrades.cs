using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private PlayerAttributes _attributes;
    private PlayerHitbox _hitbox;

    [Space]
    [SerializeField] private int[] healthLevels = new int[3];
    public int healthLevel = 0;

    [Space]
    [SerializeField] private int[] shieldLevels = new int[3];
    public int shieldLevel = 0;

    [Space]
    [SerializeField] private int[] powerLevels = new int[3];
    public int powerLevel = 0;

    [Space]
    [SerializeField] private float[] muscleLevels = new float[3];
    public int muscleLevel = 0;

    [Space]
    [SerializeField] private int[] luckLevels = new int[3];
    public int luckLevel = 0;
    public int luck = 0;

    private void Start()
    {
        _attributes = player.GetComponent<PlayerAttributes>();
        _hitbox = player.GetComponentInChildren<PlayerHitbox>();

        LevelUpHealth();
        LevelUpShield();
        LevelUpPower();
        LevelUpMuscle();
        LevelUpLuck();
    }

    public void LevelUpHealth()
    {
        _attributes.setMaxHealth(healthLevels[healthLevel]);
        healthLevel++;
    }

    public void LevelUpShield()
    {
        _attributes.setMaxDefense(powerLevels[powerLevel]);
        powerLevel++;
    }

    public void LevelUpPower()
    {
        _attributes.setMaxPower(shieldLevels[shieldLevel]);
        shieldLevel++;
    }

    public void LevelUpMuscle()
    {
        _hitbox.SetDamageMultiplier(muscleLevels[muscleLevel]);
        muscleLevel++;
    }

    public void LevelUpLuck()
    {
        luck = luckLevels[luckLevel];
        luckLevel++;
    }
}
