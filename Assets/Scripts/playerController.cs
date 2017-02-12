using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    [SerializeField]
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //player movement
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
    }
}
