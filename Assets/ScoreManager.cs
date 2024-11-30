using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    public int score = 0; 
    public GameManagerScript gameManager;
    int maxGoods;
    public bool isEndless = false;

    private void Start()
    {
        maxGoods = gameManager.maxGoods;
        if (isEndless)
        {  
            text.text = gameManager.goodsCollected.ToString();
        }
        else
        {
            text.text = gameManager.goodsCollected.ToString() + "/" + maxGoods.ToString();
        }
    }


    public void AddScore()
    {
        score++;
        gameManager.goodsCollected = score;
        if (isEndless)
        {
            text.text = score.ToString();
        }
        else
        {
            text.text = score.ToString() + "/" + maxGoods.ToString();
        }
    }
}
