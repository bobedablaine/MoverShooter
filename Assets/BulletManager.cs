using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnitPool))]
public class BulletManager : MonoBehaviour
{   
    Camera main;

    [SerializeField]
    int maxBulletsSpawned = 100;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float MoveSpeed = 1f;
    public UnitPool bulletPool {get; protected set;}
    int curSpawned = 0;
    public PlayerController player;
    public Vector2 mousPos;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<UnitPool>();
        player = FindObjectOfType<PlayerController>();
        main = Camera.main;
    }

    public Vector2 SpawnBullet()
    {
        GameObject go;
        go = bulletPool.pool.Get();
        go.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        mousPos = main.ScreenToWorldPoint(Input.mousePosition);
        curSpawned++;
        return mousPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
