using UnityEngine;
using TMPro;

public class SharedData : MonoBehaviour
{

    public static SharedData Instance;

    public int score { get; private set; } = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
