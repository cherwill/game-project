using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    [SerializeField]
    public float speed;
    Rigidbody2D rb;
    bool onGround = false;
    BoxCollider2D bc2d;
    public float shortJump = 3f;   // Velocity for the lowest jump
    public float fullJump = 10f;          // Velocity for the highest jump
    bool jump = false;
    bool jumpCancel = false;
    CapsuleCollider2D cc;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        cc = GetComponent<CapsuleCollider2D>();
	}
    //test comment
    // Update is called once per frame
    void Update() {
        //player movement
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        //onGround = Physics.CheckCapsule(cc.bounds.center, new Vector3(cc.bounds.center.x, cc.bounds.min.y - 0.1f, cc.bounds.center.z), 0.18f);

        if (Input.GetButtonDown("Jump") && onGround)   // Player starts pressing the button
            jump = true;
        if (Input.GetButtonUp("Jump") && !onGround)     // Player stops pressing the button
            jumpCancel = true;
    }

    void FixedUpdate()
    {
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Terrain")
        {
            onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Terrain")
        {
            onGround = false;
        }
    }
}
