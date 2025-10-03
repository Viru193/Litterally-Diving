using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
   public GameObject tutorialPanel;

    private string tutorialKey = "TutorialShown";

    private void Start()
    {
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 0)
        {
            if (tutorialPanel != null)
                tutorialPanel.SetActive(true);
        }
        else
        {
            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);

        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
    }
}
