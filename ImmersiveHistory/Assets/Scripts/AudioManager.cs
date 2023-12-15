using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sirenSource, footstepsSource;
    AudioSource[] allAudioSources;
    private void Start()
    {
        GameManager.Instance.onStartSirens.AddListener(OnStartSiren);
        allAudioSources = FindObjectsOfType<AudioSource>();
        GameManager.Instance.onFadeOut.AddListener(StartFadeOut);
    }

    private void OnStartSiren()
    {
        if (sirenSource)
        {
            sirenSource.Play();
        }
    }

    void StartFadeOut()
    {
        StartCoroutine(FadeOutAllAudioSources());
    }

    /// <summary>
    /// Loops through the audio sources by fading them in and out. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutAllAudioSources()
    {
        float timeEllapsed = 0f;
        float[] startVolumes = new float[allAudioSources.Length];
        for (int i = 0; i < allAudioSources.Length; i++)
        {
            startVolumes[i] = allAudioSources[i].volume;
        }
        while (timeEllapsed < GameManager.Instance.fadeOutTime)
        {
            for (int i = 0; i < allAudioSources.Length; i++)
            {
                allAudioSources[i].volume = Mathf.Lerp(startVolumes[i], 0f, timeEllapsed / GameManager.Instance.fadeOutTime);
            }
            timeEllapsed += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < allAudioSources.Length; i++)
        {
            allAudioSources[i].volume = 0f;
        }
    }
}
