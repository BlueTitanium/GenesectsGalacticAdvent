using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody rb3d;
    public float sensitivity;
    public float speed = 1f;
    public bool grounded;
    public GameObject leftFoot;
    public GameObject rightFoot;
    private bool leftGround;
    private bool rightGround;

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

        transform.Translate(new Vector3(xVal, 0, zVal)*speed);
    }

}
