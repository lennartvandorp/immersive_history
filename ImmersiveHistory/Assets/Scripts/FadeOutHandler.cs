using System.Collections;
using UnityEngine;

public class FadeOutHandler : MonoBehaviour
{
    Material material;
    const float fadedInAlpha = 0f, fadedOutAlpha = 1f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        FadeIn(2f);
        GameManager.Instance.onFadeOut.AddListener(DoFadeOut);
    }

    public void FadeIn(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, fadedInAlpha));
    }

    void DoFadeOut()
    {
        FadeOut(GameManager.Instance.fadeOutTime);
    }

    public void FadeOut(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Fade(time, fadedOutAlpha));
    }

    public IEnumerator Fade(float duration, float targetAlpha)
    {
        float timeEllapsed = 0f;
        float startAlpha = material.color.a;
        while (timeEllapsed < duration)
        {
            timeEllapsed += Time.deltaTime;

            //Interpolates to get the currently needed color
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeEllapsed / duration);
            SetAlpha(currentAlpha);
            yield return null;
        }
        SetAlpha(targetAlpha);
    }

    void SetAlpha(float alpha)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);

    }

}
