using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayTheme("None");
        }
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

    public void MoreCredits()
    {
        GameManager.instance.loadLevel("CreditsScreen");
    }

    public void Credits()
    {
        GameManager.instance.loadLevel("CreditsScreen2");
    }
}
