using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyBulletPool))]
public class EnemyBulletManager : MonoBehaviour
{   
    public EnemyBulletPool bulletPool {get; protected set;}
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<EnemyBulletPool>();
    }

    public GameObject SpawnEnemyBullet(GameObject caller)
    {
        GameObject temp;
        temp = bulletPool.pool.Get();
        temp.transform.position = new Vector2(caller.transform.position.x, caller.transform.position.y);
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
