using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmbientSounds : MonoBehaviour
{
    [SerializeField] protected float transitionTime = 5f, startTime = 0f;
    [SerializeField] AudioClip ambientClip;
    [SerializeField] protected bool keepLooping = true;
    [SerializeField, Range(0f, 1f)] float spatialBlend = 1f, volume = 1f;



    protected AudioSource[] sources = new AudioSource[2];



    protected virtual void Start()
    {
        AudioSource copyableSource = GetComponent<AudioSource>();
        AudioSource[] existingSources = GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            //Sets up the audio sources. 
            if (existingSources.Length > i)
            {
                sources[i] = existingSources[i];
            }
            else
            {
                sources[i] = gameObject.AddComponent<AudioSource>();
            }
            sources[i].clip = ambientClip;
            sources[i].volume = 0f;
            sources[i].loop = false;
            sources[i].spatialBlend = spatialBlend;
            sources[i].Stop();
        }
        if (transitionTime * 2f > ambientClip.length)
        {
            transitionTime = ambientClip.length / 2f;
        }
    }

    /// <summary>
    /// Initiates the coroutine for the sounds
    /// </summary>
    public void StartSounds()
    {
        StartCoroutine(FadeBetweenSounds(0, startTime));
    }


    /// <summary>
    /// Fades in a new audio source, recursively starts it again for another audio source, and then fades out itself
    /// </summary>
    /// <param name="source">Index of the source to fade in. </param>
    /// <param name="offset">The amount of time it takes to start</param>
    /// <returns></returns>
    protected IEnumerator FadeBetweenSounds(int source, float offset)
    {
        yield return new WaitForSeconds(offset);
        if (source >= sources.Length) { source = 0; }
        sources[source].Play();
        float timeEllapsed = 0f;
        while (timeEllapsed < transitionTime)
        {
            sources[source].volume = timeEllapsed / transitionTime * volume;
            timeEllapsed += Time.deltaTime;
            yield return null;
        }
        sources[source].volume = volume;

        yield return new WaitForSeconds(ambientClip.length - 2f * transitionTime);

        //Starts the same process for the other source
        if (keepLooping)
        {
            StartCoroutine(FadeBetweenSounds(source + 1, 0f));
        }

        timeEllapsed = 0f;
        while (timeEllapsed < transitionTime)
        {
            sources[source].volume = (1f - timeEllapsed / transitionTime) * volume;
            timeEllapsed += Time.deltaTime;
            yield return null;
        }
        sources[source].volume = 0f;
    }
}
