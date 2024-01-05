using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    public RetrieveObject retrieveObj;

    //sfx
    public AudioSource starFX;

    public void AnimateStar()
    {   
        if(gameManager.starCount != 0)
        {
            if (gameManager.index < gameManager.starCount)
            {
                print("Index: " + gameManager.index + "  Stars: " + gameManager.starCount);
                retrieveObj.starUI[gameManager.index].SetBool("playStar", true);
                gameManager.index++;
            }
        }
    }

    public void AudioPlay()
    {
        starFX.Play();
    }
}
