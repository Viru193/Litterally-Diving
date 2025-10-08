using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DangerFish : MonoBehaviour
{
    public enum FishType
    {
        BalloonBreaker, Sailfish, PufferFish, Squid
    }
    public FishType fishType = FishType.BalloonBreaker;
    public string balloonTag = "Balloon";
    // public GameObject popEffect; // Efek partikel/ letusan balon
    // public AudioClip popSound; // suara balon
    public string popTriggerName = "Pop";

    [Header("Serang Dimas")]
    public int damageToPlayer = 100;
    public string attackTrigger = "Attack";
    // public float attackCooldown = 1.5f;
    // public float chaseRange = 5f;
    public float moveSpeed = 3f;

    [Header("Alif Mengembang")]
    public float detectRange = 2f;
    public float inflateDelay = 0.5f;
    public string inflateTrigger = "Inflate";

    [Header("Davin Menyemburkan")]
    public float inkRange = 3f;
    public GameObject inkEffectPrefab;
    public float inkDuration = 3f;

    // private GameObject currentBalloon;
    private TrashPoint attachedTrashPoint;
    private FishController fishController;
    private Rigidbody2D rb;
    private Animator animator;
    // private AudioSource audioSource;
    private Transform player;

    private bool hasKilledPlayer = false;
    private bool isInflating = false;
    private bool hasInked = false;

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
        fishController = GetComponent<FishController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;

        if (fishType == FishType.Sailfish)
        {
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.isTrigger = true; 
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (fishType == FishType.PufferFish)
        {
            DetectAndInflate();
        }

        if (fishType == FishType.Squid)
            DetectAndInk();

        if (fishType == FishType.Sailfish && !hasKilledPlayer && mainCam != null) 
        {

            Vector3 viewPos = mainCam.WorldToViewportPoint(transform.position);
            if (viewPos.x < -1.5f)
                transform.position = new Vector3(Screen.width + 1f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
         Debug.Log("DangerFish menabrak: " + other.name + " (tag: " + other.tag + ")");

        // Jika mengenai balon
        if ((fishType == FishType.BalloonBreaker || fishType == FishType.Sailfish) && other.CompareTag(balloonTag))
        {
            attachedTrashPoint = other.GetComponentInParent<TrashPoint>();
            if (attachedTrashPoint != null)
                PopBalloon();
        }
        
        if (fishType == FishType.Sailfish && (other.CompareTag("WallL") || other.CompareTag("WallR")))
            return;

        Debug.Log("DangerFish menabrak: " + other.name + " (tag: " + other.tag + ")");

        if (fishType == FishType.Sailfish && other.CompareTag("Player") && !hasKilledPlayer)
        {
            hasKilledPlayer = true; rb.velocity = Vector2.zero;
            if (fishController != null)
                fishController.enabled = false;
            // animasi serang 
            if (animator != null && !string.IsNullOrEmpty(attackTrigger))
                animator.SetTrigger(attackTrigger);
            // bunuh player 
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null)
                pc.TakeDamage(damageToPlayer);
            // panggil game over 
            GameManagerr gm = FindObjectOfType<GameManagerr>();
            if (gm != null)
                gm.LoseFish();
            Debug.Log("Sailfish menyerang player → Game Over!");
        }
    }

    void DetectAndInk() 
    {
        if (player == null || hasInked || inkEffectPrefab == null) return;
        float dist = Vector2.Distance(transform.position, player.position);
        Debug.Log("Cek jarak Squid ke Player: " + dist);
        if (dist <= inkRange)
        {
            hasInked = true;
            Debug.Log("🦑 Squid menyemprot tinta!");
            StartCoroutine(SprayInk());
        }
    }

    void DetectAndInflate()
    {
        if (isInflating) return;

        GameObject[] balloons = GameObject.FindGameObjectsWithTag(balloonTag);
        foreach (GameObject balloon in balloons)
        {
            float dist = Vector2.Distance(transform.position, balloon.transform.position);
            if (dist <= detectRange)
            {
                StartCoroutine(InflateAndPop(balloon));
                break;
            }
        }
    }

    IEnumerator SprayInk()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null && inkEffectPrefab != null)
        {
            GameObject ink = Instantiate(inkEffectPrefab, canvas.transform);
            yield return new WaitForSeconds(inkDuration + 1f);
            Destroy(ink);
        }
        hasInked = false;
    }

    IEnumerator InflateAndPop(GameObject balloon)
    {
        isInflating = true;

        if (animator != null && !string.IsNullOrEmpty(inflateTrigger))
            animator.SetTrigger(inflateTrigger);

        yield return new WaitForSeconds(inflateDelay);

        if (balloon != null)
        {
            TrashPoint tp = balloon.GetComponentInParent<TrashPoint>();
            if (tp != null)
            {
                tp.balloonMode = false;
                Destroy(tp.theBalloon);

                Rigidbody2D trashRb = tp.GetComponent<Rigidbody2D>();
                if (trashRb != null)
                    trashRb.velocity = Vector2.down * 2f;

                tp.draggedFish = null;
                tp.SendMessage("ReleaseFish", SendMessageOptions.DontRequireReceiver);
            }

            Debug.Log("PufferFish meletuskan balon + trash jatuh ke bawah!");
        }

        yield return new WaitForSeconds(1f);
        isInflating = false;
    }

    // void ChasePlayer()
    // {
    //     Vector2 direction = (player.position - transform.position).normalized;
    //     rb.velocity = direction * moveSpeed;

    //     if (direction.x > 0)
    //         transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //     else if (direction.x < 0)
    //         transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    //     if (animator != null)
    //         animator.SetBool("isChasing", true);

    // }

    void PopBalloon()
    {
        if (attachedTrashPoint == null) return;
        attachedTrashPoint.balloonMode = false;

        if (attachedTrashPoint.theBalloon != null)
        {
            Destroy(attachedTrashPoint.theBalloon);
            // attachedTrashPoint.theBalloon = null;
        }

        if (fishType == FishType.PufferFish)
        {
            Rigidbody2D trashRb = attachedTrashPoint.GetComponent<Rigidbody2D>();
            if (trashRb != null)
            {
                trashRb.velocity = Vector2.down * 2f;
            }
        }
        else
        {
            if (fishController != null && rb != null)
            {
                fishController.enabled = true;
                float velocityX = fishController.speed;

                if (fishController.isFishL)
                    velocityX = -Mathf.Abs(fishController.speed);
                else if (fishController.isFishR)
                    velocityX = Mathf.Abs(fishController.speed);

                rb.velocity = new Vector2(velocityX, 0f);
            }
        }

        if (animator != null && !string.IsNullOrEmpty(popTriggerName))
        {
            animator.SetTrigger(popTriggerName);
        }

        // Efek partikel
        // if (popEffect != null)
        //     Instantiate(popEffect, transform.position, Quaternion.identity);

        // Efek suara
        // if (popSound != null)
        // {
        //     if (audioSource != null)
        //         audioSource.PlayOneShot(popSound);
        //     else
        //         AudioSource.PlayClipAtPoint(popSound, transform.position);
        // }

        attachedTrashPoint.draggedFish = null;
        attachedTrashPoint.SendMessage("ReleaseFish", SendMessageOptions.DontRequireReceiver);
        Debug.Log("Balon dipecahkan oleh DangerFish!");
    }
}

//tambahkan animasinya
