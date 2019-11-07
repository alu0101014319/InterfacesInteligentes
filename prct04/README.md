# Práctica 4 - Delegados y Eventos

Para llevar a cabo todos los puntos de esta práctica, hemos decidido utilizar un campo abierto con un objeto jugador, y dos objetos diferentes más que interactuarán con nuestro personaje principal. Esta vez, nos hemos decantado por el control en primera persona de nuestro personaje, añadiendo nuestra cámara principal como hija del jugador en la jerarquía del proyecto. Una vez hemos planteado nuestro entorno principal, podemos dar paso a la descripción de cada apartado de la práctica:

# Escenario básico y Agregar dos tipos de GameObject en la escena

Para generar el escenario simplemente hemos generado un _terrain_, al cual le hemos añadido una textura que simula el césped. Además, hemos generado al objeto jugador, el cual es una _capsule_, y también los objetos de tipo A y de tipo B, que son objetos de tipo _sphere_ y _cube_, respectivamente. A los objeto de tipo A le hemos añadido un color azul para dar la sensación de que no son enemigos, justo lo contrario a los objetos de tipo B, ya que les hemos dado un color rojo para dar a entender que ocurrirá algo en contra de nuestro personaje si se acerca a ellos. Dejando al margen a nuestro objeto jugador, de los objetos A y B hay varias instancias en todo el escenario rodeando al jugador.

# Player

Como hemos comentado antes, nuestro objeto jugador es un GameObject de tipo _capsule_ y como hija de este objeto, hemos añadido a la cámara para que dé un efecto de jugar en primera persona. Aparte de esto, nuestro personaje es el objeto al que más scripts se le han incluido para que interactúe de manera correcta con el entorno y los demás objetos.

En primer lugar, el personaje deberá poder moverse en todas las direcciones, además de poder rotar en su propio eje para que la cámara gire con él y así enfoque a otra parte del escenario. 

Script de movimiento del objeto Jugador: 

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

Tras coger el componente _Rigidbody_ del jugador, lo utilizaremos para moverlo en las distintas direcciones mediante la función _AddRelativeForce_ para que el personaje siempre avance en dirección a sus propios ejes y no en la dirección de los ejes de la escena. Ya que al realizar un giro, las teclas de movimiento A, S, W y D no tendrían la dirección que les fue asignada desde un principio. En relación a esto último, para que nuestro personaje pueda girar tanto a la izquierda o a la derecha usaremos la función _MoveRotation_, pero para ello, debemos definir previamente un _Quaternion_ especificando la dirección de giro junto con su velocidad. Tras ello, podremos utilizar nuestro quaternion en MoveRotation y el personaje podrá girar em ambas direcciones, destacando que todo el movimiento programado para nuestro jugador está en el interior de la función _FixedUpdate()_ para que el movimiento sea más fluido y sin interrupciones.

En el siguiente paso nos meteremos ya con el uso de **delegados y eventos**, por ello generaremos un nuevo script que haga de controlador de eventos y así 

