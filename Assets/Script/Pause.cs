using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseGame()
    {
        GetComponent<Animator>().SetTrigger("pause");
        GameManagerr.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GetComponent<Animator>().SetTrigger("resume");
        GameManagerr.Instance.ResumeGame();
    }

    public void MainMenu()
    {
        GameManagerr.Instance.ToMainMenu();
    }
}
