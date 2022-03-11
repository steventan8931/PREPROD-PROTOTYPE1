using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public float m_HealthRegen = 5;

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
            m_Player.m_NearCampfire = false;
        }
    }

    private void Update()
    {
        if (m_IsColliding)
        {
            m_Player.m_NearCampfire = true;
            if (m_Player.hitpoints <= 150)
            {
                m_Player.hitpoints += m_HealthRegen * Time.deltaTime;
            }
        }
    }
}
