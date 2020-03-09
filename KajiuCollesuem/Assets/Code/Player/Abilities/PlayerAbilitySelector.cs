using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitySelector : MonoBehaviour
{
    [Header("Select Ability")]
    [SerializeField] private SOAbilities[] _AbilitySO = new SOAbilities[1];

    [Header("Ability Prefabs")]
    [SerializeField] private GameObject _plasmaBreathPrefab;
    [SerializeField] private GameObject _plasmaBallPrefab;
    [SerializeField] private GameObject _plasmaStrikePrefab;

    [Header("Ability Setups")]
    [SerializeField] private Transform _mouthMuzzle;

    //List of all abilities to help with entering the desired abilities to be added
    public enum abilities
    {
        PlasmaBreath,
        PlasmaBall,
        PlasmaStrike
    }

    public void SetupAbilities(int pAbility, PlayerStateController pStateController)
    {
        pStateController._modelController.abilitySO = _AbilitySO[pAbility];
        pStateController._AbilityScript = AddAbility((abilities)pAbility, pStateController);

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

            // PLASMA BALL
            case abilities.PlasmaBall:
                PlasmaBall ball = gameObject.AddComponent<PlasmaBall>();
                ball.Init(pStateController, _mouthMuzzle, _plasmaBallPrefab);
                newAbility = ball;
                break;

            // PLASMA STRIKE
            case abilities.PlasmaStrike:
                PlasmaStrike strike = gameObject.AddComponent<PlasmaStrike>();
                strike.Init(pStateController, _mouthMuzzle, _plasmaStrikePrefab);
                newAbility = strike;
                break;
        }

        return newAbility;
    }
}
