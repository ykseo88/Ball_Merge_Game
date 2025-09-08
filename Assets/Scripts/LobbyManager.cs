using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button OptionButton;
    [SerializeField] private Button rankingButton;
    [SerializeField] private Button exitButton;
    
    [SerializeField] private GameObject RankingPanel;
    [SerializeField] private GameObject RankingsPanel;
    [SerializeField] private Button BackButton;
    [SerializeField] private GameObject RankElementPrefab;
    
    [SerializeField] private GameObject OptionPanel;
    [SerializeField] private Button OptionBackButton;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;
    
    [SerializeField] private SaveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(GameStart);
        rankingButton.onClick.AddListener(OnRanking);
        BackButton.onClick.AddListener(OffRanking);
        exitButton.onClick.AddListener(Exit);
        OptionButton.onClick.AddListener(OnOption);
        OptionBackButton.onClick.AddListener(ExitOption);
        SetSoundVolume();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVolume();
    }

    public void SetSoundVolume()
    {
        BGMSlider.value = GameManager.instance.saveBgmValue;
        SFXSlider.value = GameManager.instance.saveSfxValue;
    }

    private void Exit()
    {
        OnClickSound();
        Application.Quit();
    }

    private async void GameStart()
    {
        OnClickSound();
        saveManager.Load();
        saveManager.SaveOption();
        SceneManager.LoadScene(1);
    }

    private void OnRanking()
    {
        OnClickSound();
        SetRanking();
        RankingPanel.SetActive(true);
    }

    private void SetRanking()
    {
        DestroyAllChildren(RankingsPanel);
        for (int i = 0; i < saveManager.ranking.Count; i++)
        {
            Instantiate(RankElementPrefab, RankingsPanel.transform).transform.TryGetComponent(out RankElement rankElement);
            rankElement.SetRankElement((i+1).ToString(), saveManager.ranking[i].playerScore.ToString(), saveManager.ranking[i].playerName);
        }
    }

    private void OffRanking()
    {
        OnClickSound();
        RankingPanel.SetActive(false);
    }
    
    public void DestroyAllChildren(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
    }
    
    private void OnClickSound()
    {
        GameManager.instance.soundManager.OnSound(GameManager.instance.database.GetSoundDataByName("Click").clip);
    }
    
    private void UpdateVolume()
    {
        GameManager.instance.soundManager.SFXVolume =  SFXSlider.value;
        GameManager.instance.soundManager.BGMSource.volume =  BGMSlider.value;
    }
    
    private void ExitOption()
    {
        OnClickSound();
        saveManager.SaveOption();
        OptionPanel.SetActive(false);
    }
    
    private void OnOption()
    {
        OnClickSound();
        OptionPanel.SetActive(true);
    }
    
    public void SetOptionSliderValue(float BgmBalue, float SfxValue)
    {
        BGMSlider.value = BgmBalue;
        SFXSlider.value = SfxValue;
    }
}
