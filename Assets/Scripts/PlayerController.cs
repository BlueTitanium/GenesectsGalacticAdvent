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
    private float originalFlySpeed;
    public float rotSpeed = 10f;    
    public float jumpForce = 20f;
    public long score = 0;
    public long multiplier = 1;
    public bool grounded;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public Camera cam;
    private bool leftGround;
    private bool rightGround;
    public bool flying = false;
    public Animator animator;
    public float knockback = 50f;
    public float curTime = .6f;
    public float cdTime = .5f;
    public GameObject trail;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        offset = new Vector3(0,0,0);
        originalFlySpeed = flySpeed;
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {   
        if(curTime < cdTime){
            curTime += Time.deltaTime;
        } else{
            rb3d.velocity = new Vector3(0,rb3d.velocity.y,0);
        }
        leftGround = leftFoot.GetComponent<GroundCheck>().grounded;
        rightGround = rightFoot.GetComponent<GroundCheck>().grounded;
        bool boxGround = transform.GetComponent<GroundCheck>().grounded;
        if(leftGround || rightGround || boxGround) grounded = true;
        else grounded = false;
        if(grounded)animator.SetBool("IsGrounded", true);
        else animator.SetBool("IsGrounded", false);
        if(flying)animator.SetBool("IsFlying", true);
        else animator.SetBool("IsFlying", false);
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");
        if(grounded && (xVal != 0 || zVal != 0))animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);
        float step = rotSpeed * Time.deltaTime;
        if(!flying){
            trail.SetActive(false);
            cam.GetComponent<RPGCamera>().limitYRotation = true;

            if(zVal != 0) transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,new Vector3(cam.transform.forward.x,0f,cam.transform.forward.z),step, 0.0f));

            if(!flying) transform.Translate(new Vector3(xVal, 0, zVal) * groundSpeed);
        }
        float yMove = 0;
        if(Input.GetKey("space") && grounded) yMove = 1;
        rb3d.AddForce(0, jumpForce*yMove, 0);

        if(Input.GetKeyDown("space") && !grounded && !flying){flying = true;rb3d.velocity = new Vector3(0, 0, 0);}
        if(flying) {
            trail.SetActive(true);
            cam.GetComponent<RPGCamera>().limitYRotation = false;
            rb3d.useGravity = false;
            if(Input.GetKey("space")) yMove = 1f;
            if(Input.GetKey("left shift")) yMove = -.1f;
            if(zVal != 0) transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,new Vector3(cam.transform.forward.x,cam.transform.forward.y,cam.transform.forward.z),step, 0.0f));
            transform.Translate(new Vector3(xVal, yMove, zVal) * flySpeed);
        }
        if(grounded && flying) {flying = false;rb3d.useGravity= true;}
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "InnerStar"){
            multiplier *= 2;
            score += 10 * multiplier;
            col.gameObject.SetActive(false);
            flySpeed *= 1.25f;
        }
        if(col.tag == "OuterStar"){
            multiplier *= 2;
            score += 10 * multiplier;
            col.gameObject.SetActive(false);
        }  
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "DeathStar"){
            multiplier = 1;
            score -= 10;
            rb3d.velocity = new Vector3(-transform.forward.x * knockback, 0, -transform.forward.x * knockback); 
            curTime = 0f;
            flySpeed = originalFlySpeed;
            audio.Play();
        }
    }
}
