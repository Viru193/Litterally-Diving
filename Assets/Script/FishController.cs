using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    float currentSpeed;
    float originalSpeed;
    public bool isWhale;
    public bool isFishL, isFishR;
    public bool isShark;
    public bool isDangerFish;

    private float delayTimer;

    private GameObject theTrash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        delayTimer = 2;
        originalSpeed = speed;

        if (isFishL && transform.position.x > 0)
        {
            isFishL = false;
            isFishR = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (transform.localScale.x < 0)
        {
            originalSpeed = -speed;
        }

        currentSpeed = originalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
        }
        else if (delayTimer <= 0)
        {
            delayTimer = Random.Range(3f, 7f);
            currentSpeed = Random.Range(originalSpeed/2f, originalSpeed*1.5f);
        }

        rb.velocity = new Vector2(currentSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            if (isWhale)
            {
                GameManagerr.Instance.whaleCount--;
            }
            else if (isFishL)
            {
                GameManagerr.Instance.fishCountL--;
            }
            else if (isFishR)
            {
                GameManagerr.Instance.fishCountR--;
            }

            Destroy(gameObject);
        }

        if (other.CompareTag("ScoreTrigger"))
        {
            if (!isDangerFish)
            {
                GameManagerr.Instance.LoseFish();
            }
        }
    }
}
