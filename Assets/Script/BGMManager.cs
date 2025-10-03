using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public AudioSource bgm;
    public AudioClip gameplayMusik;

    public static BGMManager Instance;
    public Slider volumeSlider;
    private float defaultVolume = 0.5f;


    // Start is called before the first frame update
    void Start()
    {

        if (bgm != null)
        {
            bgm.volume = defaultVolume;
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = defaultVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
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

    public void SetVolume(float value)
    {
        if (bgm != null)
        {
            bgm.volume = value;
        }
    }
}
