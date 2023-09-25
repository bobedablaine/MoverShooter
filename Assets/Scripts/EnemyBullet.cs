using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 1f;
    [SerializeField]
    int BulletDamage = 5;
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

    void OnCollisionEnter2D(Collision2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision");
            bulletMan.bulletPool.pool.Release(gameObject);
        }
        
        //Requires both objects have rigidbody2d and collider, with maximum 1 kinematic rigidbody
    }


}
