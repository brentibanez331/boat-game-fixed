using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_ResultManager : MonoBehaviour
{
    public GameManager gameManager;
    int starCount = 0;

    //This script is unique per level for checking varying winning conditions
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.goodsCollected >= gameManager.maxGoods)
        {
            gameManager.playTimer = false;
            AddStar();
        }
    }

    void AddStar()
    {
        starCount++;
    }
}
