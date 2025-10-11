using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPoint : MonoBehaviour
{
    public GameObject balloonPrefab;
    public int pointValue = 1;
    SpriteRenderer sr;
    Rigidbody2D rb;

    public bool balloonMode;
    private bool isInside;
    public GameObject theBalloon; 
    public List<GameObject> draggedFish;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isInside)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!balloonMode)
                {
                    balloonMode = true;
                    GameManagerr.Instance.SoundPlay(1);
                    Vector3 balloonPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
                    theBalloon = Instantiate(balloonPrefab, balloonPos, Quaternion.identity, transform);
                }
                else
                {
                    balloonMode = false;
                    GameManagerr.Instance.SoundPlay(0);
                    Destroy(theBalloon);
                    foreach (GameObject fish in draggedFish)
                    {
                        if (fish.GetComponent<FishController>())
                        {
                            fish.GetComponent<Rigidbody2D>().simulated = true;
                            fish.GetComponent<FishController>().enabled = true;
                            fish.transform.parent = null;
                        }
                    }
                    draggedFish.Clear();
                    GameManagerr.Instance.ShowFishWarning(false);
                }
            }
        }

        if (balloonMode)
        {
            //rb.velocity = new Vector2(rb.velocity.x, Mathf.MoveTowards(rb.velocity.y, 30f, 15f * Time.deltaTime));
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, 15f, 1f * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (balloonMode) 
        {
            if (other.CompareTag("ScoreTrigger"))
            {
                if (draggedFish.Count > 0)
                {
                    GameManagerr.Instance.LoseFish();
                }
                else 
                {
                    GameManagerr.Instance.SoundPlay(6);
                    GameManagerr.Instance.AddScore(pointValue);
                    GameManagerr.Instance.sampahCount--;
                    Destroy(gameObject);
                }
            }

            if (other.CompareTag("Whale"))
            {
                balloonMode = false;
                GameManagerr.Instance.SoundPlay(0);
                Destroy(theBalloon);
                foreach (GameObject fish in draggedFish)
                {
                    if (fish.GetComponent<FishController>())
                    {
                        fish.GetComponent<Rigidbody2D>().simulated = true;
                        fish.GetComponent<FishController>().enabled = true;
                        fish.transform.parent = null;
                    }
                }
            }

            if (other.CompareTag("Fish"))
            {
                GameManagerr.Instance.SoundPlay(5);
                draggedFish.Add(other.gameObject);
                GameManagerr.Instance.ShowFishWarning(true);
                other.GetComponent<FishController>().Captured(transform);
                other.GetComponent<FishController>().enabled = false;
                other.GetComponent<Rigidbody2D>().simulated = false;
            }
        }

        if (other.CompareTag("Player"))
        {
            sr.color = new Color(1f, 0.5f, 0.5f);
            isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            sr.color = Color.white;
            isInside = false;
        }
    }
}
