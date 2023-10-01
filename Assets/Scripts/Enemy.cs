using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
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
    Score score;
    public Animator animator;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        em = FindObjectOfType<EnemyManager>();
        bulletMan = FindObjectOfType<EnemyBulletManager>();
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * MoveSpeed);
        distanceToPlayer = transform.position - player.transform.position;
        timer += Time.deltaTime;

        if (MoveSpeed != 0f) {
            animator.SetBool("IsMoving", true);
        }
        else {
            animator.SetBool("IsMoving", false);
        }
        
        if (distanceToPlayer.magnitude < enemyRange && timer > 3)
        {
            Fire();
            timer = 0;
        }
    }

    private void FixedUpdate()
    {   
        
    }

    private void Fire()
    {
        GameObject bulletFired = bulletMan.SpawnEnemyBullet(gameObject);
        Debug.Log(bulletFired);
        Rigidbody2D brb = bulletFired.GetComponent<Rigidbody2D>();

        Vector2 lookDir = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        brb.AddForce(lookDir.normalized * enemyBulletForce, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerBullet"))
        {
            enemyHealth -= player.bulletDamage;
            em.enemyPool.pool.Release(gameObject);
            score.score++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.curHealth -= collisionDamage;
            player.healthbarForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, player.healthbarMaxWidth * (player.curHealth/player.maxHealth));
            em.enemyPool.pool.Release(gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Destroyed");
    }
}
