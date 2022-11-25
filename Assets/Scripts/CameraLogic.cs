using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private Transform aircraft;
    [SerializeField] private Vector3 offsetCam;
    void LateUpdate()
    {
        transform.position = aircraft.position + offsetCam;
    }
}
