using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public MainMenu mainMenu;

    public void TestingMethod()
    {
        if(mainMenu.GetButtonName().Equals("settings"))
        {
            mainMenu.OpenSettings();
        }

        if (mainMenu.GetButtonName().Equals("play"))
        {
            SceneManager.LoadScene("MainScene");
        }

        if (mainMenu.GetButtonName().Equals("restart"))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        if (mainMenu.GetButtonName().Equals("showgoal"))
        {

        }

        if (mainMenu.GetButtonName().Equals("home"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
