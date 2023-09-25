using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyBulletPool))]
public class EnemyBulletManager : MonoBehaviour
{   
    public Camera main;

    [SerializeField]
    int maxBulletsSpawned = 100;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float MoveSpeed = 1f;
    public EnemyBulletPool bulletPool {get; protected set;}
    public Vector2 mousPos;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<EnemyBulletPool>();
        main = Camera.main;
    }

    public GameObject SpawnEnemyBullet(GameObject caller)
    {
        //Bullet go;
        GameObject temp;
        temp = bulletPool.pool.Get();
        temp.transform.position = new Vector2(caller.transform.position.x, caller.transform.position.y);
        Debug.Log(temp);
        //go = temp;
        //mousPos = main.ScreenToWorldPoint(Input.mousePosition);
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
