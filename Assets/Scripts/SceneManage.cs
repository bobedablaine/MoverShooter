using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        SceneManager.LoadScene(scene.name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelOne()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelOne");
    }

    public void LoadLevelTwo()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelTwo");
    }

    public void LoadLevelThree()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelThree");
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
