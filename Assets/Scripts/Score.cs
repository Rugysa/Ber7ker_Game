using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour    
{
    private int score = 0;

    private void FixedUpdate()
    {
        GetComponent<TMP_Text>().text = score.ToString();
    }

    public void SetScore(int add)
    {
        score += add;
    }

    public int GetScore()
    {
        return score;
    }
}
