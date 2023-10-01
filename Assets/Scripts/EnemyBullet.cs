using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
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
        if (distanceFromPlayer.magnitude > 15)
            bulletMan.bulletPool.pool.Release(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            player.curHealth -= bulletDamage;
            bulletMan.bulletPool.pool.Release(gameObject);
        }
    }


}
