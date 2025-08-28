using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button ExitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        MenuButton.onClick.AddListener(OnMenu);
        ResumeButton.onClick.AddListener(Resume);
        ExitButton.onClick.AddListener(ToTitle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMenu()
    {
        menu.SetActive(true);
        GameManager.instance.inputManager.gameObject.SetActive(false);
    }

    private void Resume()
    {
        menu.SetActive(false);
        GameManager.instance.inputManager.gameObject.SetActive(true);
    }

    private void ToTitle()
    {
        SceneManager.LoadScene("Lobby");
    }
}
