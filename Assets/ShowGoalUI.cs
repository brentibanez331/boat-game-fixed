using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGoalUI : MonoBehaviour
{
    public GameManager gameManager;
    public MainMenu mainMenu;

    public void ShowUIObjects()
    {
        gameManager.ShowGoal();
    }

    public void ResumeTimer()
    {
        gameManager.ResumeTimer();
    }

    public void CheckerMethod()
    {
        if (mainMenu.GetButtonName().Equals("savesettings"))
        {
            mainMenu.OpenMenu();
        }
        if (mainMenu.GetButtonName().Equals("closesettings"))
        {
            ResumeTimer();
        }
        if (mainMenu.GetButtonName().Equals("closehelp"))
        {
            gameManager.OpenQuest();
        }

        if (mainMenu.GetButtonName().Equals("closemenu"))
        {
            print("Timer is resumed from ShowGoalUI: closemenu");
            ResumeTimer();
        }
    }
}
