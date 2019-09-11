using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    public Transform target;

    public float smooth = .125f;
    public Vector3 off;
    private Vector3 vel = Vector3.zero;
    // Start is called before the first frame update
    void Start(){
        off = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 desPos = target.position + off;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, desPos, ref vel,smooth, 2f);
        transform.position = smoothPos;
        transform.LookAt(target.position);
    }
}
