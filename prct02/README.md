**Práctica 2 - Introducción a los Scripts**

Para llevar a cabo la práctica dos del curso debemos conseguir que un objeto 3D avance en el plano en todas las direcciones posibles, además de que pueda rotar sobre su eje OY mediante el uso de un Script en C#.

En primer lugar, generamos dos objetos en el plano: un terreno y un cubo 3D. Una vez seleccionamos el cubo, le añadiremos un script al cual hemos llamado _TranslationRotation_ donde programamos las acciones que debe realizar el cubo.

_Contenido del Script TranslationRotation_:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1f;
    public float rotationSpeed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Translation
        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)) {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        //Rotation
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.RotateAround(transform.position, Vector3.up, rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            this.transform.RotateAround(transform.position, Vector3.down, rotationSpeed);
        }
    }
}

