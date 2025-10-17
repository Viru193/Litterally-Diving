using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip gameplayMusik;

    public Button toggleButton;
    public Text buttonText;
=======
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    
    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource playerSFX;

    [Header("BGM Clips")]
    public AudioClip menuBGM;
    public AudioClip gameplayBGM;

    [Header("SFX Clips")]
    public AudioClip buttonClickMenu;
    public AudioClip buttonClickGameplay;
    public AudioClip balloonPop;
    public AudioClip oxygenPickup;
    public AudioClip warningSound;
    public AudioClip scoreSound;
    public AudioClip playerSplash;
    public AudioClip oxygenWarningSFX;
    public AudioClip gameOverSFX;

    [Header("SFX Dicky")]
    public AudioClip sfxSquidMove;
    public AudioClip sfxWhale;

    [Header("UI Control (Optional)")]
    public Button audioButton;
    public TextMeshProUGUI audioButtonText;
>>>>>>> Stashed changes

    private bool isBgmOn = true;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        if (bgm != null)
            bgm = GetComponent<AudioSource>();
        isBgmOn = PlayerPrefs.GetInt("BGMOn", 1) == 1;
        ApplyBGMState();
        // {
        //     bgm.volume = defaultVolume;
        // }

        // if (volumeSlider != null)
        // {
        //     volumeSlider.value = defaultVolume;
        //     volumeSlider.onValueChanged.AddListener(SetVolume);
        // }

        if (isBgmOn && gameplayMusik != null && !bgm.isPlaying)
        {
            bgm.clip = gameplayMusik;
            bgm.loop = true;
            bgm.Play();
        }

        if (toggleButton != null)
            toggleButton.onClick.AddListener(ToggleBGM);
=======
        
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
            SceneManager.sceneLoaded += OnSceneLoaded;
>>>>>>> Stashed changes
        }
        else if (Instance != this)
        {
            Debug.LogWarning("Duplikat BGMManager ditemukan, menghapus yang baru!");
            Destroy(gameObject);
        }
    }

<<<<<<< Updated upstream
    private void ApplyBGMState()
    {
        if (bgm != null)
            bgm.mute = !isBgmOn;

        if (buttonText != null)
            buttonText.text = isBgmOn ? "BGM: ON" : "BGM: OFF";
    }

    public void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        ApplyBGMState();

        PlayerPrefs.SetInt("BGMOn", isBgmOn ? 1 : 0);
        PlayerPrefs.Save();
