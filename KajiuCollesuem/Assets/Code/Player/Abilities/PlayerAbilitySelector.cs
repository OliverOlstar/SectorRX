using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitySelector : MonoBehaviour
{
    [Header("Select Ability")]
    public abilities ability1;
    public abilities ability2;
    [SerializeField] private SOAbilities[] _AbilitySO = new SOAbilities[1];

    [Header("Ability Prefabs")]
    [SerializeField] private GameObject _plasmaBreathPrefab;

    [Header("Ability Setups")]
    [SerializeField] private Transform _mouthMuzzle;

    //List of all abilities to help with entering the desired abilities to be added
    public enum abilities
    {
        PlasmaBreath
    }

    public void SetupAbilities(PlayerStateController pStateController)
    {
        pStateController._AbilityScript1 = AddAbility(ability1, pStateController);
        pStateController._AbilityScript2 = AddAbility(ability2, pStateController);
        pStateController._AbilitySO1 = _AbilitySO[(int)ability1];
        pStateController._AbilitySO2 = _AbilitySO[(int)ability2];

        Destroy(this);
    }

    private IAbility AddAbility(abilities whichAbility, PlayerStateController pStateController)
    {
        IAbility newAbility = null;

        switch (whichAbility)
        {
            // PLASMA BREATH
            case abilities.PlasmaBreath:
                PlasmaBreath breath = gameObject.AddComponent<PlasmaBreath>();
                breath.Init(pStateController, _mouthMuzzle, _plasmaBreathPrefab);
                newAbility = breath;
                break;
        }

        return newAbility;
    }
}
