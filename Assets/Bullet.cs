using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 1f;
    [SerializeField]
    int BulletDamage = 5;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float maxVelocity = 9f;
    private BulletManager bulletMan;
    private Vector3 temp;
    private Vector2 destinationDir;
    private Vector2 bulletDelta;
    // Start is called before the first frame update
    void Start()
    {
        bulletMan = FindObjectOfType<BulletManager>();
        temp = new Vector3(bulletMan.mousPos.x - bulletMan.player.transform.position.x ,
                          bulletMan.mousPos.y - bulletMan.player.transform.position.y);
        // temp = new Vector2(bulletMan.mousPos.x,
        //                    bulletMan.mousPos.y);
        //destinationDir = 
        //    Vector2.MoveTowards(transform.position, temp, Time.deltaTime * bulletSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = 
        //    Vector2.MoveTowards(transform.position, temp, Time.deltaTime * bulletSpeed);
        //rb.velocity += temp;
        //rb.AddForce(temp);

        if (rb.velocity.magnitude > maxVelocity)
        {
            //Debug.Log("Working");
            rb.velocity *= 0.5f;
        }
        rb.velocity += new Vector2(temp.x * bulletSpeed, temp.y * bulletSpeed);

        bulletDelta = transform.position;
        if (bulletDelta.magnitude > 40)
        { 
            Debug.Log("Bullet Released into Pool");
            // rb.velocity *= 0;
            // rb.angularVelocity *= 0;
            bulletMan.bulletPool.pool.Release(gameObject);
        }
    }

    void OnDisable()
    {
        Debug.Log("Bullet Reset");
        //gameObject.Disable();
        // temp = new Vector3(bulletMan.mousPos.x - bulletMan.player.transform.position.x ,
        //                   bulletMan.mousPos.y - bulletMan.player.transform.position.y);
    }

}
