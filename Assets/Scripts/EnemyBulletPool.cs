using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPool : MonoBehaviour
{
    public ObjectPool<GameObject> pool {get; protected set;}
    [SerializeField]
    GameObject bulletPrefab;
    //private BulletManager bulletMan;
    //private Vector2 temp;
    // Start is called before the first frame update
    void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreatePooledEnemyBullet, OnGetEnemyBulletFromPool, OnReleasedToEnemyBulletPool, OnDestroyFromEnemyBulletPool);

    }

    GameObject CreatePooledEnemyBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    void OnGetEnemyBulletFromPool(GameObject b)
    {
        b.gameObject.SetActive(true);
    }

    void OnReleasedToEnemyBulletPool(GameObject b)
    {
        b.gameObject.SetActive(false);
    }

    void OnDestroyFromEnemyBulletPool(GameObject b)
    {
        Destroy(b.gameObject);
    }
}