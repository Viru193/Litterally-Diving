using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRadar : MonoBehaviour
{
    public Transform player;
    public Slider radarSlider;

    public bool autoDetectRange = true;
    public float worldMinX = -10f;
    public float worldMaxX = 10f;

    public string dangerTag = "Fish";
    public RectTransform iconParent;
    public GameObject dangerIconPrefab;

    private List<GameObject> spawnedIcons = new List<GameObject>();

    void Start()
    {
        if (autoDetectRange)
        {
            Camera cam = Camera.main;
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            worldMinX = cam.transform.position.x - (camWidth / 2f);
            worldMaxX = cam.transform.position.x + (camWidth / 2f);
        }

        // UpdateSlider();
        UpdatePlayerRadar();
    }

    void Update()
    {
        // UpdateSlider();
        UpdatePlayerRadar();
        UpdateDangerIcons();
    }

    // private void UpdateSlider()
    // {
    //     if (player == null || radarSlider == null) return;
    //     float playerX = player.position.x;
    //     float normalized = Mathf.InverseLerp(worldMinX, worldMaxX, playerX);
    //     radarSlider.value = Mathf.Clamp01(normalized);

    //     // Debug.Log($"PlayerX: {player}, Min: {worldMinX}, Max: {worldMaxX}, Normalized: {normalized}");
    // }

    void UpdatePlayerRadar()
    {
        if (player == null || radarSlider == null) return;
        float normalized = Mathf.InverseLerp(worldMinX, worldMaxX, player.position.x);
        radarSlider.value = normalized;
    }

    void UpdateDangerIcons()
    {
        if (iconParent == null || dangerIconPrefab == null) return;
        foreach (var icon in spawnedIcons)
        {
            Destroy(icon);
        }
        spawnedIcons.Clear();

        GameObject[] dangers = GameObject.FindGameObjectsWithTag(dangerTag);
        float sliderWidth = iconParent.rect.width;
        foreach (GameObject danger in dangers)
        {
            float normalized = Mathf.InverseLerp(worldMinX, worldMaxX, danger.transform.position.x);
            normalized = Mathf.Clamp01(normalized);
            GameObject icon = Instantiate(dangerIconPrefab, iconParent);
            RectTransform rt = icon.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(normalized * sliderWidth, 0);
            spawnedIcons.Add(icon);

            StartCoroutine(BlinkIcon(icon));
        }
    }

    private IEnumerator BlinkIcon(GameObject icon)
    {
        CanvasGroup cg = icon.GetComponent<CanvasGroup>();
        if (cg == null) cg = icon.AddComponent<CanvasGroup>();

        float speed = 4f; // kecepatan kedip
        while (icon != null)
        {
            float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // 0-1 oscillate
            cg.alpha = alpha;
            yield return null;
        }
    }
}
