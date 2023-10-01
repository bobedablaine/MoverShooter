using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    Text text;
    public int score = 0;

    public int highscore1;
    public int highscore2;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        highscore1 = PlayerPrefs.GetInt("highscore1", highscore1);
        highscore2 = PlayerPrefs.GetInt("highscore2", highscore2);
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (text.text != score.ToString())
        {
            text.text = score.ToString();
        }


        if (score > highscore1 && sceneName == "LevelOne")
        {
            highscore1 = score;

            PlayerPrefs.SetInt("highscore1", highscore1);
        }

        if (score > highscore2 && sceneName == "LevelTwo")
        {
            highscore2 = score;

            PlayerPrefs.SetInt("highscore2", highscore2);
        }
    }
}
