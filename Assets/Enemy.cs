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
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log("Enemy Created");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = 
            Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * MoveSpeed);
    }

    void OnDestroy()
    {
        Debug.Log("Enemy Destroyed");
    }
}
