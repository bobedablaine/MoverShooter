using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    [SerializeField]
    float MoveSpeed = 1f;
    [SerializeField]
    int EnemyHealth = 5;
    private EnemyManager em;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log("Enemy Created");
        em = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * MoveSpeed);
    }

    // void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Debug.Log("Trigger");
    //     em.enemyPool.pool.Release(collider.gameObject);
    //     must be Kinematic, other collider must be "Trigger"
    // }

    void OnCollisionEnter2D(Collision2D collider)
    {
        //TODO: Must make it so enemies dont kill eachother
        Debug.Log("Collision");
        em.enemyPool.pool.Release(gameObject);
        //Requires both objects have rigidbody2d and collider, with maximum 1 kinematic rigidbody
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Destroyed");
    }
}
