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
    GameObject missile;
    [SerializeField]
    float dodgeLength = 3f;
    [SerializeField]
    float maxVelocity = 9f;
    [SerializeField]
    float dodgeCD = 5f;
    private bool dodgeOnCD = false;
    [SerializeField]
    private float cdTimer = 0;

    private BulletManager bulletMan;
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
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            //Debug.Log("Working");
            rb.velocity *= 0.5f;
        }
        rb.velocity += new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        
        
    }

    private void Fire(InputAction.CallbackContext context)
    {
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        bulletMan.SpawnBullet();
        //Instantiate<GameObject>(missile, mousePos, Quaternion.identity);
        //Debug.Log(mousePos);
        //Debug.Log("We fired");
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
