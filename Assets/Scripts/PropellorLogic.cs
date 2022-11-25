using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorLogic : MonoBehaviour
{
    private AircraftLogic aircraftLogic;
    [SerializeField] GameManager gameManager;
    private AudioSource audioSource;
    private float speed;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aircraftLogic = transform.parent.GetComponent<AircraftLogic>();
    }
    private void FixedUpdate()
    {
        if (gameManager.isGameStart == true)
        {
            PropellorSound();
        }
        RotatePropellor();
    }
    private void RotatePropellor()
    {
        speed = aircraftLogic.aircraftSpeed;
        transform.Rotate(0,0,speed);
    }
    private void PropellorSound()
    {
        audioSource.enabled = true;
    }
}
