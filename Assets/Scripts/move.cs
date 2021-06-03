using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        double x = 2.3 * Mathf.Cos(Time.deltaTime);
        double z = 2.3 * Mathf.Sin(Time.deltaTime);
        Vector3 vector3 = new Vector3((float)x, 0.3f, (float)z);
        camera.transform.position = vector3;
    }
}
