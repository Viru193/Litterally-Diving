using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BGMManager : MonoBehaviour
{
    [Header("Main Menu")]
    public AudioSource mainMenuBGM;
    public AudioSource mainMenuSFX;
    public AudioClip buttonClickMenu;

    [Header("GamePlay")]
    public AudioSource gameplayBGM;
    public AudioSource gameplaySFX;
    public AudioClip buttonClickGameplay;
    public AudioClip balloonPop;
    public AudioClip oxygenPickup;
    public AudioClip warningSound;
    public AudioClip scoreSound;

    [Header("Dicky SFX")]
    public AudioSource playerSFX;
    public AudioClip playerSplash;

    public Button toggleButton;
    public TextMeshProUGUI toggleAudioText;

    private bool isMuted = false;

    public static BGMManager Instance;
    // public Slider volumeSlider;
    // private float defaultVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        // {
        //     bgm.volume = defaultVolume;
        // }

        // if (volumeSlider != null)
        // {
        //     volumeSlider.value = defaultVolume;
        //     volumeSlider.onValueChanged.AddListener(SetVolume);
        // }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded; 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        isMuted = PlayerPrefs.GetInt("AudioMuted", 0) == 1;
        UpdateAudioState();
        UpdateAudioButtonText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            PlayMainMenuBGM();
        }
        else if (scene.name == "Litterally Diving")
        {
            PlayGameplayBGM();
        }

        UpdateAudioButtonText();
    }

    //-------------------------------------------------------------
    //BGM Script Control
    public void PlayMainMenuBGM()
    {
        // gameplayBGM?.Stop();
        if (gameplayBGM != null)
            gameplayBGM.Stop();
        if (mainMenuBGM != null && !mainMenuBGM.isPlaying)
            mainMenuBGM.Play();
    }

    public void PlayGameplayBGM()
    {
        // mainMenuBGM?.Stop();
        if (mainMenuBGM != null)
            mainMenuBGM.Stop();
        if (gameplayBGM != null && !gameplayBGM.isPlaying)
            gameplayBGM.Play();
    }

    //----------------------------------------------------------
    //SFX Script Control
    public void PlayMenuClick()
    {
        if (buttonClickMenu != null && !isMuted)
            mainMenuSFX.PlayOneShot(buttonClickMenu);
    }

    public void PlayGameplayClick()
    {
        if (buttonClickGameplay != null && !isMuted)
            gameplaySFX.PlayOneShot(buttonClickGameplay);
    }

    public void PlayBalloonPop()
    {
        if (balloonPop != null && !isMuted)
            gameplaySFX.PlayOneShot(balloonPop);
    }

    public void PlayOxygenPickup()
    {
        if (oxygenPickup != null && !isMuted)
            gameplaySFX.PlayOneShot(oxygenPickup);
    }

    public void PlayWarningSound()
    {
        if (warningSound != null && !isMuted)
            gameplaySFX.PlayOneShot(warningSound);
    }
    public void PlayScoreSound()
    {
        if (scoreSound != null && !isMuted)
            gameplaySFX.PlayOneShot(scoreSound);
    }
    public void PlayPlayerSplash()
    {
        if (playerSplash != null && !isMuted)
            playerSFX.PlayOneShot(playerSplash);
    }
    //---------------------------------------------------------

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AudioMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateAudioState();
        UpdateAudioButtonText();
    }
    private void UpdateAudioState()
    {
        if (mainMenuBGM) mainMenuBGM.mute = isMuted;
        if (mainMenuSFX) mainMenuSFX.mute = isMuted;
        if (gameplayBGM) gameplayBGM.mute = isMuted;
        if (gameplaySFX) gameplaySFX.mute = isMuted;
        if (playerSFX) playerSFX.mute = isMuted;
    }

    private void UpdateAudioButtonText()
    {
        if (toggleAudioText != null)
            toggleAudioText.text = isMuted ? "Audio: OFF" : "Audio: ON";
    }

    public bool InstanceIsMuted()
    {
        return isMuted;
    }

    // public void SetVolume(float value)
    // {
    //     if (bgm != null)
    //     {
    //         bgm.volume = value;
    //     }
    // }
}
