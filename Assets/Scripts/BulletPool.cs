using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public ObjectPool<GameObject> pool {get; protected set;}
    [SerializeField]
    GameObject bulletPrefab;
    private BulletManager bulletMan;
    //private Vector2 temp;
    // Start is called before the first frame update
    void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreatePooledBullet, OnGetBulletFromPool, OnReleasedToPool, OnDestroyFromPool);
        //bulletMan = FindObjectOfType<BulletManager>();
        //temp = new Vector2(bulletMan.mousPos.x - bulletMan.player.transform.position.x ,
        //                  bulletMan.mousPos.y - bulletMan.player.transform.position.y);
        //temp = bulletMan.main.ScreenToWorldPoint(Input.mousePosition);

    }

    GameObject CreatePooledBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    void OnGetBulletFromPool(GameObject b)
    {
        b.gameObject.SetActive(true);
    }

    void OnReleasedToPool(GameObject b)
    {
        b.gameObject.SetActive(false);
    }

    void OnDestroyFromPool(GameObject b)
    {
        Destroy(b.gameObject);
    }
}