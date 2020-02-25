using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public GameObject mainMenu;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        videoPlayer.Play();
        videoPlayer.SetDirectAudioVolume(1, 0.5f);
        audioSource.PlayDelayed(7.0f);
    }

    private void Update()
    {
        StartCoroutine("IntroTransition");
    }

    IEnumerator IntroTransition()
    {
        yield return new WaitForSeconds(6.5f);
        mainMenu.SetActive(true);
        if(videoPlayer.targetCameraAlpha > 0)
        {
            videoPlayer.targetCameraAlpha -= fadeSpeed * Time.deltaTime;
        }
    }
}