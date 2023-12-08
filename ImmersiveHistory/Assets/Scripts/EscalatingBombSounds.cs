using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EscalatingBombSounds : MonoBehaviour
{
    [SerializeField] private AudioClip clip1, clip2, clip3, clip4, planeClip;
    [SerializeField] private AudioSource source;

    float startBombSoundsTime;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        GameManager.Instance.onFinale.AddListener(InstantiateBombSounds);
    }

    void InstantiateBombSounds()
    {
        StartCoroutine(InitBombSounds());
    }

    private IEnumerator InitBombSounds()
    {
        source.PlayOneShot(clip4);
        GameManager.Instance.onExplosion.Invoke(.02f);
        yield return new WaitForSeconds(planeClip.length / 5f);
        source.PlayOneShot(clip3);
        GameManager.Instance.onExplosion.Invoke(.04f);
        yield return new WaitForSeconds(planeClip.length / 5f);
        source.PlayOneShot(clip2);
        GameManager.Instance.onExplosion.Invoke(.06f);
        yield return new WaitForSeconds(planeClip.length / 5f);
        source.PlayOneShot(clip1);
        GameManager.Instance.onExplosion.Invoke(.1f);
    }
}
