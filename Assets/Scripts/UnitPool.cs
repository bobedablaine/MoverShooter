using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitPool : MonoBehaviour
{
    public ObjectPool<GameObject> pool {get; protected set;}
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    EnemyManager enemyMan;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void OnEnable()
    {
        pool = new ObjectPool<GameObject>(CreatePooledEnemy, OnGetEnemyFromPool, OnReleasedToPool, OnDestroyFromPool);
    }

    GameObject CreatePooledEnemy()
    {
        GameObject obj = Instantiate(enemyPrefab);
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        return obj;
    }

    void OnGetEnemyFromPool(GameObject b)
    {
        //b.postition = player.position;
        b.gameObject.SetActive(true);
    }

    void OnReleasedToPool(GameObject b)
    {
        b.gameObject.SetActive(false);
        enemyMan.curSpawned--;
    }

    void OnDestroyFromPool(GameObject b)
    {
        Destroy(b.gameObject);
    }
}