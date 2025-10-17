using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject settingsPanel, creditPanel;

    private bool isPaused, isSettings;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else if (isSettings)
            {
                settingsPanel.SetActive(false);
                creditPanel.SetActive(false);
            }
            else ResumeGame();
        }
    }

    public void PauseGame()
    {
        GetComponent<Animator>().SetTrigger("pause");
        isPaused = true;
        GameManagerr.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        GetComponent<Animator>().SetTrigger("resume");
        isPaused = false;
        GameManagerr.Instance.ResumeGame();
    }

    public void SettingsButton()
    {
        GameManagerr.Instance.SoundPlay(2);
        if (isSettings)
        {
            isSettings = false;
            settingsPanel.SetActive(false);
<<<<<<< Updated upstream
        }else
        {
            isSettings = true;
            settingsPanel.SetActive(true);
        }
    }
=======
        }
        else
        {
            isSettings = true;
            settingsPanel.SetActive(true);

            StartCoroutine(RefreshAudioButtonDelayed());
        }
    }
    
    private IEnumerator RefreshAudioButtonDelayed()
    {
        yield return null;

        if (BGMManager.Instance != null)
        BGMManager.Instance.RefreshUIButton();
    }
>>>>>>> Stashed changes

    public void CreditButton()
    {
        if (isSettings)
        {
            isSettings = false;
            creditPanel.SetActive(false);
        }else
        {
            isSettings = true;
            creditPanel.SetActive(true);
        }
    }

    public void MainMenu()
    {
        GameManagerr.Instance.ToMainMenu();
    }
}
