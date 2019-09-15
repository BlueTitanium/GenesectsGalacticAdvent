using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 offset;
    private Rigidbody rb3d;
    public float sensitivity;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        offset = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, sensitivity * Input.GetAxis("Mouse X"),0);
        
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xVal, 0, zVal)*speed);
    }
}
