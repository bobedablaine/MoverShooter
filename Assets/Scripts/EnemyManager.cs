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
    public int curSpawned = 0;
    private PlayerController player;
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
        go.transform.position = new Vector2(Random.Range((player.transform.position.x-10), player.transform.position.x+10), 
                                            Random.Range((player.transform.position.y-10), player.transform.position.y+10));
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
