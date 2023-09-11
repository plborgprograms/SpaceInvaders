using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public static void FinishLevel(bool win)
    {
        ScoreBoard.UpdateScores();
        if (win)
        {
            instance.GetComponent<EndOfLevelScreen>().winScreen.SetActive(true);
        }
        else
        {
            instance.GetComponent<EndOfLevelScreen>().loseScreen.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (PauseMenu.paused)
            {
                this.GetComponent<PauseMenu>().Resume();
            }
            else
            {
                this.GetComponent<PauseMenu>().Pause();
            }
        }
    }
}
