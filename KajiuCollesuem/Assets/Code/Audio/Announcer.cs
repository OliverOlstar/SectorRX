using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer(s): Scott Watman
 Description: Handles which Announcer clip plays depending on how a player is knocked out.*/

public class Announcer : MonoBehaviour
{
    private static Announcer _Instance;
    public AudioClip[] knockOuts = new AudioClip[6];
    public AudioClip[] KOstreak = new AudioClip[2];
    public AudioClip incinerated;
    public AudioClip eviscerated;
    public AudioSource announceSource;
    public Text KOText; //Text that appears when a player is killed. Text is set to whatever word the Announcer says.

    void Awake()
    {
        _Instance = this;
    }

    //Plays Announcer Audio Clip(s) for when a player is killed by another player's light or heavy attack.
    public void NormalKO()
    {
        PlayAnnounce(1.0f, 1.0f, knockOuts[Random.Range(0, 6)], announceSource);
    }

    //Plays Announcer Audio Clip for when a player is killed by a plasma ball, breath, or the tar.
    public void IncinKO()
    {
        PlayAnnounce(1.0f, 1.0f, incinerated, announceSource);
    }

    //Plays Announcer Audio Clip for when a player is killed by a blade beam, or a wolf enemy.
    public void EvisKO()
    {
        PlayAnnounce(1.0f, 1.0f, eviscerated, announceSource);
    }

    //Plays Announcer Audio Clip(s) for when a single player has killed two or more players.
    public void KillStreak()
    {
        PlayAnnounce(1.0f, 1.0f, KOstreak[0], announceSource);
    }

    //Function which handles setting the Audio Source's personal variables
    private void PlayAnnounce(float pVolume, float pPitch, AudioClip pClip, AudioSource pSource)
    {
        announceSource = pSource;
        announceSource.clip = pClip;
        announceSource.volume = pVolume;
        announceSource.pitch = pPitch;
        announceSource.Play();
    }
}
