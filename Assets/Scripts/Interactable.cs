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
    public Sprite[] m_DamagedSprite;
    public SpriteRenderer m_DamageRenderer;

    private AudioManager m_audio;
    protected int cacheHP;

    private void Start()
    {
        cacheHP = m_Health;
    }

    public void TakeDamage(int _Damage)
    {
        m_DamagePrefab.GetComponent<TextMeshPro>().text = _Damage.ToString();
        if (m_Health > 0)
        {
            m_Health -= _Damage;
        }
    }

    protected void UpdateVisualDamage()
    {

        if (m_Health < cacheHP / 4)         //25%
        {
            m_DamageRenderer.sprite = m_DamagedSprite[2];
        }
        else if (m_Health < (cacheHP / 2)) //50%
        {
            m_DamageRenderer.sprite = m_DamagedSprite[1];
        }
        else if (m_Health < (cacheHP * 0.75)) //75%
        {
            m_DamageRenderer.sprite = m_DamagedSprite[0];
        }
        else //>75%
        {
            m_DamageRenderer.sprite = null;
        }
    }
}
