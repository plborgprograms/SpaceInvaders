using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;

    private static int score = 0;

    public static List<int> highScores = new List<int>() { 10000000, 8000000, 2500000, 1000000, 500000 };
    public static int capacity = 5;

    public TMP_Text scoreUIText;
    public TMP_Text livesUIText;

    private void Awake()
    {
        instance = this;
    }

    public static List<int> LoadScores()
    {
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i.ToString(), highScores[i]);
            highScores[i] = score;
        }

        return highScores;
    }

    public static void UpdateScores()
    {
        highScores.Add(score);
        highScores.Sort();
        highScores.Reverse();

        if(highScores.Count > capacity)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), highScores[i]);
        }
    }

    public void UpdateScoreBoard(int increment)
    {
        score += increment;
        scoreUIText.text = "Score:" + score;
        Debug.Log("Total Score:" + score);
    }

    public void UpdateLives(int Lives)
    {
        livesUIText.text = "Lives:" + Lives;
        Debug.Log("Lives:" + Lives);
    }

}
