using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private Transform startPos, endPos;
    float startMovingTime;
    bool isMoving;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        startPos.parent = null;
        startPos.parent = null;
    }

    public void StartMoving()
    {
        duration = source.clip.length;
        isMoving = true;
        transform.position = startPos.position;
        startMovingTime = Time.timeSinceLevelLoad;
        source.Play();
        StartCoroutine(DoFinaleCallBack());
    }

    IEnumerator DoFinaleCallBack()
    {
        yield return new WaitForSeconds(source.clip.length);
        GameManager.Instance.onFadeOut.Invoke();
        Debug.Log("DoCallBack");
    }

    public void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(startPos.position, endPos.position, (Time.timeSinceLevelLoad - startMovingTime) / duration);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(startPos.position, endPos.position);
    }
}