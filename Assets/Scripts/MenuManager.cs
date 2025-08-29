using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button ExitOptionButton;
    [SerializeField] private Button RegistButton;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private float GameOverOnTime;
    [SerializeField] private TMP_InputField NickNameInput;

    private Vector3 GameOverPanelScale;
    private SaveManager saveManager;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.menuManager = this;
        saveManager = GameManager.instance.saveManager;
        MenuButton.onClick.AddListener(OnMenu);
        ResumeButton.onClick.AddListener(Resume);
        ExitButton.onClick.AddListener(ToTitle);
        OptionButton.onClick.AddListener(OnOption);
        ExitOptionButton.onClick.AddListener(ExitOption);
        RegistButton.onClick.AddListener(RegistRanking);
        GameOverPanelScale = GameOverPanel.transform.localScale;
        SetSoundVolume();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();
    }
    
    public async void SetSoundVolume()
    {
        await GameManager.instance.saveManager.Load();
        BGMVolumeSlider.value = GameManager.instance.saveBgmValue;
        SFXVolumeSlider.value = GameManager.instance.saveSfxValue;
    }

    private void OnMenu()
    {
        OnClickSound();
        menu.SetActive(true);
        GameManager.instance.inputManager.gameObject.SetActive(false);
    }

    private void Resume()
    {
        OnClickSound();
        menu.SetActive(false);
        GameManager.instance.inputManager.gameObject.SetActive(true);
    }

    private void ToTitle()
    {
        OnClickSound();
        SceneManager.LoadScene("Lobby");
    }

    private void OnOption()
    {
        OnClickSound();
        optionMenu.SetActive(true);
    }

    private void ExitOption()
    {
        OnClickSound();
        saveManager.SaveOption();
        optionMenu.SetActive(false);
    }

    private void UpdateVolume()
    {
        GameManager.instance.soundManager.SFXVolume =  SFXVolumeSlider.value;
        GameManager.instance.soundManager.BGMSource.volume =  BGMVolumeSlider.value;
    }

    private void OnClickSound()
    {
        GameManager.instance.soundManager.OnSound(GameManager.instance.database.GetSoundDataByName("Click").clip);
    }

    public void OnGameOver()
    {
        GameOverPanel.SetActive(true);
        StartCoroutine(OnBiggerPanel());
    }

    private IEnumerator OnBiggerPanel()
    {
        float currentTime = 0f;

        while (currentTime <= GameOverOnTime)
        {
            currentTime += Time.deltaTime;
            GameOverPanel.transform.localScale = Vector3.Lerp(Vector3.zero, GameOverPanelScale, currentTime/GameOverOnTime);
            yield return null;
        }
    }

    private void RegistRanking()
    {
        PlayerScoreData score = new PlayerScoreData();
        score.playerName = NickNameInput.text;
        score.playerScore = GameManager.instance.scoreManager.currentScore;
        
        GameManager.instance.saveManager.UpdateRanking(score);
        SceneManager.LoadScene("Lobby");
    }
    
}
