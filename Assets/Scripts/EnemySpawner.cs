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
    public int enemyLimitDay = 5;
    public int enemyLimitNight = 8;
    public float SpawningCd = 2.0f;
    public float currSpawnCd = 0f;

    public DayNightScr dayNightSystem;
    void Start()
    {
        dayNightSystem = GameObject.FindGameObjectWithTag("DayNightManager").GetComponent<DayNightScr>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    void spawnEnemies()
    {
        //print("spawning enemies");
        
        if ((fullyspawned && enemyCount < enemyLimitDay && dayNightSystem.isNight == false)|| (fullyspawned && enemyCount < enemyLimitNight && dayNightSystem.isNight == true))
        {
            fullyspawned = false;
        }
        if(fullyspawned)
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
                if ((randnum >= 1 && enemyCount < enemyLimitDay && dayNightSystem.isNight == false) || (randnum >= 1 && enemyCount < enemyLimitNight && dayNightSystem.isNight == true))
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
            if ((enemyCount == enemyLimitDay && dayNightSystem.isNight == false) || (enemyCount == enemyLimitNight && dayNightSystem.isNight == true))
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
