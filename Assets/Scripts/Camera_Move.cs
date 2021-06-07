using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Camera_Move : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private Vector3 _distanceFromObject;

    private void LateUpdate()
    {
        Vector3 positionToGo = _object.transform.position + _distanceFromObject; //Target position of the camera
        Vector3 smoothPosition = Vector3.Lerp(a: transform.position, b: positionToGo, Time.deltaTime); //Smooth position of the camera
        transform.position = smoothPosition;
        transform.LookAt(_object.transform.position);
    }

}
