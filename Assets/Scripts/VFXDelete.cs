using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDelete : MonoBehaviour
{
    public float m_DecayTime = 2.0f;
    private float m_Timer = 0.0f;

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer > m_DecayTime)
        {
            Destroy(gameObject);
        }
    }
}
