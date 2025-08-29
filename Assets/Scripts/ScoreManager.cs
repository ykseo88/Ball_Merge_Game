using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public TMP_Text[] scoreTexts = new TMP_Text[5];
    public TMP_Text[] resultScoreTexts = new TMP_Text[5];
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.scoreManager = this;
        UpdateScore(scoreTexts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(TMP_Text[] tempScoreTexts)
    {
        int tempScore = currentScore;
        
        for (int i = tempScoreTexts.Length - 1; i >= 0; i--)
        {
            int temp = (int)(tempScore / Mathf.Pow(10, i));
            tempScore -= temp * (int)Mathf.Pow(10, i);
            tempScoreTexts[i].text = temp.ToString();
        }
    }
}
