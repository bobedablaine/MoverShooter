using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Camera main;
    PlayerController player;
    // Start is called before the first frame update
    [SerializeField]
    float MoveSpeed = 1f;
    [SerializeField]
    float enemyHealth = 20f;
    private EnemyManager em;
    private Vector2 distanceToPlayer;
    [SerializeField]
    float enemyRange = 7;
    [SerializeField]
    Rigidbody2D rb;
    private EnemyBulletManager bulletMan;
    [SerializeField]
    float enemyBulletForce = 5f;
    float timer = 2;
    [SerializeField]
    float collisionDamage = 20f;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log("Enemy Created");
        em = FindObjectOfType<EnemyManager>();
        main = Camera.main;
        bulletMan = FindObjectOfType<EnemyBulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * MoveSpeed);
        distanceToPlayer = transform.position - player.transform.position;
        timer += Time.deltaTime;
        
        if (distanceToPlayer.magnitude < enemyRange && timer > 3)
        {
            //Debug.Log("distanceToPlayer Mag: " + distanceToPlayer.magnitude);
            //Debug.Log("enemyRange Mag: " + enemyRange);
            Fire();
            timer = 0;
        }
    }

    private void FixedUpdate()
    {   
        Vector2 lookDir = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Fire()
    {
        GameObject bulletFired = bulletMan.SpawnEnemyBullet(gameObject);
        Debug.Log(bulletFired);
        Rigidbody2D brb = bulletFired.GetComponent<Rigidbody2D>();
        brb.AddForce(transform.up * enemyBulletForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //TODO: Must make it so enemies dont kill eachother
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            enemyHealth -= player.bulletDamage;
            Debug.Log("Enemy Hit by Player Bullet");
            em.enemyPool.pool.Release(gameObject);
        }
        else if (collider.gameObject.CompareTag("Player"))
        {
            player.curHealth -= collisionDamage;
            Debug.Log("Enemy Hit by Player Body");
            em.enemyPool.pool.Release(gameObject);
        }
        //Requires both objects have rigidbody2d and collider, with maximum 1 kinematic rigidbody
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Destroyed");
    }
}
