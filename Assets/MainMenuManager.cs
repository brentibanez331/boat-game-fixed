using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Diagnostics.Tracing;
//using Unity.PlasticSCM.Editor.WebApi;

public class MainMenu : MonoBehaviour
{
    public Animator settingsAnim;
    public Animator menuAnim;

    public string buttonName;
    GameObject followTarget;

    [HideInInspector] public bool gameIsPaused;
    public bool isEndless = false;

    private void Awake()
    {
        if (!isEndless)
        {
            gameIsPaused = true;
        }
        else
        {
            gameIsPaused = false;
        }
        CloseSettings();
        CloseMenu();
        settingsAnim.gameObject.SetActive(false);
        menuAnim.gameObject.SetActive(false);
    }

    public void EnableMenu()
    {
        settingsAnim.gameObject.SetActive(true);
        menuAnim.gameObject.SetActive(true);
        CloseSettings();
    }

    public void OpenMenu()
    {
        print("menu opening");
        menuAnim.SetBool("MenuIsClosed", false);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void CloseMenu()
    {
        menuAnim.SetBool("MenuIsClosed", true);
        Debug.Log("This is played");
    }

    public void OpenSettings()
    {
        settingsAnim.SetBool("SettingsIsClosed", false);
    }

    public void CloseSettings()
    {
        settingsAnim.SetBool("SettingsIsClosed", true);
    }

    public void SetButtonName(string buttonName_)
    {
        buttonName = buttonName_;
    }

    public void HideObject(GameObject objectToHide)
    {
        objectToHide.SetActive(false);
    }

    public void ShowObject(GameObject objectToShow)
    {
        objectToShow.SetActive(true);
    }

    public string GetButtonName()
    {
        return buttonName;
    }

    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void PauseGame(CinemachineFreeLook vCam)
    {
        gameIsPaused = true;
        vCam.Follow = null;
        vCam.m_XAxis.m_MaxSpeed = 0;
        vCam.m_YAxis.m_MaxSpeed = 0;
        vCam.LookAt = null;
    }

    public void GetFollowTarget(GameObject followTarget_)
    {
        followTarget = followTarget_;
    }

    public void ResumeGame(CinemachineFreeLook vCam)
    {
        gameIsPaused = false;
        vCam.Follow = followTarget.transform;
        print(followTarget);
        vCam.m_XAxis.m_MaxSpeed = 100;
        vCam.m_YAxis.m_MaxSpeed = 2;
        vCam.LookAt = followTarget.transform;
    }
}
