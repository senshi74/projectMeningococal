using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public GameObject timeIsUp;

    [System.NonSerialized]public bool gameHasEnded = false;

    public GameObject gameCanvasUI;

    public GameObject completeLevelUI; //The screen that shows when you complete the level.

    public GameObject gameOverUI;

    // Use this for initialization
    void Start()
    {
        completeLevelUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

	// Update is called once per frame
	void Update () 
    {
        if (TimeLeft.timeleft <= 0)
        {
            Time.timeScale = 0;
            timeIsUp.gameObject.SetActive(true);
            gameCanvasUI.gameObject.SetActive(false);
            gameOverUI.gameObject.SetActive(true);
        }
	}

    public void RestartScence()
    {
        Time.timeScale = 1;
        TimeLeft.timeleft = 60f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompleteLevel()
    {
        gameCanvasUI.gameObject.SetActive(false);
        completeLevelUI.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
