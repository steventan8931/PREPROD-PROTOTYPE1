using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Interactable
{
    [Header("Tree Components")]
    public float m_DeathTimer = 0.0f;
    public float m_DespawnTimer = 1.0f;
    public Transform[] m_RockSpawnPoints;

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
                    //Instantiate(m_DropPrefab, m_RockSpawnPoints[i].position, Quaternion.identity);
                    Instantiate(m_DropPrefab, transform.position, Quaternion.identity);
                }


                ObjectRespwaner.Instance.m_Rocks.Remove(gameObject);
                Destroy(gameObject);
            }

        }
    }
}
