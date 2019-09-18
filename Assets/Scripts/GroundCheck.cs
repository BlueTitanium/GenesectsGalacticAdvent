using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool grounded;
    public LayerMask layer;
    void Update()
    {
        if(!Physics.CheckCapsule(GetComponent<Collider>().bounds.center,
                                new Vector3(GetComponent<Collider>().bounds.center.x,
                                GetComponent<Collider>().bounds.min.y-0.1f,
                                GetComponent<Collider>().bounds.center.z),
                                0.25f,
                                layer))
            grounded = false;
        else grounded = true;
    }
}
