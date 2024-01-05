using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator questAnim;
    public Animator helpAnim;
    public MainMenu mainMenu;
    public CinemachineFreeLook vCam;
    public bool playTimer = false;
    public TextMeshProUGUI timerText;
    public float remainingTime;
    public TextMeshProUGUI resultText;

    public GameObject TimerUI;
    public GameObject ScoreUI;
    public GameObject ResultUI;

    public int maxGoods;
    [HideInInspector]
    public int goodsCollected;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            OpenTutorial();
        }
        //OpenQuest();
        CloseResult();
        HideGoal();
        mainMenu.PauseGame(vCam);
    }

    public void ShowGoal()
    {
        TimerUI.GetComponent<Animator>().SetBool("TimerIsClosed", false);
        ScoreUI.GetComponent<Animator>().SetBool("ScoreIsClosed", false);
        playTimer = true;
    }

    public void HideGoal()
    {
        TimerUI.GetComponent<Animator>().SetBool("TimerIsClosed", true);
        ScoreUI.GetComponent<Animator>().SetBool("ScoreIsClosed", true);
    }

    public void OpenTutorial()
    {
        helpAnim.SetBool("HelpIsClosed", false);
    }

    public void CloseTutorial()
    {
        helpAnim.SetBool("HelpIsClosed", true);
    }

    public void OpenQuest()
    {
        questAnim.SetBool("QuestIsClosed", false);
    }

    public void CloseQuest()
    {
        questAnim.SetBool("QuestIsClosed", true);
    }

    public void StopTimer()
    {
        playTimer = false;
    }

    public void ResumeTimer()
    {
        playTimer = true;
    }

    public void OpenResult()
    {
        ResultUI.GetComponent<Animator>().SetBool("ResultIsClosed", false);
    }

    public void CloseResult()
    {
        ResultUI.GetComponent<Animator>().SetBool("ResultIsClosed", true);
    }

    private void Update()
    {
        if (playTimer)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if(goodsCollected == maxGoods)
            {
                //Player won Game
            }

            if(remainingTime <= 3)
            {
                timerText.color = Color.red;
            }

            if(remainingTime <= 0 && goodsCollected < maxGoods)
            {
                //remainingTime = 0;
                minutes = 0;
                seconds = 0;    
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                //Show GameOver Screen
                print("GameOver");
                if(remainingTime < -2)
                {
                    remainingTime = 0;
                    HideGoal();
                    OpenResult();
                    //Calculate result
                    resultText.text = "LEVEL FAILED!";
                }
                
            }
        }

        
    }
}
