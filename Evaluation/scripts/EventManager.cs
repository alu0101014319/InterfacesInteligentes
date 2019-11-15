using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void Event();
    public static event Event CollisionedA;
    public static event Event CollisionedB;
    public static event Event CollisionedC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.tag == "A") || (collision.gameObject.tag == "B"))
        {
            if (CollisionedC != null)
            {
                CollisionedC();
            }
        }

        if (CollisionedA != null)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                CollisionedA();
            }
        }

        if (CollisionedB != null)
        {
            if (Input.GetKey(KeyCode.X))
            {
                CollisionedB();
            }
        }
    }
}