=======
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
            PlayBGM(menuBGM);
        else if (scene.name == "Litterally Diving")
            PlayBGM(gameplayBGM);

        StartCoroutine(SetupAudioButtonCoroutine(scene));
    }
    
    private IEnumerator SetupAudioButtonCoroutine(Scene scene)
    {
        float timer = 0f;
    float maxWait = 1f;
    audioButton = null;

    while (audioButton == null && timer < maxWait)
    {
        string[] buttonNames = { "On/Off", "AudioButton", "SoundToggle" };
        foreach (var name in buttonNames)
        {
            GameObject btnObj = GameObject.Find(name);
            if (btnObj != null)
            {
                audioButton = btnObj.GetComponent<Button>();
                audioButtonText = audioButton.GetComponentInChildren<TextMeshProUGUI>();
                audioButton.onClick.RemoveAllListeners();
                audioButton.onClick.AddListener(ToggleAudio);

                UpdateAudioState();
                UpdateAudioButtonText();

                Debug.Log($"🔊 Audio button listener terpasang di scene: {scene.name}");
                break;
            }
        }

        if (audioButton == null)
        {
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    if (audioButton == null)
        Debug.LogWarning($"Tombol audio tidak ditemukan di scene: {scene.name}");
    }

    private void TryReconnectButton()
    {
         if (audioButton != null) return;

    string[] buttonNames = { "On/Off", "AudioButton", "SoundToggle" };
    foreach (string name in buttonNames)
    {
        GameObject btnObj = GameObject.Find(name);
        if (btnObj != null)
        {
            audioButton = btnObj.GetComponent<Button>();
            if (audioButton != null)
            {
                audioButton.onClick.RemoveAllListeners();
                audioButton.onClick.AddListener(ToggleAudio);

                audioButtonText = audioButton.GetComponentInChildren<TextMeshProUGUI>();

                UpdateAudioState();
                UpdateAudioButtonText();

                Debug.Log("Audio button reconnected and synced!");
                break;
            }
        }
    }
    }
    
    public void RefreshUIButton()
    {
        StartCoroutine(RefreshUIButtonCoroutine());
    }

    private IEnumerator RefreshUIButtonCoroutine()
    {
        yield return null;

        GameObject btnObj = GameObject.Find("On/Off");
        if (btnObj != null)
        {
            audioButton = btnObj.GetComponent<Button>();
            if (audioButton != null)
            {
                audioButton.onClick.RemoveAllListeners();
                audioButton.onClick.AddListener(ToggleAudio);

                audioButtonText = audioButton.GetComponentInChildren<TextMeshProUGUI>();
                UpdateAudioButtonText();

                Debug.Log("Audio button ditemukan ulang di Pause Menu");
            }
        }
        else
        {
            Debug.LogWarning("Tidak menemukan tombol On/Off saat refresh pause menu");
        }
    }

    //-------------------------------------------------------------
    //BGM Script Control
    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource == null || clip == null)
    {
        Debug.LogWarning("BGMManager: AudioSource atau Clip tidak ditemukan!");
        return;
    }
    if (!bgmSource.enabled || !bgmSource.gameObject.activeInHierarchy)
    {
        Debug.LogWarning("BGMManager: AudioSource sedang tidak aktif, diaktifkan ulang.");
        bgmSource.enabled = true;
        bgmSource.gameObject.SetActive(true);
    }

        Debug.Log($"🎵 Memutar BGM: {clip.name}");

        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.mute = isMuted;
        bgmSource.Play();

        UpdateAudioButtonText();
    }

    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    //----------------------------------------------------------
    //SFX Script Control
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null || isMuted) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMenuClick() => PlaySFX(buttonClickMenu);
    public void PlayGameplayClick() => PlaySFX(buttonClickGameplay);
    public void PlayBalloonPop()    => PlaySFX(balloonPop);
    public void PlayOxygenPickup()  => PlaySFX(oxygenPickup);
    public void PlayWarningSound()  => PlaySFX(warningSound);
    public void PlayScoreSound()    => PlaySFX(scoreSound);
    public void PlayOxygenWarningSFX()  => PlaySFX(oxygenWarningSFX);
    public void PlayGameOverSFX()   => PlaySFX(gameOverSFX);

    public void PlayPlayerSplash()
    {
        if (playerSFX != null && playerSplash != null && !isMuted)
            playerSFX.PlayOneShot(playerSplash);
    }
    //---------------------------------------------------------

    public void PlaySquidMove()
    {
        if (sfxSquidMove != null)
            sfxSource.PlayOneShot(sfxSquidMove);
    }

    public void PlayWhale()
    {
        if (sfxSource == null || sfxWhale == null || isMuted) return;
        sfxSource.PlayOneShot(sfxWhale);
    }

    public void ToggleAudio()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("AudioMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateAudioState();
        UpdateAudioButtonText();
        Debug.Log($"ToggleAudio dipanggil! Status sekarang: {(isMuted ? "OFF" : "ON")}");
    }

    private void UpdateAudioState()
    {
        if (bgmSource) bgmSource.mute = isMuted;
        if (sfxSource) sfxSource.mute = isMuted;
        if (playerSFX) playerSFX.mute = isMuted;
    }

    private void UpdateAudioButtonText()
    {
        if (audioButtonText != null)
            audioButtonText.text = isMuted ? "Audio: OFF" : "Audio: ON";
    }

    public bool InstanceIsMuted() => isMuted;

    void OnEnable()
    {
        UpdateAudioButtonText();
>>>>>>> Stashed changes
    }
}
