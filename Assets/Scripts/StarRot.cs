using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StarRot : MonoBehaviour
{
    public float x;
    public float z;
    public float speed;
    private float time = 0;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(x, (float)time * speed, z);
    }
}
