using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip gameplayMusik;

    public Button toggleButton;
    public Text buttonText;

    private bool isBgmOn = true;

    public static BGMManager Instance;
    // public Slider volumeSlider;
    // private float defaultVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
    }

    // public void SetVolume(float value)
    // {
    //     if (bgm != null)
    //     {
    //         bgm.volume = value;
    //     }
    // }
}
