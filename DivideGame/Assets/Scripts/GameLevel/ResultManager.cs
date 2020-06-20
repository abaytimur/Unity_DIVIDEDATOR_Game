using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public GamaManagerSc gamaManagerSc;
    BackgroundMusic backgroundMusic;

    public void RestartGame()
    {
        backgroundMusic = Object.FindObjectOfType<BackgroundMusic>();
        backgroundMusic.acikMi = false;
        gamaManagerSc.remaningLife = 3;
        SceneManager.LoadScene("GameLevel");
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MenuLevel");
    }
}
