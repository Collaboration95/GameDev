using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "Score: " + score.ToString();
    }

    public void HideGameOverScreen()
    {
        gameObject.SetActive(false);
    }
}
