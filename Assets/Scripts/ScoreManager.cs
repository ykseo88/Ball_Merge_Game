using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] TMP_Text scoreText1;
    [SerializeField] TMP_Text scoreText10;
    [SerializeField] TMP_Text scoreText100;
    [SerializeField] TMP_Text scoreText1000;
    [SerializeField] TMP_Text scoreText10000;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.scoreManager = this;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        int tempScore = currentScore;
        int temp10000 = tempScore / 10000;
        tempScore -= temp10000 * 10000;
        int temp1000 = tempScore / 1000;
        tempScore -= temp1000 * 1000;
        int temp100 = tempScore / 100;
        tempScore -= temp100 * 100;
        int temp10 = tempScore / 10;
        tempScore -= temp10 * 10;
        int temp1 = tempScore;
        
        scoreText1.text = temp1.ToString();
        scoreText10.text = temp10.ToString();
        scoreText100.text = temp100.ToString();
        scoreText1000.text = temp1000.ToString();
        scoreText10000.text = temp10000.ToString();
    }
}
