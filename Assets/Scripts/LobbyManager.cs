using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(GameStart);
        exitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
