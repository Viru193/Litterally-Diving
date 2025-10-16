using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public string whichMixer;

    public Sprite buttonMute, buttonUnmute;
    private Image theButton;

    void Start()
    {
        theButton = GetComponent<Image>();

        if (PlayerPrefs.GetInt(whichMixer) == 1)
        {
            MuteAudioGroup();
        }
        else UnmuteAudioGroup();
    }

    public void MuteAudioGroup()
    {
        mixer.SetFloat(whichMixer, -80f);
        PlayerPrefs.SetInt(whichMixer, 1);
        UIButtonToggle(true);
    }

    public void UnmuteAudioGroup()
    {
        mixer.SetFloat(whichMixer, 0);
        PlayerPrefs.SetInt(whichMixer, 0);
        UIButtonToggle(false);
    }

    public void ToggleMuteAudioGroup()
    {
        float volume;
        mixer.GetFloat(whichMixer, out volume);

        if (volume <= -79f)
        {
            UnmuteAudioGroup();
        }
        else
        {
            MuteAudioGroup();
        }
    }

    void UIButtonToggle(bool mode)
    {
        if (theButton)
        {
            if (buttonMute && buttonUnmute)
            {
                if (mode)
                {
                    theButton.sprite = buttonMute;
                }else
                {
                    theButton.sprite = buttonUnmute;
                }
            }else Debug.Log("Button UI non-existent.");
        }
    }
}
