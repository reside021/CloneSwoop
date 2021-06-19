using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private bool gameIsPause = false;

    [SerializeField] private GameObject _pauseMenuUi;
    [SerializeField] private GameObject _exitMenuUi;
    [SerializeField] private GameObject _gameOverUi;

    void Update()
    {
        if (_gameOverUi.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume()
    {
        _pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        _pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }
    public void ChangeScenes(int numScenes)
    {
        Time.timeScale = 1f;
        gameIsPause = false;
        SceneManager.LoadScene(numScenes);
    }

    public void ExitMessage()
    {
        _exitMenuUi.SetActive(true);
    }

    public void CancelExit()
    {
        _exitMenuUi.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
