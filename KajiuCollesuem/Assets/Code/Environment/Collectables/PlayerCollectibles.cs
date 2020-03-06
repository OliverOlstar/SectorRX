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
    private ModelAnimations _modelAnimations;
    [SerializeField] SliderController sliderController;

    // VARS
    [SerializeField] private GameObject[] statTexts = new GameObject[7];
    [SerializeField] private Slider[] statSliders = new Slider[7];
    [SerializeField] private float MAXUPGRADES = 10;
    //private int statUpMeters = 1;
    private float[] upgradeCounts = new float[7];

    [Header("Health")]
    [SerializeField] private int minHealth = 60;
    [SerializeField] private int maxHealth = 120;

    [Header("Shield")]
    [SerializeField] private int minShield = 20;
    [SerializeField] private int maxShield = 60;

    //[Header("Power")]

    [Header("Speed")]
    [SerializeField] [Range(1, 1)] private float minWalkSpeed = 1.0f;
    [SerializeField] private float maxWalkSpeed = 2.0f;
    [SerializeField] [Range(1, 1)] private float minDodgeSpeed = 1.0f;
    [SerializeField] private float maxDodgeSpeed = 2.0f;
    [SerializeField] private float minAnimSpeed = 2.0f;
    [SerializeField] private float maxAnimSpeed = 3.0f;

    [Header("Jump")]
    [SerializeField] [Range(1,1)] private float minJump = 1.0f;
    [SerializeField] private float maxJump = 2.0f;

    [Header("Weight")]
    [SerializeField] private float offsetWeight = 0.65f;
    [SerializeField] private float minWeight = 1.0f;
    [SerializeField] private float maxWeight = 3.0f;

    [Header("Attack")]
    [SerializeField] [Range(1, 1)] private float minAttack = 1.0f;
    [SerializeField] private float maxAttack = 2.0f;

    // SETUP
    private void Start()
    {
        _stateController = GetComponent<PlayerStateController>();
        _modelAnimations = GetComponentInChildren<ModelAnimations>();

        //Text for when stats are raised
        statTexts[0].SetActive(false);
        statTexts[1].SetActive(false);
        statTexts[2].SetActive(false);
        statTexts[3].SetActive(false);
        statTexts[4].SetActive(false);
        statTexts[5].SetActive(false);
        statTexts[6].SetActive(false);

        //Set stat slider values to 0
        statSliders[0].value = 0;
        statSliders[1].value = 0;
        statSliders[2].value = 0;
        statSliders[3].value = 0;
        statSliders[4].value = 0;
        statSliders[5].value = 0;
        statSliders[6].value = 0;

        // Health
        _stateController._playerAttributes.setMaxHealth(minHealth);

        // Shield
        _stateController._playerAttributes.setMaxDefense(minShield);

        // Speed
        _modelAnimations.stepMult = minAnimSpeed;

        // Weight
        _stateController._playerAttributes.weight = offsetWeight + minWeight;

    }

    [ExecuteInEditMode]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CollectedItem(Upgrades.Health);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            CollectedItem(Upgrades.Shield);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            CollectedItem(Upgrades.Power);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            CollectedItem(Upgrades.Speed);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            CollectedItem(Upgrades.Jump);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            CollectedItem(Upgrades.Weight);

        if (Input.GetKeyDown(KeyCode.Alpha7))
            CollectedItem(Upgrades.Attack);
    }

    // MIDGAME UPGRADE (return true if collected, false if already at max collect)
    public bool CollectedItem(Upgrades pStat)
    {
        int index = (int)pStat;
        upgradeCounts[index]++;
        
        // Maxed out Upgrade
        if (upgradeCounts[index] > MAXUPGRADES)
            return false;

        float value;

        switch (pStat)
        {
            case Upgrades.Health:
                statTexts[0].SetActive(true);
                sliderController.UpdateBars(3, upgradeCounts[index]);
                value = Mathf.Lerp(minHealth, maxHealth, upgradeCounts[index] / MAXUPGRADES);
                _stateController._playerAttributes.setMaxHealth(Mathf.RoundToInt(value));
                break;

            case Upgrades.Shield:
                statTexts[1].SetActive(true);
                sliderController.UpdateBars(4, upgradeCounts[index]);
                value = Mathf.Lerp(minShield, maxShield, upgradeCounts[index] / MAXUPGRADES);
                _stateController._playerAttributes.setMaxDefense(Mathf.RoundToInt(value));
                break;

            case Upgrades.Power:
                statTexts[2].SetActive(true);
                sliderController.UpdateBars(5, upgradeCounts[index]);
                break;

            case Upgrades.Speed:
                statTexts[4].SetActive(true);
                sliderController.UpdateBars(7, upgradeCounts[index]);
                value = Mathf.Lerp(minWalkSpeed, maxWalkSpeed, upgradeCounts[index] / MAXUPGRADES);
                _stateController._movementComponent.speedMult = value;

                value = Mathf.Lerp(minDodgeSpeed, maxDodgeSpeed, upgradeCounts[index] / MAXUPGRADES);
                _stateController._dodgeComponent.speedMult = value;

                value = Mathf.Lerp(minAnimSpeed, maxAnimSpeed, upgradeCounts[index] / MAXUPGRADES);
                _modelAnimations.stepMult = value;
                break;

            case Upgrades.Jump:
                statTexts[5].SetActive(true);
                sliderController.UpdateBars(8, upgradeCounts[index]);
                value = Mathf.Lerp(minJump, maxJump, upgradeCounts[index] / MAXUPGRADES);
                _stateController._movementComponent.jumpMult = value;
                break;

            case Upgrades.Weight:
                statTexts[6].SetActive(true);
                sliderController.UpdateBars(9, upgradeCounts[index]);
                value = Mathf.Lerp(minWeight, maxWeight, upgradeCounts[index] / MAXUPGRADES);
                _stateController._playerAttributes.weight = offsetWeight + Mathf.Pow(value, 2);
                break;

            case Upgrades.Attack:
                statTexts[3].SetActive(true);
                sliderController.UpdateBars(6, upgradeCounts[index]);

                value = Mathf.Lerp(minAttack, maxAttack, upgradeCounts[index] / MAXUPGRADES);
                foreach (PlayerHitbox hitbox in _stateController.hitboxes)
                {
                    hitbox.attackMult = value;
                }
                break;
        }

        // Sound
        _stateController._Sound.StatUpSound(pStat, 0.0f);

        return true;
    }
}
