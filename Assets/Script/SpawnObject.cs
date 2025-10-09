using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objectPrefab;
    //public Transform player;
    //public float spawnRadius;
    public float timer = 3;

    public Vector2 maxSpawnPos, minSpawnPos;
    public bool sampahMode, fishModeL, fishModeR, whaleMode, sharkMode, oxygenMode, dangerFishMode;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (sampahMode)
        {
            StartCoroutine(SpawningSampah());
        }
        if (fishModeL)
        {
            StartCoroutine(SpawningFishL());
        }
        if (fishModeR)
        {
            StartCoroutine(SpawningFishR());
        }
        if (whaleMode)
        {
            StartCoroutine(SpawningWhale());
        }
        if (oxygenMode)
        {
            StartCoroutine(SpawningOxygen());
        }

        if (dangerFishMode)
    {
        StartCoroutine(SpawningDangerFish());
    }
    }

    IEnumerator SpawningSampah()
    {
        while (true)
        {
            if (GameManagerr.Instance.sampahCount < GameManagerr.Instance.sampahMax)
            {
                SpawningObject();
                GameManagerr.Instance.sampahCount++;
            }
            yield return new WaitForSeconds(Random.Range(timer, (timer + 2)));
        }
    }

    IEnumerator SpawningFishL()
    {
        while (true)
        {
            if (GameManagerr.Instance.fishCountL < GameManagerr.Instance.fishMax)
            {
                SpawningObject();
                GameManagerr.Instance.fishCountL++;
            }
            yield return new WaitForSeconds(Random.Range(timer, (timer + 2)));
        }
    }

    IEnumerator SpawningFishR()
    {
        while (true)
        {
            if (GameManagerr.Instance.fishCountR < GameManagerr.Instance.fishMax)
            {
                SpawningObject();
                GameManagerr.Instance.fishCountR++;
            }
            yield return new WaitForSeconds(Random.Range(timer, (timer + 2)));
        }
    }

    IEnumerator SpawningWhale()
    {
        while (true)
        {
            if (GameManagerr.Instance.whaleCount < GameManagerr.Instance.whaleMax)
            {
                SpawningObject();
                GameManagerr.Instance.whaleCount++;
            }
            yield return new WaitForSeconds(Random.Range(timer, (timer + 2)));
        }
    }

    IEnumerator SpawningOxygen()
    {
        while (true)
        {
            if (GameManagerr.Instance.whaleCount < GameManagerr.Instance.tankMax)
            {
                SpawningObject();
                GameManagerr.Instance.tankCount++;
            }
            yield return new WaitForSeconds(Random.Range(timer, (timer + 2)));
        }
    }

    IEnumerator SpawningDangerFish()
    {
        while (true)
        {
            if (GameManagerr.Instance.dangerFishCount < GameManagerr.Instance.dangerFishMax)
            {
                SpawningObject();
                GameManagerr.Instance.dangerFishCount++;
            }
            yield return new WaitForSeconds(Random.Range(timer, timer + 2));
        }
    }

    void SpawningObject()
    {
        if (objectPrefab == null) return;

        //Vector2 randomDirection = Random.insideUnitCircle.normalized;
        //Vector3 spwanPos = player.position + new Vector3(randomDirection.x, randomDirection.y, 0) * spawnRadius;

        Vector2 randomPos = new Vector2(Random.Range(-minSpawnPos.x, maxSpawnPos.x), Random.Range(-minSpawnPos.y, maxSpawnPos.y));
        Vector3 spawnPos = transform.position + new Vector3(randomPos.x, randomPos.y, 0);

        Instantiate(objectPrefab[Random.Range(0, objectPrefab.Length)], spawnPos, Quaternion.identity);
    }
}
