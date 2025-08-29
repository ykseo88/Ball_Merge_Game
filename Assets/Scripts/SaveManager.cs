using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

[System.Serializable]
public class RankingList
{
    public List<PlayerScoreData>  playerScores;
}

[System.Serializable]
public class OptionValue
{
    public float BGMValue;
    public float SEXValue;
}


public class SaveManager : MonoBehaviour
{
    public List<PlayerScoreData> ranking = new List<PlayerScoreData>();
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance != null) GameManager.instance.saveManager = this;
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRanking(PlayerScoreData score)
    {
        ranking.Add(score);

        for (int i = 0; i < ranking.Count; i++)
        {
            for (int j = 0; j < ranking.Count; j++)
            {
                if (ranking[i].playerScore > ranking[j].playerScore)
                {
                    (ranking[i], ranking[j]) = (ranking[j], ranking[i]);
                }
            }
        }
        
        SaveRanking();
    }

    public void SaveRanking()
    {
        RankingList rankingList = new RankingList();
        rankingList.playerScores = ranking;
        
        string json = JsonUtility.ToJson(rankingList);
        string filePath = Application.persistentDataPath + "/ranking.json";
        
        File.WriteAllText(filePath, json);
    }

    public void SaveOption()
    {
        OptionValue optionValue = new OptionValue();
        optionValue.BGMValue = GameManager.instance.soundManager.BGMSource.volume;
        optionValue.SEXValue = GameManager.instance.soundManager.SFXVolume;
        
        string json = JsonUtility.ToJson(optionValue);
        string filePath = Application.persistentDataPath + "/OptionVolume.json";
        
        File.WriteAllText(filePath, json);
    }

    public async Task Load()
    {
        string filePath = Application.persistentDataPath + "/ranking.json";

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);

            RankingList rankingList = JsonUtility.FromJson<RankingList>(jsonString);
            ranking = rankingList.playerScores;
        }
        
        filePath = Application.persistentDataPath + "/OptionVolume.json";

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);

            OptionValue optionValue = JsonUtility.FromJson<OptionValue>(jsonString);
            GameManager.instance.saveSfxValue = optionValue.SEXValue;
            GameManager.instance.saveBgmValue = optionValue.BGMValue;
        }
    }
}
