using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    [SerializeField]
    public float speed;
    Rigidbody2D rb;
    [SerializeField] float jumpHeight;
    bool onGround;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpHeight = 500f;
	}
	
	// Update is called once per frame
	void Update () {
        //player movement
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
        
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(jumpHeight * Vector2.up);
        }
            
    }
}
