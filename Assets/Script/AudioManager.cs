using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public string musicVol = "MusicVol";

    void Start()
    {
        if (PlayerPrefs.GetInt("MuteMusic") == 1)
        {
            MuteAudioGroup();
        }
        else UnmuteAudioGroup();
    }

    public void MuteAudioGroup()
    {
        mixer.SetFloat(musicVol, -80f);
        PlayerPrefs.SetInt("MuteMusic", 1);
    }

    public void UnmuteAudioGroup()
    {
        mixer.SetFloat(musicVol, 0);
        PlayerPrefs.SetInt("MuteMusic", 0);
    }

    public void ToggleMuteAudioGroup()
    {
        float volume;
        mixer.GetFloat(musicVol, out volume);

        if (volume <= -79f)
        {
            UnmuteAudioGroup();
        }
        else
        {
            MuteAudioGroup();
        }
    }
}
