using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] EnemyLoc;
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public bool isSpawning = false;
    public bool fullyspawned = false;
    public int enemyCount = 0;
    public float SpawningCd = 2.0f;
    public float currSpawnCd = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnEnemies()
    {
        print("spawning enemies");
        if (fullyspawned)
        {
            return;
        }
        if (isSpawning == false)
        {
            isSpawning = true;
        }
        if (currSpawnCd <= 0 && fullyspawned == false)
        {
            foreach (Transform location in EnemyLoc)
            {
                float randnum = Random.Range(0, 2);
                if (randnum >= 1 && enemyCount < EnemyLoc.Length)
                {
                    float enemyRand = Random.Range(0, 2);
                    if(enemyRand >= 1)
                    {
                      Instantiate(rangedEnemyPrefab, location.position, Quaternion.identity);
                    }else
                    {
                        Instantiate(meleeEnemyPrefab, location.position, Quaternion.identity);
                    }
                    
                    enemyCount += 1;
                }
            }
            currSpawnCd = SpawningCd;
            if (enemyCount == EnemyLoc.Length)
            {
                fullyspawned = true;
                isSpawning = false;
            }
        }
        else
        {
            currSpawnCd -= Time.deltaTime;
        }


    }
}
