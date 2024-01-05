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

    private void Start()
    {
        maxGoods = gameManager.maxGoods;
        text.text = gameManager.goodsCollected.ToString() + "/" + maxGoods.ToString();
    }


    public void AddScore()
    {
        score++;
        gameManager.goodsCollected = score;
        text.text = score.ToString() + "/" + maxGoods.ToString();
    }
}
