using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    public float speed = 6f;

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        transform.localRotation = Quaternion.Euler((float)time.TotalMinutes * speed, -140f, 70f);
    }
}