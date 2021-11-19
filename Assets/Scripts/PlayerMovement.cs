using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    private bool isGrounded;
    public float checkRadius;
    private int jumpCount;
    public int maxJumpCount;
   

    // Awake is called after all objects are initalized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //Will look for a component on this GameObject (what the script is attached to of the type RigidBody2D.

    }
    private void Start()
    {
        jumpCount = maxJumpCount;
    }
    // Use this for initialization


    // Update is called once per frame
    void Update ()
    {
        // Get Inputs
        ProcessInputs();

        // Animate
        Animate();

        

    }

    // Better for handling Physics, can be called multipel times per update frame.
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        //Move
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            flipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            flipCharacter();
        }
    }
  
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 -> 1
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
        
            transform.GetChild(0).Rotate(0f, 0f, -90f);
            //transform.GetChild(0).Rotate(0f, 0f, 0f);

        }
    }

    private void flipCharacter()
    {
        facingRight = !facingRight; //Inverse bool
        transform.Rotate(0f, 180f, 0f);
    }
}
