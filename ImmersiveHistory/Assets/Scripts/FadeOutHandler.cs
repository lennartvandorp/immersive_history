using System.Collections;
using UnityEngine;

public class FadeOutHandler : MonoBehaviour
{
    Material material;
    const float fadedInAlpha = 0f, fadedOutAlpha = 1f;

    void Start()
    {
        // Retrieves the material component and triggers a fade-in animation
        material = GetComponent<MeshRenderer>().material;
        FadeIn(2f);

        // Subscribes to the GameManager's onFadeOut event
        GameManager.Instance.onFadeOut.AddListener(DoFadeOut);
    }

    /// <summary>
    /// Initiates a fade-in animation with the specified time duration.
    /// </summary>
    /// <param name="time">Duration of the fade-in animation</param>
    public void FadeIn(float time)
    {
        // Stops any ongoing fading and starts a new fade-in coroutine
        StopAllCoroutines();
        StartCoroutine(Fade(time, fadedInAlpha));
    }

    /// <summary>
    /// Initiates a fade-out animation using the GameManager's specified fade-out time.
    /// </summary>
    void DoFadeOut()
    {
        FadeOut(GameManager.Instance.fadeOutTime);
    }

    /// <summary>
    /// Initiates a fade-out animation with the specified time duration.
    /// </summary>
    /// <param name="time">Duration of the fade-out animation</param>
    public void FadeOut(float time)
    {
        // Stops any ongoing fading and starts a new fade-out coroutine
        StopAllCoroutines();
        StartCoroutine(Fade(time, fadedOutAlpha));
    }

    /// <summary>
    /// Coroutine responsible for the fading animation over a specified duration to a target alpha value.
    /// </summary>
    /// <param name="duration">Duration of the fading animation</param>
    /// <param name="targetAlpha">Target alpha value</param>
    /// <returns>Coroutine for handling the fading animation</returns>
    public IEnumerator Fade(float duration, float targetAlpha)
    {
        float timeElapsed = 0f;
        float startAlpha = material.color.a;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            // Interpolates to get the current alpha value
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / duration);
            SetAlpha(currentAlpha);

            yield return null;
        }

        // Ensures the final alpha value is set correctly
        SetAlpha(targetAlpha);
    }

    /// <summary>
    /// Sets the alpha value of the material's color.
    /// </summary>
    /// <param name="alpha">Alpha value to set</param>
    void SetAlpha(float alpha)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
    }
}
