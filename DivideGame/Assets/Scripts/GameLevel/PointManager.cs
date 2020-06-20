using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int totalPoints;
    private int pointIncrease;
    public Text highScore;

    [SerializeField]
    private Text pointsText;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteKey("HighScore");
        pointsText.text = totalPoints.ToString();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    
    public void HighScore()
    {
         
    }

    public void IncreasePoints(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                pointIncrease = 5;
                    break;
            case "Medium":
                pointIncrease = 10;
                break;
            case "Hard":
                pointIncrease = 15;
                break;
        }
        totalPoints += pointIncrease;
        pointsText.text = totalPoints.ToString();

        if(totalPoints > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", totalPoints);
            highScore.text = totalPoints.ToString();
        }
    }

    public void DecreasePoints()
    {
        totalPoints -= 5;
        if (totalPoints <= 0)
        {
            totalPoints = 0;
        }
        pointsText.text = totalPoints.ToString();
    }
}
