using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SAOBallDatabase", menuName = "Scripts/Ball Database"), ]
public class SAOBallDatabase : ScriptableObject
{
    public BallData[] ballDatas;

    public BallData GetBallDataByLevel(int level)
    {
        foreach (BallData ballData in ballDatas)
        {
            if(ballData.level == level) return ballData;
        }

        return null;
    }
}
