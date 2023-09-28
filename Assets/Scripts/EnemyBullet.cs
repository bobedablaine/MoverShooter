using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 1f;
    [SerializeField]
    public float bulletDamage = 20f;
    private EnemyBulletManager bulletMan;
    private Vector2 distanceFromPlayer;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        bulletMan = FindObjectOfType<EnemyBulletManager>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = transform.position - player.transform.position;
        //Debug.Log(distanceFromPlayer.magnitude);
        if (distanceFromPlayer.magnitude > 15)
            bulletMan.bulletPool.pool.Release(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            player.curHealth -= bulletDamage;
            Debug.Log("Hit by Enemy Bullet");
            bulletMan.bulletPool.pool.Release(gameObject);
        }
    }


}
