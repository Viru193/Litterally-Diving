using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float minPos, maxPos;
    public Transform farBackground, middleBackground, middleBackground2;

    private Vector2 lastPos;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBackground.position += new Vector3(amountToMove.x * .8f, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x * .65f, amountToMove.y, 0f);
        middleBackground2.position += new Vector3(amountToMove.x * .4f, amountToMove.y, 0f);

        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float xPos = Mathf.Clamp(transform.position.x, minPos, maxPos);
            Vector3 camPos = new Vector3(xPos, transform.position.y, transform.position.z);
            Vector3 desiredPos = player.transform.position;
            Vector3 smoothedPos = Vector3.Lerp(camPos, desiredPos, 0.125f);

            transform.position = new Vector3(smoothedPos.x, transform.position.y, transform.position.z);
        }
    }
}
