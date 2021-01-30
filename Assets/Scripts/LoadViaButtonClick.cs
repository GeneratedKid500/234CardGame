using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadViaButtonClick : MonoBehaviour
{
    public Text score;

    private void Start()
    {
        score.text = score.text + " " + MatchGameManager.score;
    }

    public void Load(string level)
    {
        if (level == "Quit")
            Application.Quit();
        else if (level != null)
        {
            MatchGameManager.score = 0;
            SceneManager.LoadScene(level);
        }

    }  
}
