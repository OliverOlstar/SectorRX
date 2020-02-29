using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer(s): Scott Watman, Oliver Loescher
 Description: Edits player's stats depending on which collectible they pick up*/

public class PlayerCollectibles : MonoBehaviour
{ 
    // ENUM
    public enum Upgrades
    {
        Health,
        Shield,
        Power,
        Speed,
        Jump,
        Weight,
        Attack
    }

    // COMPONENTS
    private PlayerStateController _stateController;

    // VARS
    [SerializeField] private GameObject[] statTexts = new GameObject[7];
    [SerializeField] private int MAXUPGRADES = 10;
    private int[] upgradeCounts = new int[7];
    private float[] upgradeMults = new float[7];

    [Header("Health")]
    [SerializeField] private int minHealth = 60;
    [SerializeField] private int maxHealth = 120;

    [Header("Shield")]
    [SerializeField] private int minShield = 20;
    [SerializeField] private int maxShield = 60;

    //[Header("Power")]

    [Header("Speed")]
    [SerializeField] [Range(1, 1)] private float minSpeed = 1.0f;
    [SerializeField] private float maxSpeed = 2.0f;

    [Header("Jump")]
    [SerializeField] [Range(1,1)] private float minJump = 1.0f;
    [SerializeField] private float maxJump = 2.0f;

    [Header("Weight")]
    [SerializeField] private float minWeight = 1.0f;
    [SerializeField] private float maxWeight = 3.0f;

    [Header("Attack")]
    [SerializeField] [Range(1, 1)] private float minAttack = 1.0f;
    [SerializeField] private float maxAttack = 2.0f;

    // SETUP
    private void Start()
    {
        int index = 0;
        _stateController = GetComponent<PlayerStateController>();
        
        //Text for when stats are raised
        statTexts[0].SetActive(false);
        statTexts[1].SetActive(false);
        statTexts[2].SetActive(false);
        statTexts[3].SetActive(false);
        statTexts[4].SetActive(false);
        statTexts[5].SetActive(false);
        statTexts[6].SetActive(false);

        // Health
        _stateController._playerAttributes.setMaxHealth(minHealth);
        upgradeMults[index] = (maxHealth - minHealth) / MAXUPGRADES;
        index++;

        // Shield
        _stateController._playerAttributes.setMaxDefense(minShield);
        upgradeMults[index] = (maxShield - minShield) / MAXUPGRADES;
        index++;

        // Power
        index++;

        // Speed
        upgradeMults[index] = (maxSpeed - minSpeed) / MAXUPGRADES;
        index++;

        // Jump
        upgradeMults[index] = (maxJump - minJump) / MAXUPGRADES;
        index++;

        // Weight
        _stateController._playerAttributes.weight = minWeight;
        upgradeMults[index] = (maxWeight - minWeight) / MAXUPGRADES;
        index++;

        // Attack
        upgradeMults[index] = (maxAttack - minAttack) / MAXUPGRADES;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //        CollectedItem(Upgrades.Health);

    //    if (Input.GetKeyDown(KeyCode.Alpha2))
    //        CollectedItem(Upgrades.Shield);

    //    if (Input.GetKeyDown(KeyCode.Alpha3))
    //        CollectedItem(Upgrades.Power);

    //    if (Input.GetKeyDown(KeyCode.Alpha4))
    //        CollectedItem(Upgrades.Speed);

    //    if (Input.GetKeyDown(KeyCode.Alpha5))
    //        CollectedItem(Upgrades.Jump);

    //    if (Input.GetKeyDown(KeyCode.Alpha6))
    //        CollectedItem(Upgrades.Weight);

    //    if (Input.GetKeyDown(KeyCode.Alpha7))
    //        CollectedItem(Upgrades.Attack);
    //}

    // MIDGAME UPGRADE (return true if collected, false if already at max collect)
    public bool CollectedItem(Upgrades pStat)
    {
        int index = (int)pStat;
        upgradeCounts[index]++;
        
        // Maxed out Upgrade
        if (upgradeCounts[index] > MAXUPGRADES)
            return false;

        switch (pStat)
        {
            case Upgrades.Health:
                statTexts[0].SetActive(true);
                _stateController._playerAttributes.setMaxHealth(Mathf.FloorToInt(minHealth + (upgradeMults[index] * upgradeCounts[index])));
                break;

            case Upgrades.Shield:
                statTexts[1].SetActive(true);
                _stateController._playerAttributes.setMaxDefense(Mathf.FloorToInt(minShield + (upgradeMults[index] * upgradeCounts[index])));
                break;

            case Upgrades.Power:
                statTexts[2].SetActive(true);
                break;

            case Upgrades.Speed:
                statTexts[4].SetActive(true);
                _stateController._movementComponent.speedMult = minSpeed + (upgradeMults[index] * upgradeCounts[index]);
                _stateController._dodgeComponent.speedMult = minSpeed + (upgradeMults[index] * upgradeCounts[index]);
                break;

            case Upgrades.Jump:
                statTexts[5].SetActive(true);
                _stateController._movementComponent.jumpMult = minJump + (upgradeMults[index] * upgradeCounts[index]);
                break;

            case Upgrades.Weight:
                statTexts[6].SetActive(true);
                _stateController._playerAttributes.weight = minWeight + (upgradeMults[index] * upgradeCounts[index]);
                break;

            case Upgrades.Attack:
                statTexts[3].SetActive(true);
                foreach (PlayerHitbox hitbox in _stateController.hitboxes)
                {
                    hitbox.attackMult = minAttack + (upgradeMults[index] * upgradeCounts[index]);
                }
                break;
        }

        // Sound
        _stateController._Sound.StatUpSound(pStat, 0.0f);

        return true;
    }
}
