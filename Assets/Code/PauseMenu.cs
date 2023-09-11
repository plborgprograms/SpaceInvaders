using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool paused = false;
    public string mainMenuSceneName;

    public List<TMP_Text> highScoreLabels;
    // Start is called before the first frame update
    void Start()
    {
        setupHighScoreLabels();
        pauseMenu.SetActive(false);
    }

    public void setupHighScoreLabels()
    {
        List<int> scores = ScoreBoard.LoadScores();
        for (int i = 0; i < highScoreLabels.Count; i++)
        {
            highScoreLabels[i].text = "High Score:" + scores[i];
        }
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
