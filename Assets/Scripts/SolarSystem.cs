using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public float speed = 6f;

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        transform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * speed, 0f);
    }
}
