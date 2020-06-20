using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioLowPassFilter audioLowPassFilter;
    public bool acikMi;
    // public bool isMuted;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // isMuted = PlayerPrefs.GetInt("MuteSound") == 1;
        acikMi = false;
    }

    private void Update()
    {
        if (acikMi)
        {
            audioLowPassFilter.enabled = true;
        }
        else if (!acikMi)
        {
            audioLowPassFilter.enabled = false;
        }
    }

    public void KilltheMusic()
    {
        Destroy(this.gameObject);
    }

    //public void MuteSound()
    //{
    //    isMuted = !isMuted;
    //    if (isMuted == true)
    //    {
    //        isMuted = PlayerPrefs.GetInt("MuteSound") == 1;
    //        audioSource.mute = isMuted;
    //        PlayerPrefs.SetInt("MuteSound", isMuted ? 1 : 0);
    //    }
    //    if (isMuted == false)
    //    {
    //        isMuted = PlayerPrefs.GetInt("MuteSound") == 0;
    //        audioSource.mute = !isMuted;
    //        PlayerPrefs.SetInt("MuteSound", isMuted ? 1 : 0);
    //    }
    //}
}
