using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_ResultManager : MonoBehaviour
{
    public GameManagerScript gameManager;
    float elapsedTime;
    public float time_1;
    public float time_2;
    int index = -1;
    bool gameDone = false;

    // Update is called once per frame
    void Update()
    {
        if (!gameDone)
        {
            if (gameManager.goodsCollected >= gameManager.maxGoods)
            {
                gameDone = true;
                elapsedTime = gameManager.remainingTime;
                gameManager.AddStar();

                if (elapsedTime >= time_1)
                {
                    gameManager.AddStar();
                }
                if (elapsedTime >= time_2)
                {
                    gameManager.AddStar();
                }
            }
        }
    }

    

    
}
