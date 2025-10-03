using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        GameManagerr.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        GameManagerr.Instance.ResumeGame();
    }

    public void MainMenu()
    {
        GameManagerr.Instance.ToMainMenu();
    }
}
