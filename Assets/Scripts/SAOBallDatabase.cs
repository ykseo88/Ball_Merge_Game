using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SAODatabase", menuName = "Scripts/Database"),]
public class SAODatabase : ScriptableObject
{
    public BallData[] ballDatas;

    public SoundData[] soundDatas;

    public BallData GetBallDataByLevel(int level)
    {
        foreach (BallData ballData in ballDatas)
        {
            if (ballData.level == level) return ballData;
        }

        return null;
    }

    public SoundData GetSoundDataByName(string soundName)
    {
        foreach (SoundData soundData in soundDatas)
        {
            if (soundData.name == soundName) return soundData;
        }

        return null;
    }
}
    
    
