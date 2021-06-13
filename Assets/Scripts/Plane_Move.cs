using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane_Move : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moduleForce = 1;
    [SerializeField] private GameObject _gameOverUi;
    [SerializeField] private GameObject _propeller;
    [SerializeField] private GameObject _fuelProgressBar;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    private float radius;
    private float angle = 0f;
    private Vector3 defaultPosition;
    float x0, z0, x1, z1;
    private float fuelSize = 100f;
    private float currentFuel;
    private GameObject fillGameObject;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = _plane.transform.position;
        radius = Mathf.Sqrt(Mathf.Pow(_plane.transform.position.x, 2) + Mathf.Pow(_plane.transform.position.y-22, 2) + Mathf.Pow(_plane.transform.position.z, 2));
        currentFuel = fuelSize;
        fillGameObject = GameObject.Find("Fill Area");
    }

    private void FixedUpdate()
    {
        if(currentFuel <= 0)
        {
            fillGameObject.SetActive(false);
        }
        _slider.value = currentFuel;
        //x0 = transform.position.x;
        //z0 = transform.position.z;
        x1 = Mathf.Cos(angle * _speed) * radius;
        z1 = Mathf.Sin(angle * _speed) * radius;
        transform.position = new Vector3(x1, _plane.transform.position.y, z1);
        angle = angle + Time.deltaTime * _speed;
        _propeller.transform.Rotate(Vector3.right, Time.deltaTime * 1000f, Space.Self);

        _fill.fillAmount = _slider.value;
        //_fuelProgressBar.transform.localScale = new Vector3(currentFuel / 100, _fuelProgressBar.transform.localScale.y, _fuelProgressBar.transform.localScale.z);
        //print(currentFuel / 100);
        //var tan0 = z0 / x0;
        //var tan1 = z1 / x1;
        //var kek = Mathf.Atan((tan0 - tan1) / (1 + tan0 * tan1));
        //transform.Rotate(Vector3.down, kek, Space.Self);
        // поворот самолета по ссвоей оси вместо lookat (НЕРАБОТАЕТ)

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
                if(currentFuel > 0)
                {
                    if (Input.GetMouseButton(0))
                    {
                        currentFuel -= 8 * Time.deltaTime;
                        _rigidbody.AddForce(Vector3.up * _moduleForce);
                    }
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
