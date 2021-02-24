using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Image fadeImg;
    public float fadingTime;
    private Color unfadedColor = new Color();
    private Color fadedColor = new Color();

    public bool isFadedOut = false;
    private void Start()
    {
        fadedColor = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1f);
        unfadedColor = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 0f);
    }
    public void FadeScreen()
    {
        StartCoroutine(FadeRoutine(true));
    }

    public void UnFadeScreen()
    {
        StartCoroutine(FadeRoutine(false));
    }

    private IEnumerator FadeRoutine(bool fade)
    {
        isFadedOut = false;
        float timeLeft = fadingTime;
        while (timeLeft > Time.deltaTime)
        {
            if (fade)
                fadeImg.color = Color.Lerp(fadeImg.color, fadedColor, Time.deltaTime / timeLeft);
            else
                fadeImg.color = Color.Lerp(fadeImg.color, unfadedColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
        }
        isFadedOut = true;
    }
}
