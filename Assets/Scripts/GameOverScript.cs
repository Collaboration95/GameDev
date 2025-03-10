using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverScript : Singleton<GameOverScript>
{
    // Start is called before the first frame update
    public GameObject ScoreScreen;
    public GameObject totalScoreText;
    public IntVariable gameScore;
    public GameObject gameOverScreen;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI ScoreOverlay;
    private int score;

    override public void Awake()
    {
        GameManager.instance.gameStart.AddListener(HideGameOverScreen);
        GameManager.instance.gameOver.AddListener(DisplayGameOverScreen);
        GameManager.instance.gameRestart.AddListener(HideGameOverScreen);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }
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
        totalScoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        // show
        totalScoreText.SetActive(true);
        ScoreScreen.SetActive(true);
    }
}
