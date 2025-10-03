using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float kecepatanMaks;
    public float gayaDorong;
    Rigidbody2D player;
    Animator anim;
    public float resistensiAir;

    [Header("Pengaturan Darah Dimas")]
    public int maxHealth = 3;
    public int currentHealth;
    public AudioClip damageSound;
    AudioSource audioSource;

    private float iMaju;
    private float iRotasi;
    private float originalScale;
    private float delayBubble = 3;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player.drag = resistensiAir;
        player.gravityScale = 0;
        originalScale = transform.localScale.x;

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (GameManagerr.Instance != null && GameManagerr.Instance.currentOxygen <= 0)
        {
            iMaju = 0;
            iRotasi = 0;
            anim.SetFloat("moveSpeed", 0);
            player.velocity = Vector2.zero;
            return;
        }

        iMaju = Input.GetAxis("Horizontal");

        if (player.velocity.x < 0)
        {
            transform.localScale = new Vector3(-originalScale, transform.localScale.y, transform.localScale.z);
        }
        else if (player.velocity.x > 0)
        {
            transform.localScale = new Vector3(originalScale, transform.localScale.y, transform.localScale.z);
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(iMaju));
        iRotasi = Input.GetAxis("Vertical");

        if (delayBubble > 0)
        {
            delayBubble -= Time.deltaTime;
        }
        if (delayBubble <= 0)
        {
            bubblePrefab.SetActive(false);
            delayBubble = UnityEngine.Random.Range(3f, 5f);
            bubblePrefab.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector2 arah = new Vector2(iMaju, iRotasi).normalized;

        player.AddForce(arah * gayaDorong, ForceMode2D.Force);

        if (player.velocity.magnitude > kecepatanMaks)
        {
            player.velocity = player.velocity.normalized * kecepatanMaks;
        }

        player.velocity = new Vector2(player.velocity.x, player.velocity.y * .95f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (damageSound != null && audioSource != null)
            audioSource.PlayOneShot(damageSound);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player mati karena diserang ikan!");
        anim.SetFloat("moveSpeed", 0);
        player.velocity = Vector2.zero;

        if (GameManagerr.Instance != null)
        {
            GameManagerr.Instance.LoseFish();
        }
    }
}
