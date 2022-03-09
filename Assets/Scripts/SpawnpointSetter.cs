using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointSetter : MonoBehaviour
{
    private CharacterMotor m_Player;
    public bool m_IsColliding = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_Player = other.GetComponent<CharacterMotor>();
            m_IsColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMotor>())
        {
            m_IsColliding = false;
        }
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                float cacheY = m_Player.transform.position.y;
                m_Player.m_SpawnPoint = new Vector3(transform.position.x, cacheY, transform.position.z);
            }
        }

    }
}
