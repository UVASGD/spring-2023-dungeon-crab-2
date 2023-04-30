using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.loadLevel("IntroCutscene");
        //AudioManager.instance.switchTheme();
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        GameManager.instance.loadLevel("MainMenu");
    }

    public void Credits()
    {
        GameManager.instance.loadLevel("CreditsScreen");
    }
}
