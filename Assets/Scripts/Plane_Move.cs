using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Move : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private float _speed = 0.5f;
    private float radius;
    private float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        radius = Mathf.Sqrt(Mathf.Pow(_plane.transform.position.x, 2) + Mathf.Pow(_plane.transform.position.y, 2) + Mathf.Pow(_plane.transform.position.z, 2));
    }

    // Update is called once per frame
    void Update()
    {

        var x = Mathf.Cos(angle * _speed) * radius;
        var z = Mathf.Sin(angle * _speed) * radius;
        transform.position = new Vector3(x, _plane.transform.position.y, z);
        angle = angle + Time.deltaTime * _speed;

        if (angle > 360f)
        {
            angle = 0f;
        }

        //Quaternion rotation = Quaternion.AngleAxis(angleCub, Vector3.up);
        //transform.rotation = rotation;
    }

}


//var oldCoord = new Vector3(_plane.transform.position.x, _plane.transform.position.y, _plane.transform.position.z);
//float dist = Vector3.Distance(newCoord, oldCoord);
//var angleCub = Mathf.Sin(dist / radius);