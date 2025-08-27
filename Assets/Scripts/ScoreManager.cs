using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    [SerializeField] TMP_Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.scoreManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();
    }
}
