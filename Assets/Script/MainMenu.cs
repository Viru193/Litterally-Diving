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

<<<<<<< Updated upstream
=======
    [Header("GForm Link")]
    [Tooltip("Linknya bro")]
    public string gFormURL = "https://torku.itch.io/litterally-diving";

>>>>>>> Stashed changes
    private bool isInMenu;

    void Start()
    {
        Time.timeScale = 1f;
        highScorePanel.text = PlayerPrefs.GetInt("HighScore").ToString();
        isInMenu = true;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isInMenu)
            {
                QuitGame();
            }
            else Menu();
        }
    }

    public void PlayGame()
    {
        SoundPlay();
        PlayerPrefs.Save();
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        SoundPlay();
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Options()
    {
        SoundPlay();
        mainMenuPanel.SetActive(false);
        optionPanel.SetActive(true);
        isInMenu = false;
    }

    public void Credits()
    {
        SoundPlay();
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
        isInMenu = false;
    }

    public void Menu()
    {
        SoundPlay();
        mainMenuPanel.SetActive(true);
        optionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        isInMenu = true;
    }

    public void OpenGForm() 
    {
        SoundPlay();
        if (!string.IsNullOrEmpty(gFormURL)) 
        {
            Application.OpenURL(gFormURL);
        }
        else
        {
            Debug.LogWarning("Linknya Mana?????");
        }
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
