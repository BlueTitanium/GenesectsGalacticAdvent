using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody rb3d;
    public float sensitivity;
    public float groundSpeed = 1f;
    public float flySpeed = .5f;
    public float rotSpeed = 10f;    
    public float jumpForce = 20f;
    public bool grounded;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public Camera cam;
    private bool leftGround;
    private bool rightGround;
    public bool flying = false;
    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        offset = new Vector3(0,0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        leftGround = leftFoot.GetComponent<GroundCheck>().grounded;
        rightGround = rightFoot.GetComponent<GroundCheck>().grounded;
        
        if(leftGround || rightGround) grounded = true;
        else grounded = false;
        
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");
        float step = rotSpeed * Time.deltaTime;
        if(!flying){
            cam.GetComponent<RPGCamera>().limitYRotation = true;

            if(zVal != 0) transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,new Vector3(cam.transform.forward.x,0f,cam.transform.forward.z),step, 0.0f));

            if(!flying) transform.Translate(new Vector3(xVal, 0, zVal) * groundSpeed);
        }
        float yMove = 0;
        if(Input.GetKey("space") && grounded) yMove = 1;
        rb3d.AddForce(0, jumpForce*yMove, 0);

        if(Input.GetKeyDown("space") && !grounded && !flying){flying = true;rb3d.velocity = new Vector3(0, 0, 0);}
        if(flying) {
            cam.GetComponent<RPGCamera>().limitYRotation = false;
            rb3d.useGravity = false;
            if(Input.GetKey("space")) yMove = 1f;
            if(Input.GetKey("left shift")) yMove = -.1f;
            if(zVal != 0) transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,new Vector3(cam.transform.forward.x,cam.transform.forward.y,cam.transform.forward.z),step, 0.0f));
            transform.Translate(new Vector3(xVal, yMove, zVal) * flySpeed);
        }
        if(grounded && flying) {flying = false;rb3d.useGravity= true;}
    }
}
