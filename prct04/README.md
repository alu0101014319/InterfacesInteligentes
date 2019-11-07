# Práctica 4 - Delegados y Eventos

Para llevar a cabo todos los puntos de esta práctica, hemos decidido utilizar un campo abierto con un objeto jugador, y dos objetos diferentes más que interactuarán con nuestro personaje principal. Esta vez, nos hemos decantado por el control en primera persona de nuestro personaje, añadiendo nuestra cámara principal como hija del jugador en la jerarquía del proyecto. Una vez hemos planteado nuestro entorno principal, podemos dar paso a la descripción de cada apartado de la práctica:

# Escenario básico y Agregar dos tipos de GameObject en la escena

Para generar el escenario simplemente hemos generado un _terrain_, al cual le hemos añadido una textura que simula el césped. Además, hemos generado al objeto jugador, el cual es una _capsule_, y también los objetos de tipo A y de tipo B, que son objetos de tipo _sphere_ y _cube_, respectivamente. A los objeto de tipo A le hemos añadido un color azul para dar la sensación de que no son enemigos, justo lo contrario a los objetos de tipo B, ya que les hemos dado un color rojo para dar a entender que ocurrirá algo en contra de nuestro personaje si se acerca a ellos. Dejando al margen a nuestro objeto jugador, de los objetos A y B hay varias instancias en todo el escenario rodeando al jugador.

# Player

Como hemos comentado antes, nuestro objeto jugador es un GameObject de tipo _capsule_ y como hija de este objeto, hemos añadido a la cámara para que dé un efecto de jugar en primera persona. Aparte de esto, nuestro personaje es el objeto al que más scripts se le han incluido para que interactúe de manera correcta con el entorno y los demás objetos.

En primer lugar, el personaje deberá poder moverse en todas las direcciones, además de poder rotar en su propio eje para que la cámara gire con él y así enfoque a otra parte del escenario. 

**Script de movimiento del objeto Jugador: **

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

En el siguiente paso nos meteremos ya con el uso de **delegados y eventos**, por ello generaremos un nuevo script que haga de controlador de los distintos eventos que manejaremos en nuestro proyecto, debido a que los eventos solo se activarán cuando el jugador colisione con uno de los dos tipos de objetos del escenario. 

**Script del controlador de eventos:**

        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class EventManager : MonoBehaviour
        {
            public delegate void Event();
            public static event Event CollisionedA;
            public static event Event CollisionedB;
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
                if(collision.gameObject.tag == "A")
                {
                    if (CollisionedA != null)
                    {
                        CollisionedA();
                    }

                }

                if (collision.gameObject.tag == "B")
                {
                    if (CollisionedB != null)
                    {
                        CollisionedB();
                    }

                }
            }
        }


Lo primero que debemos hacer declarar un delegado para luego convertirlo en un evento, ya que un evento es un tipo de delegado. Tras ello, declaramos los dos tipos de eventos que vamos a gestionar: colisiones con el objeto A y colisiones con el objeto B. Una vez declarados, tenemos que establecer la condición para que dichos eventos sean lanzados automáticamente. Por esa razón, como la condición para ambos eventos es la misma, es decir, deben ejecutarse cuando el objeto jugador entre en colisión con alguno de tipo A o B, establecemos dicha condición dentro de la función _OnCollisionEnter()_. Una vez detecte una colisión con el objeto de tipo A o con el objeto B, realizará lo establecido en el siguiente script.

**Script con las acciones a realizar por el personaje cuando se lance un evento:**

        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.UI;

        public class ActionPlayer : MonoBehaviour
        {
            public static float power = 0;
            public Text texto;
            void Start()
            {

            }

            void Update()
            {
                SetPower();
            }

            void OnEnable()
            {
                EventManager.CollisionedA += Increase;
                EventManager.CollisionedB += Decrement;
            }

            void OnDisable()
            {
                EventManager.CollisionedA -= Increase;
                EventManager.CollisionedB -= Decrement;
            }

            void Increase()
            {
                if(power < 2)
                {
                    power = power + 1;
                }   
            }

            void Decrement()
            {
                if(power > 0)
                {
                    power = power - 1;
                }
            }

            void SetPower()
            {
                texto.text = "Power: " + power.ToString();
            }
        }

