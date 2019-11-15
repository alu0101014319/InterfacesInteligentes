using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ObjectC : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 500f;
    private float pow;
    private bool check = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        pow = ActionPlayer.power;
    }

    void OnEnable()
    {
        EventManager.CollisionedC += Move;
    }

    void OnDisable()
    {
        EventManager.CollisionedA -= Move;
    }
    
    void Move()
    {
        if (check == false)
        {
            rb.AddForce(Vector3.forward * pow * moveSpeed);
            check = true;
        }
        else
        {
            rb.AddForce(Vector3.back * pow * moveSpeed);
            check = false;
        }
        
        
    }
}
