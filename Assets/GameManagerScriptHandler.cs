using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public Animator questAnim;
    public Animator helpAnim;
    public MainMenu mainMenu;
    public CinemachineFreeLook vCam;
    public bool playTimer = false;
    public TextMeshProUGUI timerText;
    public float remainingTime;
    public TextMeshProUGUI resultText;
    public int starCount = 0;
    public int maxGoods;
    public int index = 0;
    public RetrieveObject retrieveObj;
    public int goodsCollected;

    //SFX
    public AudioSource alarmSFX;
    public AudioSource tickSFX;
    public AudioSource levelCompleteFX;

    private void Awake()
    {
        for(int i = 0; i < retrieveObj.starUI.Length; i++)
        {
            retrieveObj.starUI[i].SetBool("playStar", false);
        }
        
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            OpenTutorial();
        }
        else
        {
            CloseTutorial();
            OpenQuest();    
        }
        //OpenQuest();
        CloseResult();
        HideGoal();
        mainMenu.PauseGame(vCam);
    }

    public void ShowGoal()
    {
        retrieveObj.TimerUI.GetComponent<Animator>().SetBool("TimerIsClosed", false);
        retrieveObj.ScoreUI.GetComponent<Animator>().SetBool("ScoreIsClosed", false);
        playTimer = true;
    }

    public void HideGoal()
    {
        retrieveObj.TimerUI.GetComponent<Animator>().SetBool("TimerIsClosed", true);
        retrieveObj.ScoreUI.GetComponent<Animator>().SetBool("ScoreIsClosed", true);
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
        retrieveObj.ResultUI.GetComponent<Animator>().SetBool("ResultIsClosed", false);
    }

    public void CloseResult()
    {
        retrieveObj.ResultUI.GetComponent<Animator>().SetBool("ResultIsClosed", true);
    }

    public void AddStar()
    {
        starCount++;
        //print(starCount);
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
                if(remainingTime <= 3 && remainingTime > 2.6f)
                {
                    tickSFX.Play();
                }
                if (remainingTime <= 2 && remainingTime > 1.6f)
                {
                    tickSFX.Play();
                }
                if (remainingTime <= 1 && remainingTime > .6f)
                {
                    tickSFX.Play();
                }

                timerText.color = Color.red;
            }
            if(remainingTime <= 0 && remainingTime >= -0.1)
            {
                alarmSFX.Play();
            }
            if(remainingTime <= 0 && goodsCollected < maxGoods)
            {
                //Stop Warning Audio


                minutes = 0;
                seconds = 0;    
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                if(remainingTime < -2)
                {
                    remainingTime = 0;
                    HideGoal();
                    OpenResult();
                    playTimer = false;
                    mainMenu.PauseGame(vCam);

                    if (goodsCollected <= maxGoods)
                    {
                        resultText.text = "LEVEL FAILED!";
                    }
                }
            }

            if(remainingTime > 0 && goodsCollected >= maxGoods)
            {
                HideGoal();
                OpenResult();
                levelCompleteFX.Play();
                playTimer = false;
                mainMenu.PauseGame(vCam);
                resultText.text = "LEVEL COMPLETE!";
            }
        }
    }

    public void AddScore()
    {
        goodsCollected++;
    }
}
