using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Dialogue1End()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Lose()
    {
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        SceneManager.LoadScene(8);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
