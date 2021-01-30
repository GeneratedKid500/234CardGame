using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    public Text lowSoFar;

    void Start()
    {
        lowSoFar.text = lowSoFar.text + " " + GetLowScore();
    }

    public static string GetLowScore()
    {
        Debug.Log(PlayerPrefs.GetInt("Score"));
        return PlayerPrefs.GetInt("Score").ToString();
    }

    public static void SetLowScore()
    {
        if (PlayerPrefs.GetInt("Score") > MatchGameManager.score)
        {
            PlayerPrefs.SetInt("Score", MatchGameManager.score);
        }
    }
}
