using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoother : MonoBehaviour
{
    public Transform target;
    public Transform targetLook;
    public float smoothSpeed;
    public float smoothLookSpeed;
    Vector3 targetLookPosSmooth;

    public bool pausePosition;

    void FixedUpdate()
    {
        Vector3 targPosition = target.position;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targPosition, smoothSpeed);
        if (!pausePosition) transform.position = smoothPos;
        Vector3 targLookPosition = targetLook.position;
        targetLookPosSmooth = Vector3.Lerp(targetLookPosSmooth, targLookPosition, smoothLookSpeed);
        transform.LookAt(targetLookPosSmooth);
    }
}
