using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerController player;
    [SerializeField]
    Rigidbody2D rb;
    Vector2 distanceFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        distanceFromPlayer = transform.position - player.transform.position;
        if (distanceFromPlayer.magnitude > 30)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(rb.position.x + 10 * Time.deltaTime, rb.position.y));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.curHealth -= player.maxHealth / 2;
            player.healthbarForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, player.healthbarMaxWidth * (player.curHealth/player.maxHealth));
            if (player.curHealth <= 0) player.PlayerDeath();
            Destroy(gameObject);
        }
    }

}
