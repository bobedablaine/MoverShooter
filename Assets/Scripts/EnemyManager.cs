using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(UnitPool))]
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    int maxEnemiesSpawned = 10, spawnsPerSecond = 1;
    [SerializeField]
    GameObject enemyPrefab;
    public UnitPool enemyPool {get; protected set;}
    float timer = 0;
    int curSpawned = 0;
    private PlayerController player;
    //private Vector2 distanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        enemyPool = GetComponent<UnitPool>();
        player = FindObjectOfType<PlayerController>();
    }
    
    GameObject SpawnEnemy()
    {
        GameObject go;
        go = enemyPool.pool.Get();
        go.transform.position = new Vector2(Random.Range(-8, 8), Random.Range(-8, 8));
        curSpawned++;
        return go;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (curSpawned < maxEnemiesSpawned && timer > 1f/spawnsPerSecond)
        {
            SpawnEnemy();
            timer = 0;
        }
    }
}
