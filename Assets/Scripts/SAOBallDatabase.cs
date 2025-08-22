using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SAOBallDatabase", menuName = "Scripts/Ball Database"), ]
public class SAOBallDatabase : ScriptableObject
{
    public BallData[] ballDatas;
}
