using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image m_HealthBar;
    public CharacterMotor m_Motor;
    private float cacheHP;

    private void Start()
    {
        m_Motor = FindObjectOfType<CharacterMotor>();
        cacheHP = m_Motor.hitpoints;
    }
    private void Update()
    {
        m_HealthBar.fillAmount = m_Motor.hitpoints / cacheHP;
    }
}
