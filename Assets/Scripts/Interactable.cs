using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Components")]
    public int m_Health = 10;
    public GameObject m_DropPrefab;

    public void TakeDamage(int _Damage)
    {
        if (m_Health > 0)
        {
            m_Health -= _Damage;
        }
    }
}
