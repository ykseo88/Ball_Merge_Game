using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankElement : MonoBehaviour
{
    public TMP_Text rank;
    public TMP_Text score;
    public TMP_Text nickName;

    public void SetRankElement(string rank, string score, string nickName)
    {
        this.rank.text = rank;
        this.score.text = score;
        this.nickName.text = nickName;
    }
}
