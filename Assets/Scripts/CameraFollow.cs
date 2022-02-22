using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float m_FollowSpeed = 5.0f;
    public Transform m_CameraOffset;

    private void Update()
    {
        if (transform.position != m_CameraOffset.position)
        {
            transform.position = Vector3.Lerp(transform.position, m_CameraOffset.position, Time.deltaTime * m_FollowSpeed);
        }
    }
}
