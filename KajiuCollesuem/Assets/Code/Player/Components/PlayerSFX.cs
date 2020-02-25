using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer(s): Scott Watman
 Description: Handles the play back and timing of sound effects depending on variety of scenarios.*/

public class PlayerSFX : MonoBehaviour
{
    public AudioClip[] surfaces = new AudioClip[2]; //Walking sound effects.
    public AudioClip[] movement = new AudioClip[3]; //Sound effects for dodging, jumping, and landing.
    public AudioClip[] playerHit = new AudioClip[3]; //Sound effects for when the player takes damage from something or dies.
    public AudioClip[] lightAttack = new AudioClip[2]; //Sound effects for the light attack.
    public AudioClip[] heavyAttack = new AudioClip[2]; //Sound effects for the heavy attack.
    public AudioClip[] collectStat = new AudioClip[7]; //One different sound effect for each of the seven collectibles.
    public AudioClip[] abilitySounds = new AudioClip[4]; //Sound effects for the various player abilities.
    public AudioSource sfxSource; //Audio Source which handles single playback of sounds.
    public AudioSource walkingSource; //Audio Source which will be looping the walking sounds. Adjust playback speed depending on movement speed.

    [SerializeField] private PlayerCollectibles.Upgrades statName;

    #region PlayerReactions
    //Plays sound when player takes damage.
    public void HitByAttackSound()
    {
        sfxSource.clip = playerHit[0];
        sfxSource.Play();
    }

    //Plays sound when player touches the tar.
    public void HitTarSound()
    {
        sfxSource.clip = playerHit[1];
        sfxSource.Play();
    }

    //Plays sound when the player dies.
    public void PlayerDeathSound()
    {
        sfxSource.clip = playerHit[2];
        sfxSource.Play();
    }

    //Plays sound when the player has collected a stat increasing item
    public void StatUpSound()
    {
        sfxSource.clip = collectStat[(int)statName];
        sfxSource.Play();
    }
    #endregion

    #region Locomotion
    //Plays sound when the player performs a dodge
    public void DodgeSound()
    {
        sfxSource.clip = movement[0];
        sfxSource.Play();
    }

    //Plays sound the player lands after a jump or falling
    public void LandingSound()
    {
        sfxSource.clip = movement[2];
        sfxSource.Play();
    }

    //Plays sound the player is walking on a sand surface
    public void WalkingSand()
    {
        walkingSource.clip = surfaces[0];
        walkingSource.Play();
    }

    //Plays sound the player is walking on a metal surface
    public void WalkingMetal()
    {
        walkingSource.clip = surfaces[1];
        walkingSource.Play();
    }
    #endregion

    #region PlayerActions
    //Plays sound when the player jumps.
    public void JumpSound()
    {
        sfxSource.clip = movement[1];
        sfxSource.Play();
    }

    //Randomly plays one of two sounds when the player performs a light attack.
    public void LightAttackSound()
    {
        sfxSource.clip = lightAttack[Random.Range(0, 1)];
        sfxSource.Play();
    }

    //Randomly plays one of two sounds when the player performs a heavy attack.
    public void HeavyAttackSound()
    {
        sfxSource.clip = lightAttack[Random.Range(0, 1)];
        sfxSource.pitch = 0.5f;
        sfxSource.Play();
    }
    #endregion

    #region Abilities
    //Plays sound when playeer uses Plasma Breath ability.
    public void PlasmaBreathSound()
    {
        sfxSource.clip = abilitySounds[0];
        sfxSource.Play();
    }

    //Plays sound when playeer uses Roll Attack ability.
    public void RollAttackSound()
    {
        sfxSource.clip = abilitySounds[1];
        sfxSource.Play();
    }

    //Plays sound when playeer uses Plasma Ball ability.
    public void PlasmaBallSound()
    {
        sfxSource.clip = abilitySounds[2];
        sfxSource.Play();
    }

    //Plays sound when playeer uses Ground Pound ability.
    public void GroundPoundSound()
    {
        sfxSource.clip = abilitySounds[3];
        sfxSource.Play();
    }
    #endregion
}