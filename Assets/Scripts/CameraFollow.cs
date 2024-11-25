using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target = null;
    private float smoothSpeed = 0.125f;
    [SerializeField]
    private Vector3 offset = new Vector3 (0f, 2.25f, -1.5f);

    private Vector3 velocity = Vector3.zero;
    private void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
    }

    public void CenterOnTarget()
    {
        transform.position = target.position + offset;
    }
} 
