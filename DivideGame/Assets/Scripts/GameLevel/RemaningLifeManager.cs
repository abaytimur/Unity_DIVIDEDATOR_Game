using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemaningLifeManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip damageSound;

    [SerializeField]
    private GameObject lifeRemaning1, lifeRemaning2, lifeRemaning3;

    public void CheckRemainingLives(int remaninLife)
    {
        switch (remaninLife)
        {
            case 3:
                lifeRemaning1.SetActive(true);
                lifeRemaning2.SetActive(true);
                lifeRemaning3.SetActive(true);
                break;

            case 2:
                lifeRemaning1.SetActive(true);
                lifeRemaning2.SetActive(true);
                lifeRemaning3.SetActive(false);
                audioSource.PlayOneShot(damageSound, 0.5f);
                break;

            case 1:
                lifeRemaning1.SetActive(true);
                lifeRemaning2.SetActive(false);
                lifeRemaning3.SetActive(false);
                audioSource.PlayOneShot(damageSound, 0.7f);
                break;

            case 0:
                lifeRemaning1.SetActive(false);
                lifeRemaning2.SetActive(false);
                lifeRemaning3.SetActive(false);
                audioSource.PlayOneShot(damageSound, 1f);
                break;
        }
    }
}
