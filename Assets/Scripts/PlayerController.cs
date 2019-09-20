using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody rb3d;
    public float sensitivity;
    public float groundSpeed = 1f;
    public float flySpeed = 1f;
    public float jumpForce = 20f;
    public bool grounded;
    public GameObject leftFoot;
    public GameObject rightFoot;
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

        transform.Rotate(0, sensitivity * Input.GetAxis("Mouse X"),0);
        
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");

        if(!flying) transform.Translate(new Vector3(xVal, 0, zVal) * groundSpeed);

        float yMove = 0;
        if(Input.GetKey("space") && grounded) yMove = 1;
        rb3d.AddForce(0, jumpForce*yMove, 0);

        if(Input.GetKey("space") && !grounded && !flying){flying = true;rb3d.velocity = new Vector3(0, 0, 0);}
        if(flying) {
            rb3d.useGravity = false;
            if(Input.GetKey("space")) yMove = 1;
            if(Input.GetKey("left shift")) yMove = -1;
            transform.Translate(new Vector3(xVal, yMove, zVal) * flySpeed);
        }
        if(grounded && flying) {flying = false;rb3d.useGravity= true;}
    }
}
