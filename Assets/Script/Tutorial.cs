using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel, fishesPanel;

    public bool shownTutorial, shownFishes;

    private void Start()
    {
        shownTutorial = false;
        shownFishes = false;
    }

    public void OpenOrCloseTutorial()
    {
        if (tutorialPanel != null) 
        {
            if (shownTutorial)
            {
                tutorialPanel.SetActive(false);
                shownTutorial = false;
            }else
            {
                tutorialPanel.SetActive(true);
                shownTutorial = true;
            }
        }
        PlaySound();
    }

    public void OpenOrCloseFishes()
    {
        if (fishesPanel != null) 
        {
            if (shownFishes)
            {
                fishesPanel.SetActive(false);
                shownFishes = false;
            }else
            {
                fishesPanel.SetActive(true);
                shownFishes = true;
            }
        }
        PlaySound();
    }

    void PlaySound()
    {
        if (GameManagerr.Instance != null)
        {
            GameManagerr.Instance.SoundPlay(2);
        }
    }
}
