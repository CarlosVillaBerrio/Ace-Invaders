using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJefeFinal : Enemigos
{
    string pasos = "Iniciar";
    int direccionalH = 1; // Variables para determinar diferentes tipos de direcciones al mismo tiempo
    int direccionalV = 1;
    public Text laifu; // Muestra en el UI Canvas la vida del jefe final

    void Awake()
    {
        durabilidad = 2000; // Vida del Jefe Final
        velocidadMovimiento = 200f; // Velocidad de movimiento
        audioUsar = GetComponent<AudioSource>(); // Sonidos del jefe

    }

    void Update()
    {
        Mover();
    }

    void PasoInicial() // Animacion de entrada del jefe
    {
        transform.position += transform.right * (50f) * Time.deltaTime; // Transforma la posicion hacia la derecha    
        if (transform.localPosition.z <= 25)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 25f);
            pasos = "Rebotes";

        }
    }

    void PasoRebotes() // Funcion que hace que el jefe se mueva por la pantalla rebotando
    {

        transform.position += direccionalH * transform.forward * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha  

        if(transform.localPosition.x >= 160f || transform.localPosition.x <= -245f)
        {
            if(transform.localPosition.x >= 160f)
            {
                direccionalH = -1;
            }

            if(transform.localPosition.x <= -245f)
            {
                direccionalH = 1;
            }

        }


        transform.position += direccionalV * transform.up * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha  

        if (transform.localPosition.y >= 123f || transform.localPosition.y <= -48f)
        {
            if(transform.localPosition.y >= 123f)
            {
                direccionalV = -1;
            }

            if(transform.localPosition.y <= -48f)
            {
                direccionalV = 1;
            }
        }
    }

    void Mover() // Determina ls pasos del movimiento del jefe
    {
        switch (pasos)
        {
            case "Iniciar":
                PasoInicial();
                break;
            case "Rebotes":
                PasoRebotes();
                break;
            default:
                break;
        }
    }
    

    private void OnCollisionEnter(Collision collision) // colisiones que acepta el jefe
    {
        if (collision.gameObject.name == "BalaNormal")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoNormal;
            laifu.GetComponent<Text>().text = "Durabilidad: " + durabilidad.ToString();
            
            if (durabilidad <= 0)
            {
                durabilidad = 0;
                laifu.GetComponent<Text>().text = "Durabilidad: " + durabilidad.ToString();

                EfectoMuerteExplosiva();

                Invoke("GoVictory", 0.8f);
                Destroy(gameObject, 1f);
            }
        }

        if (collision.gameObject.name == "BalaEspecial")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoEspecial;
            laifu.GetComponent<Text>().text = "Durabilidad: " + durabilidad.ToString();

            if (durabilidad <= 0)
            {
                durabilidad = 0;
                laifu.GetComponent<Text>().text = "Durabilidad: " + durabilidad.ToString();

                EfectoMuerteExplosiva();

                Invoke("GoVictory", 0.8f); // Se llama la funcion de la victoria del jugador cuando el jefe es destruido
                Destroy(gameObject, 1f);
            }
        }
    }

    void GoVictory() // Funcion que determina la victoria del jugador
    {
        SceneManager.LoadScene("Ganaste");
    }
}