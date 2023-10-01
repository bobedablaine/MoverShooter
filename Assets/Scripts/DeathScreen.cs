using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;

    public void DeathSetActive(int score, int highscore)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
        highscoreText.text = "High Score: " + highscore.ToString();

    }
}
