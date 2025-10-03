using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCondition : MonoBehaviour
{
    public int conditionalScoreSpawner = 0;

    void Update()
    {
        if (GameManagerr.Instance.currentScore >= conditionalScoreSpawner)
        {
            GetComponent<SpawnObject>().enabled = true;
        }
    }
}