Para suscribir una acción a nuestro gestionador de eventos y más en concreto, a alguno de los dos eventos que hemos declarado debemos declararlo dentro de la función _OnEnable()_. De esta manera mediante el operador += podemos añadir más de una acción a nuestros eventos. Como buena práctica, se recomienda que de la misma manera que tenemos una función para suscribir acciones, debemos tener otra para desuscribirlos, _OnDisable()_. Como queremos que haya diferencia entre colisionar con un objeto A y uno B, hemos declarado dos eventos diferentes, ya que si hubiéramos suscrito todas las acciones al mismo evento, se activarían las acciones indistintamente de cuando chocáramos con un objeto A o B.

Al suscribir una acción, debemos declarar un método que reciba y devuelva los mismos tipos de datos que nuestro delegado. Por ello, hemos declarado de esa manera las funciones _Increase()_ y _Decrement()_, las cuales aumentan o disminuyen el poder de nuestro jugador en cada colisión. Por otra parte, nos encontramos con una función _SetPower()_, cuya funcionalidad es permitirnos visualizar por pantalla el poder actual que tiene nuestro jugador.


# Objeto de tipo B

Como en el guión nos han pedido que cuando choquemos con un objeto de este tipo nos disminuya el poder, pero también debe realizar una transformación en los objetos de este tipo, hemos decidido añadirle un script específico para ellos.

**Script específico para los objetos de tipo B:**

        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class Object_B : MonoBehaviour
        {
            private Vector3 duplicar = new Vector3(5, 5, 5);
            private float pow;
            private bool check = false;
            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                pow = ActionPlayer.power;
            }

            void OnEnable()
            {
                EventManager.CollisionedB += Scale;
            }

            void OnDisable()
            {
                EventManager.CollisionedB -= Scale;
            }

            void Scale()
            {
                if(pow == 0 && check == false)
                {
                    transform.localScale += duplicar;
                    check = true;
                }
                if(pow == 1 && check == true)
                {
                    transform.localScale -= duplicar;
                    check = false;
                }
            }
        }


Lo único que realizamos en este script es realizar un cambio de escala en nuestros objetos de tipo B, es decir, cuando el jugador no tiene puntos de poder, los cuales los recibe colisionando con objetos de tipo A, y colisiona con los tipo B, este aumenta su escala duplicando su tamaño. Sin embargo, cuando el jugador ha conseguido un punto de poder, al colisionar estos objetos B vuelven a su estado original disminuyendo su escala nuevamente. Para ello, seguiremos la metodología utilizada en los anteriores scripts, es decir, para suscribir una acción al evento lo realizamos mediante la función _OnEnable()_ y aparte definimos una función _Scale()_ para realizar la transformación en nuestros objetos B.


# Lights

Por último, para finalizar el proyecto hemos decidido añadirle a cada objeto A, un _spotlight_ como hijo en su jerarquía dentro del proyecto. De esta manera, podemos llevar a cabo el último punto de nuestro guión, el cual nos impone que debemos añadir algún elemento de luz al escenario y que apretando una tecla se encienda y se apague. Aunque nosotros hemos añadido otra funcionalidad más, la cual podremos ver a continuación:

**Script para los elementos de luz:**

        using System;
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        public class Lights : MonoBehaviour
        {
            Light spotl;
            private float pow;
            // Start is called before the first frame update
            void Start()
            {
                spotl = this.GetComponent<Light>();
                spotl.enabled = false;

            }

            // Update is called once per frame
            void Update()
            {
                 if (Input.GetKey(KeyCode.R))
                 {
                    if (spotl.enabled == false)
                    {
                        spotl.color = Color.blue;
                        spotl.enabled = true;
                    }
                    else
                    {
                        spotl.enabled = false;
                    }
                }
            }

            void OnEnable()
            {
                EventManager.CollisionedB += Lights_;
            }

            void OnDisable()
            {
                EventManager.CollisionedB -= Lights_;
            }

            void Lights_()
            {
                spotl.enabled = true;
                spotl.color = Color.green;
            }
        }


En dicho script podemos distinguir dos funcionalidades claras. Podemos comprobar que dentro de la función _Update()_ se mantiene escuchando a ver si el usuario presiona la tecla R y así encender los focos de color azul, así como apagarlos en el caso de que ya estuvieran encendidos.

Sin embargo, podemos apreciar que hemos añadido una nueva acción al evento que gestionaba las colisiones con los objetos B. Esto se debe a que hemos decidido que cuando el jugador colisione con uno de estos objetos, inmediatamente se enciendan los focos, pero esta vez de color verde mediante el predicado _Enabled_, que poseen los objetos de tipo A como señal que debe incrementar su poder para poder enfrentarse a los objetos B.

# Gif del Proyecto

![Gif1](img/prct04-1.gif)
![Gif2](img/prct04-2.gif)
