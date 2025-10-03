using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonSound;

    [Header("Scene Setting")]
    public string gameSceneName = "Litterally Diving";

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionPanel;
    public GameObject creditsPanel;
    public Text highScorePanel;

    void Start()
    {
        highScorePanel.text = PlayerPrefs.GetInt("HighScore").ToString();
        Debug.Log(PlayerPrefs.GetInt("HighScore"));
    }

    public void PlayGame()
    {
        SoundPlay();
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        SoundPlay();
        Application.Quit();
    }

    public void Options()
    {
        SoundPlay();
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void Credits()
    {
        SoundPlay();
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void Menu()
    {
        SoundPlay();
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    void SoundPlay()
    {
        if (buttonSound != null)
        {
            buttonSound.Stop();
            buttonSound.Play();
        }
    }
}
