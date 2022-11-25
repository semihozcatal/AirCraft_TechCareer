using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AircraftLogic : MonoBehaviour
{
    private float yaw;
    private float pitch;
    private float roll;

    private Rigidbody rb;
    private Collider _collider;
    
    public FloatingJoystick floatingJoystick;
    
    public float aircraftSpeed;
    public float gearSpeed;
    [SerializeField] private float yawSpeed;
    [SerializeField] private float rotationSpeed;
    
    private float point;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI finalPoint;

    [SerializeField] private GameManager gameManager;
    
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject explosionEffect;

    private bool isGameFinish = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        TextPoint();
    }
    
    void Update()
    {
        ControlGravity();
        AircraftMovement();
    }

    private void AircraftMovement()
    {
        if (gameManager.gear.value > 0 && isGameFinish == false)
        {
            aircraftSpeed = Mathf.Lerp(aircraftSpeed, gameManager.gear.value,gearSpeed * Time.deltaTime);
            transform.position += transform.forward * (aircraftSpeed * Time.deltaTime);

            //float horizontalInput = Input.GetAxis("Horizontal");
            //float verticalInput = Input.GetAxis("Vertical");
            float verticalInput = floatingJoystick.Vertical;
            float horizontalInput = floatingJoystick.Horizontal;

            yaw += horizontalInput * yawSpeed * Time.deltaTime;
            pitch = Mathf.Lerp(0, 30, Mathf.Abs(-verticalInput)) * Mathf.Sign(-verticalInput);
            roll = Mathf.Lerp(0, 40, Mathf.Abs(horizontalInput)) * -Mathf.Sign(horizontalInput);

            transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll),rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Explosion();
            gameManager.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Explosion();
            gameManager.GameOver();
        }
        
        if (other.CompareTag("Checkpoint"))
        {
            point += 10;
            TextPoint();
        }

        if (other.CompareTag("MissCheckpoint"))
        { 
            point -= 10;
            TextPoint();
        }
        
        if (other.CompareTag("Ceiling"))
        {
            gameManager.Warning();
        }
        
        if (other.CompareTag("Finish"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isGameFinish = true;
            successPanel.SetActive(true);
            FinalPoint();
            
            if (point < 40)
            {
                star1.SetActive(true);
            }
            else if (point >= 40 && point < 80 )
            {
                star1.SetActive(true);
                star2.SetActive(true);
            }
            else if (point >= 80)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ceiling"))
        {
            if (gameManager.isGoingAway == true)
            {
                gameManager.ExitCeiling();
            }
        }
    }
    private void TextPoint()
    {
        pointText.text = "Poınt: " + point;
    }
    private void FinalPoint()
    {
        finalPoint.text = "Your Poınt " + point;
    }
    private void Explosion()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        explosionEffect.SetActive(true);
        transform.gameObject.SetActive(false);
    }
    private void ControlGravity()
    {
        if (gameManager.gear.value > 0)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            _collider.isTrigger = true;
        }
        else
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            _collider.isTrigger = false;
        }
    }
    
}
