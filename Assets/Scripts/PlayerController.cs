using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    public RectTransform healthbarForeground;
    [SerializeField]
    RectTransform staminaForeground;
    public PlayerInputActions playerControls;
    Vector2 moveDirection;
    private InputAction move;
    private InputAction fire;
    private InputAction dodge;
    private Vector2 mousePos;
    Camera main;

    [SerializeField]
    float dodgeLength = 3f;
    [SerializeField]
    float bulletForce = 20f;
    [SerializeField]
    public float maxHealth = 100f;
    public float curHealth = 0f;
    public float healthbarMaxWidth = 247.58f;
    public float bulletDamage = 20f;
    private BulletManager bulletMan;
    private GameObject bulletFired;
    [SerializeField]
    float maxStamina = 10f;
    public float curStamina = 0;
    float staminabarMaxWidth = 247.58f;
    [SerializeField]
    float dodgeCost = 5f;
    Score score;
    [SerializeField]
    DeathScreen deathScreen;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        main = Camera.main;
        bulletMan = FindObjectOfType<BulletManager>();
        curHealth = maxHealth;
        curStamina = maxStamina;
        score = FindObjectOfType<Score>();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        dodge = playerControls.Player.Dodge;
        dodge.Enable();
        dodge.performed += Dodge;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        dodge.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        
        //Dodge Cooldown Timer
        if (curStamina < maxStamina)
        {
            staminaForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, staminabarMaxWidth * (curStamina/maxStamina));
            curStamina += Time.deltaTime;
        }


        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {  
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        bulletFired = bulletMan.SpawnBullet();
        Rigidbody2D brb = bulletFired.GetComponent<Rigidbody2D>();
        brb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }

    private void Dodge(InputAction.CallbackContext context)
    {
        if (curStamina > dodgeCost)
        {
            rb.position += new Vector2(moveDirection.x*dodgeLength, moveDirection.y*dodgeLength);
            curStamina -= 5;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (curHealth <= 0) PlayerDeath();
        healthbarForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, healthbarMaxWidth * (curHealth/maxHealth));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (curHealth <= 0) 
        {
            healthbarForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, healthbarMaxWidth * (0));
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        //gameObject.SetActive(false);
        Time.timeScale = 0;
        if (score.sceneName == "LevelOne")
            deathScreen.DeathSetActive(score.score, score.highscore1);
        if (score.sceneName == "LevelTwo")
            deathScreen.DeathSetActive(score.score, score.highscore2); 
        if (score.sceneName == "LevelThree")
            deathScreen.DeathSetActive(score.score, score.highscore3);
    }

}
