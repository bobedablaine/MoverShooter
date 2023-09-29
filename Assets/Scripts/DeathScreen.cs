using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Text scoretext;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoretext.text = "Score: " + score.ToString();
    }
}
