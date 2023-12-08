using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomShake : MonoBehaviour
{
    [SerializeField] private float shakeFrequency, shakeSpeedMult, intensityReduction, intensityMult;
    private float shakeSpeed;
    Vector3 shakeTargetPos;
    Rigidbody rb;
    private Vector3 startPosition;

    [SerializeField] private bool doShake;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
        shakeTargetPos = startPosition;
        GameManager.Instance.onExplosion.AddListener(StartRoomShake);
    }

    private void StartRoomShake(float _intensity)
    {
        StartCoroutine(ShakeRoom(_intensity));
    }

    float intensity;
    public IEnumerator ShakeRoom(float _intensity)
    {
        intensity = _intensity * intensityMult;
        while (intensity > 0)
        {
            SetTargetPos();
            intensity -= intensityReduction;
            yield return new WaitForSeconds(1f / shakeFrequency);
        }
        shakeTargetPos = startPosition;
    }

    void SetTargetPos()
    {
        shakeTargetPos = startPosition +
            new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * intensity;
    }

    private void Update()
    {
        rb.position = MoveAndStop(rb.position, shakeTargetPos, shakeSpeedMult * Time.deltaTime);
        if (doShake)
        {
            StartRoomShake(.1f);
            doShake = false;
        }
    }

    Vector3 MoveAndStop(Vector3 start, Vector3 destination, float maxDistance)
    {
        if ((start - destination).magnitude < maxDistance)
        {
            return destination;
        }
        Vector3 dir = destination - start;
        return start + dir.normalized * maxDistance;
    }
}