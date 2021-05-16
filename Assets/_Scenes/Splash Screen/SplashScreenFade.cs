using Zenject;
using System.Collections;
using UnityEngine;

public class SplashScreenFade : MonoBehaviour
{
    LevelHandler _levelHandler;

    [Inject]
    public void Construct(LevelHandler levelHandler)
    {
        _levelHandler = levelHandler;
    }

    private SpriteRenderer myLogoRenderer;
    public float fadeOutTime = 10;
    public float fadeOutValue = -1;

    void Awake()
    {
        myLogoRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(fadeOutValue, fadeOutTime));
    }

    IEnumerator FadeOut(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            myLogoRenderer.material.color = newColor;
            yield return null;
        }
        myLogoRenderer.enabled = false;
        _levelHandler.PreLoadLevel(1);
    }
}
