using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Components")]
    public int m_Health = 10;
    public GameObject m_DropPrefab;
    public GameObject m_DamagePrefab;
    public Vector2 m_SpawnCountExtents = new Vector2(1, 2);
    public ObjectRespwaner m_Respawner;

    private AudioManager m_audio;

    public void TakeDamage(int _Damage)
    {
        m_DamagePrefab.GetComponent<TextMeshPro>().text = _Damage.ToString();
        if (m_Health > 0)
        {
            m_Health -= _Damage;
        }
    }
}
