using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class EndOfLevelScreen : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    // Start is called before the first frame update

    public GameObject winScreenNextLevel;
    //public GameObject loseScreenNextLevel;
    void OnEnable()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if(SceneManager.GetActiveScene().buildIndex == sceneCount-1)
        {
            winScreenNextLevel.SetActive(false);
            //loseScreenNextLevel.SetActive(false); //useful in case there is a level skip option
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
