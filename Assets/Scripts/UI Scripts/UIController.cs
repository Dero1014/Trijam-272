using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] Screens;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    public void ShowScreen(int index)
    {
        foreach (var screen in Screens)
        {
            screen.SetActive(false);
        }

        Screens[index].SetActive(true);
    }

    public void UpdateScore(int currentScore)
    {
        ScoreText.text = currentScore.ToString();
    }

    public void ShowHighScore(int highScore, int currentScore)
    {
        HighScoreText.text = $"Current score\n{currentScore}\nHIGH SCORE\n{highScore}\n\nSPACE TO GO AGAIN";
    }
}
