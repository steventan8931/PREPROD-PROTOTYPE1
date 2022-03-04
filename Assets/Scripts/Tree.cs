using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Interactable
{
    [Header("Tree Components")]
    public float m_DeathTimer = 0.0f;
    public float m_DespawnTimer = 1.0f;

    private void Update()
    {
        if (m_Health <= 0)
        {
            m_DeathTimer += Time.deltaTime;

            if (m_DeathTimer > m_DespawnTimer)
            {
                int spawnCount = (int)Random.Range(m_SpawnCountExtents.x, m_SpawnCountExtents.y);
                for (int i = 0; i < spawnCount; i++)
                {
                    Instantiate(m_DropPrefab, transform.position, Quaternion.Euler(Vector3.zero));
                }

                ObjectRespwaner.Instance.m_Trees.Remove(gameObject);
                Destroy(gameObject);
            }

        }
    }
}
