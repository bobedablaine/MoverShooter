using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    float spawnTimer = 4f;
    private float timer = 0f;
    [SerializeField]
    GameObject obstaclePrefab;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(player.transform.position.x - 12, Random.Range(player.transform.position.y-5,
                    player.transform.position.y+5), 0), Quaternion.identity);
    }
}
