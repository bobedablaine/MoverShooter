using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BulletPool))]
public class BulletManager : MonoBehaviour
{   
    public BulletPool bulletPool {get; protected set;}
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<BulletPool>();
        player = FindObjectOfType<PlayerController>();
    }

    public GameObject SpawnBullet()
    {
        GameObject temp;
        temp = bulletPool.pool.Get();
        temp.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        return temp;
    }
}
