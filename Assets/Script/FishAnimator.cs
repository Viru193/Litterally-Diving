using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimator : MonoBehaviour
{
    public Sprite frame1, frame2; 
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        if (frame1 && frame2)
        {
            sr.sprite = frame1;
            yield return new WaitForSeconds(0.5f);
            sr.sprite = frame2;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Anim());
        }
    }
}
