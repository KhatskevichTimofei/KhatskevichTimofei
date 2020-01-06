using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UnityEngine.MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Programist rukozhop");
    }

    public void ExitInMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
