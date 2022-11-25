using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPointLogic : MonoBehaviour
{
    private Collider _collider;
    private MeshRenderer meshRenderer;

    private AudioSource audioSource;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private GameObject checkPartical;
    
    private void Start()
    {
        _collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aircraft"))
        {
            audioSource.PlayOneShot(checkPointSound);
            _collider.enabled = false;
            meshRenderer.enabled = false;
            GameObject particalIns = Instantiate(checkPartical, transform.position, transform.rotation);
            Destroy(particalIns,1f);
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
