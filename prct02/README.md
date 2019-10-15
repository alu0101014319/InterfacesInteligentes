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
                this.transform.RotateAround(transform.position, Vector3.down, rotationSpeed);
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.RotateAround(transform.position, Vector3.up, rotationSpeed);
            }
        }
    }

Lo primero que declaramos son dos variables, las cuales las utilizaremos para establecer la velocidad de desplazamiento y de rotación. Ambas son declaradas como públicas para que desde la interfaz de Unity podamos modificar los valores sin tener que estar modificando el script cada vez que queramos realizar una prueba.

Tras establecer los valores que creamos adecuados pasamos a la función _Update()_ para que se esté ejecutando constantemente. En este área codificaremos la traslación y la rotación del objeto cuando el usuario presione las teclas habilitadas para ello.
La función _Input_ junto con la función _GetKey_ permite al programa estar a la escucha para ver si el usuario presione la tecla que hemos especificado en la función _GetKey_. En nuestro caso hemos codificado las teclas A, S, W, D para manejar el desplazamiento del cubo. 

Una vez el usuario presione esas teclas, se ejecutará la orden que está dentro del _If_. El cubo consigue desplazarse gracias al _transform_, ya que manipula las coordenadas de los ejes x, y y z. Añadiéndole la función _Translate_ para que realice el dezplazamiento, debemos utilizar el objeto _Vector3_ junto con su dirección (forward, back, left, right) para que solo modifique el eje correspondiente con su movimiento. Dicho objeto debe multiplicarse por la velocidad que establecimos al principio del script y por otra nueva variable, _Time.deltaTime_, la cual va a escalar el tamaño del movimiento por el frame time que nuestra CPU sea capaz de procesar para que parezca que el objeto se mueve a una velocidad regular. Una vez configurado esto, el desplazamiento del cubo ya estaría terminado.

Por otro lado, para realizar la rotación seguiremos el mismo procedimiento excepto por dos diferencias.
La primera de ellas la podemos observar que a parte de pulsar la tecla de dirección antes configurada, también el usuario debe pulsar la tecla Shift para que así el programa diferencie entre la traslación y la rotación utilizando prácticamente las mismas teclas.
La segunda diferencia tiene relación con la función que utiliza _transform_, en este caso hemos utilizado la función _RotateAround_ cuyos parámetros son la posición actual del objeto, el objeto _Vector3_ que vaya a modificar el eje correspondiente para realizar el giro sobre el eje OY, y por último la velocidad de giro que establecimos con anterioridad. 

**Gif de la práctica**

![Gif](prct02/Img/ezgif.com-video-to-gif.gif)
