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
    float dodgeCD = 5f;
    private bool dodgeOnCD = false;
    [SerializeField]
    private float cdTimer = 0;
    [SerializeField]
    float bulletForce = 20f;

    private BulletManager bulletMan;
    private GameObject bulletFired;
    private void Awake()
    {
        playerControls = new PlayerInputActions();
        main = Camera.main;
        bulletMan = FindObjectOfType<BulletManager>();
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
        if (dodgeOnCD)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer > dodgeCD)
            {
                dodgeOnCD = false;
                cdTimer = 0;
            }
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
        if (!dodgeOnCD)
        {
            rb.position += new Vector2(moveDirection.x*dodgeLength, moveDirection.y*dodgeLength);
            dodgeOnCD = true;
        }
        
    }
}
