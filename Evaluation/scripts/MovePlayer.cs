using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private float moveSpeed = 7000f;
    private float roteSpeed = 50f;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddRelativeForce(Vector3.left * moveSpeed);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddRelativeForce(Vector3.right * moveSpeed);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * Time.deltaTime * roteSpeed);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            Quaternion deltaRotation = Quaternion.Euler(Vector3.down * Time.deltaTime * roteSpeed);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
