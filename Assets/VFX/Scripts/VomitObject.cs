using System;
using UnityEngine;

public class VomitObject : MonoBehaviour
{
    [SerializeField] private GameObject vomitSplash;
    [SerializeField] private Vector3 vomitDirectionAdjustment;
    [SerializeField] private float vomitForce;
    
    Rigidbody _rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartVomit(Vector3 direction)
    {
        _rb.AddForce((direction + vomitDirectionAdjustment) * vomitForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        Instantiate(vomitSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
