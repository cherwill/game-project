using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    [SerializeField]
    public float speed;
    Rigidbody2D rb;
    [SerializeField] float jumpHeight;
    bool onGround = true;
    BoxCollider2D bc2d;
    public float jumpShortSpeed = 300f;   // Velocity for the lowest jump
    public float jumpSpeed = 1000f;          // Velocity for the highest jump
    bool jump = false;
    bool jumpCancel = false;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        jumpHeight = 500f;
	}
	//test comment
	// Update is called once per frame
	void Update () {
        //player movement
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        
        //if(Input.GetButtonDown("Jump") && onGround ) {}
        //{
        //    rb.AddForce(jumpHeight * Vector2.up);
        //}


        if (Input.GetButtonDown("Jump") && onGround)   // Player starts pressing the button
            jump = true;
        if (Input.GetButtonUp("Jump") && !onGround)     // Player stops pressing the button
            jumpCancel = true;
    }

    void FixedUpdate()
    {
        // Normal jump (full speed)
        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jump = false;
        }
        // Cancel the jump when the button is no longer pressed
        if (jumpCancel)
        {
            if (rb.velocity.y > jumpShortSpeed)
                rb.velocity = new Vector2(rb.velocity.x, jumpShortSpeed);
            jumpCancel = false;
        }
    }
}
