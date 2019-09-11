using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb3d;
    
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xVal = Input.GetAxis("Horizontal");
        float zVal = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(xVal, 0, zVal)*speed);
    }
}
