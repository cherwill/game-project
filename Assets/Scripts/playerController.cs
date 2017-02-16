using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //speed stuff
    float speed;
    [SerializeField]
    float defaultSpeed;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float speedDecay;

    //Components
    Rigidbody2D rb;
    BoxCollider2D bc2d;
    CapsuleCollider2D cc;

    //Jumping variablse
    bool onGround = false;
    public float shortJump = 3f;   // Velocity for the lowest jump
    public float fullJump = 10f;          // Velocity for the highest jump
    bool jump = false;
    bool jumpCancel = false;
    


    // Use this for initialization
    void Start () {
        speed = 0f;
        maxSpeed = 10f;
        acceleration = 10f;
        speedDecay = 0.3f;
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        cc = GetComponent<CapsuleCollider2D>();
	}

    // Update is called once per frame
    void Update() {
        calculateSpeed();

        if (Input.GetButtonDown("Jump") && onGround)   // Player starts pressing the button
            jump = true;
        if (Input.GetButtonUp("Jump") && !onGround)     // Player stops pressing the button
            jumpCancel = true;
    }

    void FixedUpdate()
    {
        //Move
        transform.Translate(speed * Time.deltaTime, 0 , 0);


        // Normal jump
        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, fullJump);
            jump = false;
        }
        // Cancel the jump when the button is no longer pressed
        if (jumpCancel)
        {
            if (rb.velocity.y > shortJump)
                rb.velocity = new Vector2(rb.velocity.x, shortJump);
            jumpCancel = false;
        }
    }

    void calculateSpeed()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            float inputAccel = Input.GetAxis("Horizontal"); //however much input the player is giving
            speed += inputAccel * acceleration;
        } else
        {
            //deceleration, more on ground
            if (speed > 0)
            {
                if (onGround)
                {
                    speed -= speedDecay;
                } else
                {
                    speed -= (speedDecay * 0.25f);
                }
                
            }
            else if (speed < 0)
            {
                if (onGround)
                {
                    speed += speedDecay;
                }
                else
                {
                    speed += (speedDecay * 0.25f);
                }
            }
            
        }
        //player movement
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        if (speed < 0 && Mathf.Abs(speed) > maxSpeed)
        {
            speed = -maxSpeed;
        }
        else if (speed > 0 && speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Terrain")
        {
            //if youre on terrain, you're grounded and can jump again
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        
        if (other.tag == "Terrain")
        {
            //when you leave terrain, you're in the air and cannot jump again
            onGround = false;
        }
    }
}
