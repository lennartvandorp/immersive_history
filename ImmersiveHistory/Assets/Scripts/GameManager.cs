using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    

    [SerializeField] private float minExplosionTime, maxExplosionTime, minIntensity, maxIntensity;

    public static GameManager Instance { get; private set; }
    private void Awake() { Instance = this; }

    [SerializeField] public float timeUntilSirens, timeUntilBombing, timeUntilFinale, fadeOutTime;
    public UnityEvent onStartGame, onStartSirens, onStartBombing, onFinale, onFadeOut;

    public FloatEvent onExplosion;

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> { }
    private void Start()
    {
        StartCoroutine(DoTimers());
    }
    private IEnumerator DoTimers()
    {
        onStartGame.Invoke();
        yield return new WaitForSeconds(timeUntilSirens);
        onStartSirens.Invoke();
        yield return new WaitForSeconds(timeUntilBombing);
        onStartBombing.Invoke();
        yield return new WaitForSeconds(timeUntilFinale);
        onFinale.Invoke();
    }
}