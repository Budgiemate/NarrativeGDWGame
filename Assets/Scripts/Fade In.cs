using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Image fadeInBox;
    public GameObject mainCam;
    void Start()
    {
        fadeInBox = GetComponent<Image>();
        StartCoroutine(StartFadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCam.GetComponent<CameraScript>().isShooting)
        {
            gameObject.SetActive(false);
        }
    }
    public IEnumerator StartFadeIn()
    {
        float fadeInVal = 1;
        Color fadeInColor = fadeInBox.color;
        while (fadeInVal > 0)
        {
            fadeInVal = fadeInVal - 0.01f;
            fadeInColor.a = fadeInVal;
            fadeInBox.color = fadeInColor;
            yield return new WaitForSeconds(0.01f);
        }
        fadeInColor.a = fadeInVal;
        yield return null;
    }
}
