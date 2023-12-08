using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioPlayer : MonoBehaviour
{
    [SerializeField] private float playVolume;

    bool isPlaying;
    public void ToggleSound()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            GetComponent<AudioSource>().volume = playVolume;
        }
        else
        {
            isPlaying = false;
            GetComponent<AudioSource>().volume = 0;
        }
    }
}
