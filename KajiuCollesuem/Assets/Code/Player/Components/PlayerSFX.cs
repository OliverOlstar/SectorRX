using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer(s): Scott Watman
 Description: Handles the play back and timing of sound effects depending on variety of scenarios.*/

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] surfaces = new AudioClip[2]; //Walking sound effects.
    [SerializeField] private AudioClip[] movement = new AudioClip[3]; //Sound effects for dodging, jumping, and landing.
    [SerializeField] private AudioClip[] playerHit = new AudioClip[3]; //Sound effects for when the player takes damage from something or dies.
    [SerializeField] private AudioClip[] lightAttack = new AudioClip[2]; //Sound effects for the light attack.
    [SerializeField] private AudioClip[] heavyAttack = new AudioClip[2]; //Sound effects for the heavy attack.
    [SerializeField] private AudioClip[] collectStat = new AudioClip[7]; //One different sound effect for each of the seven collectibles.
    [SerializeField] private AudioClip[] abilitySounds = new AudioClip[4]; //Sound effects for the various player abilities.
    [SerializeField] private AudioSource sfxSource; //Audio Source which handles single playback of sounds.
    [SerializeField] private AudioSource walkingSource; //Audio Source which will be looping the walking sounds. Adjust playback speed depending on movement speed.

    //[SerializeField] private PlayerCollectibles.Upgrades _statName;

    #region PlayerReactions
    //Plays sound when player takes damage.
    public void HitByAttackSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, playerHit[0], sfxSource);
    }

    //Plays sound when player touches the tar.
    public void HitTarSound(float pDelay)
    {
        PlaySound(pDelay, 0.5f, 1.0f, playerHit[1], sfxSource);
    }

    //Plays sound when the player dies.
    public void PlayerDeathSound(float pDelay)
    {
        PlaySound(pDelay, 0.5f, 1.0f, playerHit[2], sfxSource);
    }

    //Plays sound when the player has collected a stat increasing item
    public void StatUpSound(PlayerCollectibles.Upgrades pStat, float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, collectStat[(int)pStat], sfxSource);
    }
    #endregion

    #region Locomotion
    //Plays sound when the player performs a dodge
    public void DodgeSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, movement[0], sfxSource);
    }

    //Plays sound the player lands after a jump or falling
    public void LandingSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, movement[2], sfxSource);
    }

    //Plays sound the player is walking on a sand or metal surface
    public void Walking(int pGroundMaterial, float pSpeed, float pDelay)
    {
        if (pSpeed > 0.6f)
        {
            PlaySound(pDelay, 0.4f, 1.0f, surfaces[pGroundMaterial], walkingSource);
        }

        PlaySound(pDelay, 0.4f, 1.0f, surfaces[1], walkingSource);
    }
    #endregion

    #region PlayerActions
    //Plays sound when the player jumps.
    public void JumpSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, movement[1], sfxSource);
    }

    //Randomly plays one of two sounds when the player performs a light attack.
    public void LightAttackSound(float pDelay)
    {
        PlaySound(pDelay, 0.3f, 1.0f, lightAttack[Random.Range(0, 1)], sfxSource);
    }

    //Randomly plays one of two sounds when the player performs a heavy attack.
    public void HeavyAttackSound(float pDelay)
    {
        PlaySound(pDelay, 0.3f, 0.5f, lightAttack[Random.Range(0, 1)], sfxSource);
    }
    #endregion

    #region Abilities
    //Plays sound when playeer uses Plasma Breath ability.
    public void PlasmaBreathSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, abilitySounds[0], sfxSource);
    }

    //Plays sound when playeer uses Roll Attack ability.
    public void RollAttackSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, abilitySounds[1], sfxSource);
    }

    //Plays sound when playeer uses Plasma Ball ability.
    public void PlasmaBallSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, abilitySounds[2], sfxSource);
    }

    //Plays sound when playeer uses Ground Pound ability.
    public void GroundPoundSound(float pDelay)
    {
        PlaySound(pDelay, 0.4f, 1.0f, abilitySounds[3], sfxSource);
    }
    #endregion

    private void PlaySound(float pDelay, float pVolume, float pPitch, AudioClip pClip, AudioSource pSource)
    {
        if (pDelay > 0)
        {
            StartCoroutine(SoundDelay(pDelay, pPitch, pClip));
        }
        else
        {
            sfxSource = pSource;
            sfxSource.clip = pClip;
            sfxSource.volume = pVolume;
            sfxSource.pitch = pPitch;
            sfxSource.Play();
        }
    }

    private IEnumerator SoundDelay(float pDelay, float pPitch, AudioClip pClip)
    {
        yield return new WaitForSeconds(pDelay);
        sfxSource.clip = pClip;
        sfxSource.pitch = pPitch;
        sfxSource.Play();
    }
}