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
    [SerializeField]
    float enemySpawnRange = 7;
    [SerializeField]
    
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
        go.transform.position = new Vector2(Random.Range((player.transform.position.x-10), player.transform.position.x+10), 
                                            Random.Range((player.transform.position.y-10), player.transform.position.y+10));
        //go.transform.position = Random.insideUnitCircle * enemySpawnRange;
        //Vector2 temp = go.transform.position - player.transform.position;
        //Debug.Log("Enemy Spawn: " + temp);
        // if (temp.x < 2 || temp.y < 2)
        // {
        //     temp += new Vector2(2, 2);
        // }
        //go.transform.position += new Vector3(2, 2, 0);
        curSpawned++;
        return go;
    }

    // GameObject SpawnObstacle()
    // {

    // }

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
