using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plane_Move : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moduleForce = 1;
    [SerializeField] private GameObject _gameOverUi;
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private GameObject _propeller;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private GameObject [] _preset;
    private int score = 0;
    private int countCircle = 0;
    private float radius;
    private float angle = 0f;
    private Vector3 defaultPosition;
    private float currentFuel;
    private GameObject fillGameObject;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        angle = 0f;
        defaultPosition = _plane.transform.position;
        radius = Mathf.Sqrt(Mathf.Pow(_plane.transform.position.x, 2) + Mathf.Pow(_plane.transform.position.y-22, 2) + Mathf.Pow(_plane.transform.position.z, 2));
        currentFuel = 100f;
        fillGameObject = GameObject.Find("Fill Area");
    }

    private void FixedUpdate()
    {
        if (currentFuel <= 0)
        {
            fillGameObject.SetActive(false);
        }
        _slider.value = currentFuel;
        var x = Mathf.Cos(angle * _speed) * radius;
        var z = Mathf.Sin(angle * _speed) * radius;
        transform.position = new Vector3(x, _plane.transform.position.y, z);
        angle = angle + Time.deltaTime * _speed;
        _propeller.transform.Rotate(Vector3.right, Time.deltaTime * 1000f, Space.Self);
        _fill.fillAmount = _slider.value;

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
                if (currentFuel > 0)
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
            _gameOverUi.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = score.ToString();
            _gameOverUi.SetActive(true);
            SaveGame();
        }
    }

    void SaveGame()
    {
        int lastNote = 0;
        if (PlayerPrefs.HasKey("lastNote"))
        {
            lastNote = PlayerPrefs.GetInt("lastNote");
            if (lastNote == 5)
            {
                lastNote = 0;
            }
            else
            {
                lastNote++;
            }
        }
        var today = DateTime.Now;
        string record = today + "&" + score;
        string numRow = $"R{lastNote}";
        PlayerPrefs.SetString(numRow, record);
        PlayerPrefs.SetInt("lastNote", lastNote); ;
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cloud") {
            other.gameObject.SetActive(false);
            currentFuel -= 20;
        }

        if (other.gameObject.tag == "Star")
        {
            score += 2;
            other.gameObject.SetActive(false);
            _textScore.text = score.ToString();
        }

        if (other.gameObject.tag == "StarFuel")
        {
            currentFuel += 20;
            if (currentFuel > 100) 
            {
                currentFuel = 100;
            }
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Cube")
        {
            if (_preset[0].activeSelf == true && countCircle == 0) {
                _preset[0].SetActive(false);
                _preset[1].SetActive(true);
            }
            if (_preset[1].activeSelf == true && countCircle == 1)
            {
                _preset[1].SetActive(false);
                _preset[2].SetActive(true);
            }
            if (_preset[2].activeSelf == true & countCircle == 2)
            {
                for (int i = 0; i < _preset[2].transform.childCount; i++)
                {
                    _preset[2].transform.GetChild(i).gameObject.SetActive(true);
                }
                countCircle--;
            }
            countCircle++;
        }
    }
}
