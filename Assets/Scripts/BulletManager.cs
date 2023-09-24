using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BulletPool))]
public class BulletManager : MonoBehaviour
{   
    public Camera main;

    [SerializeField]
    int maxBulletsSpawned = 100;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float MoveSpeed = 1f;
    public BulletPool bulletPool {get; protected set;}
    int curSpawned = 0;
    public PlayerController player;
    public Vector2 mousPos;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GetComponent<BulletPool>();
        player = FindObjectOfType<PlayerController>();
        main = Camera.main;
    }

    public GameObject SpawnBullet()
    {
        //Bullet go;
        GameObject temp;
        temp = bulletPool.pool.Get();
        temp.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        Debug.Log(temp);
        //go = temp;
        //mousPos = main.ScreenToWorldPoint(Input.mousePosition);
        curSpawned++;
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
