using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletManager bulletMan;
    private Vector2 distanceFromPlayer;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        bulletMan = FindObjectOfType<BulletManager>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = transform.position - player.transform.position;
        if (distanceFromPlayer.magnitude > 50)
            bulletMan.bulletPool.pool.Release(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            bulletMan.bulletPool.pool.Release(gameObject);
        }
    }


}
