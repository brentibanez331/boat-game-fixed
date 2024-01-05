using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    ParticleSystem explosion;
    MeshRenderer bomb;

    GameManagerScript gameManager;
    CinemachineFreeLook vCam;


    private void Awake()
    {
        vCam = GameObject.FindGameObjectWithTag("CinemachineCam").GetComponent<CinemachineFreeLook>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        explosion = gameObject.GetComponentInChildren<ParticleSystem>();
        bomb = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    public void Explode()
    {
        explosion.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bomb.enabled = false;
            Explode();
            Destroy(other.gameObject);
            Destroy(gameObject, 3.0f);

            gameManager.goodsCollected = 0;
            gameManager.starCount = 0;  

            gameManager.HideGoal();
            gameManager.OpenResult();
            gameManager.playTimer = false;
            gameManager.mainMenu.PauseGame(vCam);
            gameManager.resultText.text = "LEVEL COMPLETE!";
            
        }
    }
}