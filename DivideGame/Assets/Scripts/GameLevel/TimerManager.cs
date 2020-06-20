using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public float timeStart = 60f;
    public GamaManagerSc gamaManagerSc;
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart<= 0)
        {
            gamaManagerSc.GameOver();
            timerText.text = "0";
        }else if (gamaManagerSc.remaningLife == 0)
        {
            gamaManagerSc.GameOver();
            timerText.text = "0";
        }
        else if (gamaManagerSc.remaningLife != 0 && timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            timerText.text = Mathf.Round(timeStart).ToString();
        }
    }
}
