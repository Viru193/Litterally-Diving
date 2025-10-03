using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOxygen : MonoBehaviour
{
    // public GameObject balloonPrefab;
    // bool isDead;

    // // Update is called once per frame
    // void Update()
    // {
    //     if (isDead)
    //     {
    //         GetComponent<Rigidbody2D>().velocity = new Vector2(0, Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.y, 15f, Time.deltaTime));
    //     }
    // }

    // public void DeathBegin()
    // {
    //     StartCoroutine(DeathCoroutine());
    // }

    // IEnumerator DeathCoroutine()
    // {
    //     GetComponent<PlayerController>().enabled = false;
    //     GetComponent<Animator>().Play("PlayerDeathOxygen");

    //     yield return new WaitForSeconds(.7f);

    //     isDead = true;
    //     Vector3 balloonPos1 = new Vector3(transform.position.x + .1f, transform.position.y + 0.8f, transform.position.z);
    //     Instantiate(balloonPrefab, balloonPos1, Quaternion.identity, transform);
    //     Vector3 balloonPos2 = new Vector3(transform.position.x - .1f, transform.position.y + 0.8f, transform.position.z);
    //     GameObject leftBalloon = Instantiate(balloonPrefab, balloonPos2, Quaternion.identity, transform);
    //     leftBalloon.GetComponent<SpriteRenderer>().flipX = true;

    //     yield return new WaitForSeconds(3f);

    //     GameManagerr.Instance.LoseOxygen();
    // }

    public GameObject balloonPrefab;
    [HideInInspector] public bool isDead;

    public void DeathBegin()
    {
        if (isDead) return;
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        isDead = true;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<Animator>().Play("PlayerDeathOxygen");

        yield return new WaitForSeconds(.7f);

        // Spawn balon hanya untuk efek visual
        Vector3 balloonPos1 = new Vector3(transform.position.x + .1f, transform.position.y + 0.8f, transform.position.z);
        Instantiate(balloonPrefab, balloonPos1, Quaternion.identity, transform);
        Vector3 balloonPos2 = new Vector3(transform.position.x - .1f, transform.position.y + 0.8f, transform.position.z);
        GameObject leftBalloon = Instantiate(balloonPrefab, balloonPos2, Quaternion.identity, transform);
        leftBalloon.GetComponent<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(3f);

        GameManagerr.Instance.LoseOxygen();
    }
    
    void Update()
    {
        if (isDead)
        {
            // Player mengapung ke atas jika mati
            GetComponent<Rigidbody2D>().velocity = new Vector2(
                0,
                Mathf.Lerp(GetComponent<Rigidbody2D>().velocity.y, 15f, Time.deltaTime)
            );
        }
    }
}
