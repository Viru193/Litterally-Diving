using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnBecameVisible()
    {
        if (!hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true;
        }
    }
}
