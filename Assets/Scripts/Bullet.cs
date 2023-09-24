using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 1f;
    [SerializeField]
    int BulletDamage = 5;
    private BulletManager bulletMan;
    // Start is called before the first frame update
    void Start()
    {
        bulletMan = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        Debug.Log("Collision");
        bulletMan.bulletPool.pool.Release(gameObject);
        //Requires both objects have rigidbody2d and collider, with maximum 1 kinematic rigidbody
    }


}
