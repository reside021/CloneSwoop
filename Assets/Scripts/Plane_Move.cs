using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_Move : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moduleForce = 1;
    [SerializeField] private GameObject _gameOverUi;
    private float radius;
    private float angle = 0f;
    private Vector3 defaultPosition;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = _plane.transform.position;
        radius = Mathf.Sqrt(Mathf.Pow(_plane.transform.position.x, 2) + Mathf.Pow(_plane.transform.position.y-22, 2) + Mathf.Pow(_plane.transform.position.z, 2));
    }

    private void FixedUpdate()
    {
        var x = Mathf.Cos(angle * _speed) * radius;
        var z = Mathf.Sin(angle * _speed) * radius;
        transform.position = new Vector3(x, _plane.transform.position.y, z);
        angle = angle + Time.deltaTime * _speed;

        if (angle > 360f)
        {
            angle = 0f;
        }

        if (_plane.transform.position.y > 36f)
        {
            _rigidbody.velocity = Vector3.down;
        }
        else 
        {
            if (_plane.transform.position.y <= 29f)
            {
                if (Input.GetMouseButton(0))
                {
                    _rigidbody.AddForce(Vector3.up * _moduleForce);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Ground")
        {
            //_plane.transform.position = defaultPosition;
            Time.timeScale = 0f;
            _gameOverUi.SetActive(true);
        }
    }

}
