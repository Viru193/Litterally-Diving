using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkEffect : MonoBehaviour
{
    public float fadeInSpeed = 2f;
    public float fadeOutSpeed = 2f;
    public float displayTime = 3f;

    private Image inkImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void Awake()
    {
        inkImage = GetComponent<Image>();
        Color c = inkImage.color;
        c.a = 0f;
        inkImage.color = c;
    }

    void OnEnable()
    {
        StartCoroutine(InkRoutine());
    }

    IEnumerator InkRoutine()
    {
        // Fade in
        while (inkImage.color.a < 0.8f)
        {
            Color c = inkImage.color;
            c.a += Time.deltaTime * fadeInSpeed;
            inkImage.color = c;
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Fade out
        while (inkImage.color.a > 0f)
        {
            Color c = inkImage.color;
            c.a -= Time.deltaTime * fadeOutSpeed;
            inkImage.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }
}
