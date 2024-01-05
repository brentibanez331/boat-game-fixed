using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Diagnostics.Tracing;

public class MainMenu : MonoBehaviour
{
    public Animator settingsAnim;
    public Animator menuAnim;

    public string buttonName;
    GameObject followTarget;

    [HideInInspector] public bool gameIsPaused;

    private void Awake()
    {
        gameIsPaused = true;
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
        Debug.Log("This is played");
    }

    public void SetButtonName(string buttonName_)
    {
        buttonName = buttonName_;
        print(buttonName);
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

    public void PauseGame(CinemachineFreeLook vCam)
    {
        gameIsPaused = true;
        vCam.Follow = null;
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
        vCam.LookAt = followTarget.transform;
    }
}
