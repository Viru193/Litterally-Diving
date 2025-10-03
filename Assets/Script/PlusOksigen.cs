using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlusOksigen : MonoBehaviour
{
    public GameObject bubblePrefab;
    public AudioClip pickupSound;
    public Action OnPicked;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(bubblePrefab, transform.position, Quaternion.identity);
            GameManagerr.Instance.AddOksigen(GameManagerr.Instance.maxOksigen);
            GameManagerr.Instance.SoundPlay(4);
            Destroy(gameObject);
        }

        // if (pickupSound != null)
        //     AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        // OnPicked?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
