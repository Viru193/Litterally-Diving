using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerr : MonoBehaviour
{
    public static GameManagerr Instance;
    public int currentScore;
    public Text scoreText;
    public GameObject loseFishUI, loseOxygenUI, loseSailfishUI;
    public GameObject cameraObj;
    public AudioSource[] soundEffects;

    [HideInInspector]
    public int sampahCount;
    public int sampahMax;
    private int sampahMax2;
    [HideInInspector]
    public int whaleCount;
    public int whaleMax;
    [HideInInspector]
    public int fishCountL, fishCountR;
    public int fishMax;
    private int fishMax2;
    [HideInInspector]
    public int alifCount;
    public int alifMax;
    [HideInInspector]
    public int pufferfishCount;
    public int pufferfishMax;
    [HideInInspector]
    public int dangerFishCount;
    public int dangerFishMax;
    [HideInInspector]
    public int tankCount;
    public int tankMax;

    // public int currentOksigen;
    public int maxOksigen = 100;
    public Slider oxygenSlider;
    public float currentOxygen;

    // Peringatan Oksigen Berkurang
    public Image oFillImage;
    public Color normalColor;
    public Color warningColor = Color.red;
    public float flashSpeed = 4f;
    public float warningThreshold = 20f;

    //Warning
    public GameObject OxygenWarningPanel;
    public float oxygenWarningDuration = 2f;
    private bool isOxygenWarningActive;
    private Coroutine oxygenWarningCoroutine;

    //Radar
    public GameObject FishWarningPanel;
    public float warningDuration = 3f;
    public Coroutine warningCoroutine;

    private bool isFlashing;

    public GameObject player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        sampahMax2 = sampahMax * 3;
        fishMax2 = fishMax * 3;

        currentOxygen = maxOksigen;
        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = maxOksigen;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        if (sampahCount < 0)
        {
            sampahCount = 0;
        }
        if (whaleCount < 0)
        {
            whaleCount = 0;
        }
        if (fishCountL < 0)
        {
            fishCountL = 0;
        }
        if (fishCountR < 0)
        {
            fishCountR = 0;
        }
        if (dangerFishCount < 0)
        {
            dangerFishCount = 0;
        }
        if (tankCount < 0)
        {
            tankMax = 0;
        }

        // Script (Update) Score
        if (currentScore >= 500)
        {
            sampahMax = sampahMax2;
            fishMax = fishMax2;
        }

        // Script (Update) Oksigen
        if (currentScore > 0)
        {
            currentOxygen -= Time.deltaTime * 3;
        }
        
        if (oxygenSlider != null)
        {
            oxygenSlider.value = (int)currentOxygen;
        }

        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            if (player)
            {
                DeathOxygen deathScript = player.GetComponent<DeathOxygen>();
                deathScript.DeathBegin();
            }
        }

        if (currentOxygen <= warningThreshold)
        {
            if (isFlashing)
                isFlashing = true;

            if (oFillImage != null)
            {
                float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
                oFillImage.color = Color.Lerp(normalColor, warningColor, t);
            }

            if (!isOxygenWarningActive && currentOxygen > 0)
            {
                ShowOxygenWarning(true);
            }
        }
        else
        {
            if (isFlashing)
            {
                isFlashing = false;

                if (oFillImage != null)
                    oFillImage.color = normalColor;
            }
        }
    }

    public void SoundPlay(int soundToPlay)
    {
        if (BGMManager.Instance == null) return;

        switch (soundToPlay)
        {
            case 0:
                BGMManager.Instance.PlayBalloonPop();
                break;
            case 1:
                BGMManager.Instance.PlayOxygenPickup();
                break;
            case 2:
                BGMManager.Instance.PlayGameplayClick();
                break;
            case 3:
                BGMManager.Instance.PlayWarningSound();
                break;
            case 4:
                BGMManager.Instance.PlayScoreSound();
                break;
            case 5:
                BGMManager.Instance.PlayPlayerSplash();
                break;
            case 6:
                BGMManager.Instance.PlayWhale();
                break;
            default:
                Debug.LogWarning("Sound index tidak dikenal di GameManagerr: " + soundToPlay);
                break;
        }
        // soundEffects[soundToPlay].Stop();

        // soundEffects[soundToPlay].Play();

    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = " " + currentScore;
        }
    }

    public void SaveHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }

    // Script Oksigen
    public void AddOksigen(int amount)
    {
        oFillImage.color = normalColor;
        currentOxygen += amount;

        currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOksigen);

        if (oxygenSlider != null)
            oxygenSlider.value = currentOxygen;
    }

    public void ShowOxygenWarning(bool state)
    {
        if (OxygenWarningPanel == null) return;

        if (state)
        {
            SoundPlay(7);
            OxygenWarningPanel.SetActive(true);
            isOxygenWarningActive = true;

            if (oxygenWarningCoroutine != null)
                StopCoroutine(oxygenWarningCoroutine);
            oxygenWarningCoroutine = StartCoroutine(HideOxygenWarningAfterDelay());
        }
        else
        {
            OxygenWarningPanel.SetActive(false);
            isOxygenWarningActive = false;
        }
    }

    private IEnumerator HideOxygenWarningAfterDelay()
    {
        yield return new WaitForSeconds(oxygenWarningDuration);
        if (OxygenWarningPanel != null)
            OxygenWarningPanel.SetActive(false);
        isOxygenWarningActive = false;
    }
    // Akhir Script Oksigen

    //Radar
    public void ShowFishWarning(bool state)
    {
        if (FishWarningPanel == null) return;
        if (state)
        {
            FishWarningPanel.SetActive(true);
            if (warningCoroutine != null)
                StopCoroutine(warningCoroutine);
            warningCoroutine = StartCoroutine(HideFishWarningAfterDelay());
        }
        else
        {
            FishWarningPanel.SetActive(false);
        }
    }

    private IEnumerator HideFishWarningAfterDelay()
    {
        yield return new WaitForSeconds(warningDuration);
        FishWarningPanel.SetActive(false);
    }

    public void PauseGame()
    {
        SoundPlay(2);
        Time.timeScale = 0f;
        player.GetComponent<PlayerController>().enabled = false;
    }

    public void ResumeGame()
    {
        SoundPlay(2);
        Time.timeScale = 1f;
        player.GetComponent<PlayerController>().enabled = true;
    }

    public void LoseFish()
    {
        // BGMManager.Instance.PlayGeneralWarningSFX();
        BGMManager.Instance.PlayGameOverSFX();
        SoundPlay(3);
        Time.timeScale = 0f;
        loseFishUI.SetActive(true);
    }

    public void LoseOxygen()
    {
        BGMManager.Instance.PlayGameOverSFX();
        SoundPlay(3);
        Time.timeScale = 0f;
        loseOxygenUI.SetActive(true);
    }

    public void LoseSailfish()
    {
        SoundPlay(3);
        Time.timeScale = 0f;
        loseSailfishUI.SetActive(true);
    }

    public void ToMainMenu()
    {
        SaveHighScore(currentScore);
        SceneManager.LoadScene("Main Menu");
    }
}
