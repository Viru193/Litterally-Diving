using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    public Sprite[] frames;
    SpriteRenderer sr;

    private int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        if (frames.Length > 0)
        {
            sr.sprite = frames[currentFrame];
            yield return new WaitForSeconds(0.5f);
            currentFrame += 1;
            if (currentFrame > (frames.Length - 1))
            {
                currentFrame = 0;
            }
            StartCoroutine(Anim());
        }
    }
}
