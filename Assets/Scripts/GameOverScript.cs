using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScoreScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI ScoreOverlay;
    private int score;
    public void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        ScoreScreen.SetActive(false);
        pointsText.text = "Score: " + score.ToString();
    }

    public void SetScore(int incomingScore)
    {
        score = incomingScore;
        ScoreOverlay.text = "Score: " + score.ToString();
    }

    public void HideGameOverScreen()
    {
        gameOverScreen.SetActive(false);
        ScoreScreen.SetActive(true);
    }
}
