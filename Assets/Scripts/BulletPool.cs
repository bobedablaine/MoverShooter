using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public ObjectPool<GameObject> pool {get; protected set;}
    [SerializeField]
    GameObject bulletPrefab;
    //private BulletManager bulletMan;
    //private Vector2 temp;
    // Start is called before the first frame update
    void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreatePooledBullet, OnGetBulletFromPool, OnReleasedToBulletPool, OnDestroyFromBulletPool);

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

    void OnReleasedToBulletPool(GameObject b)
    {
        b.gameObject.SetActive(false);
    }

    void OnDestroyFromBulletPool(GameObject b)
    {
        Destroy(b.gameObject);
    }
}