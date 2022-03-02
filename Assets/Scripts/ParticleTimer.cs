using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTimer : MonoBehaviour
{
    public float m_DecayTime = 0.5f;
    private float m_DecayTimer = 0.0f;

    private void Update()
    {
        m_DecayTimer += Time.deltaTime;
        if (m_DecayTimer >= m_DecayTime)
        {
            Destroy(gameObject);
        }
    }
}
