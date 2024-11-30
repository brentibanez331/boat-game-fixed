using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGoalUI : MonoBehaviour
{
    public GameManagerScript gameManager;
    public MainMenu mainMenu;
    public bool isEndless = false;

    private void Awake()
    {
        isEndless = gameManager.isEndlesss;
        print(isEndless);
    }
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
            if (!isEndless)
            {
                gameManager.OpenQuest();
            }
        }

        if (mainMenu.GetButtonName().Equals("closemenu"))
        {
            print("Timer is resumed from ShowGoalUI: closemenu");
            ResumeTimer();
        }
    }
}
