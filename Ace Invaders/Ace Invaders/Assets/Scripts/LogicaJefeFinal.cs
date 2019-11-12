using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJefeFinal : Enemigos
{
    string pasos = "Iniciar";
    int direccionalH = 1;
    int direccionalV = 1;
    float contador = 0f;
    public Text laifu;

    void Awake()
    {
        durabilidad = 2000;
        velocidadMovimiento = 200f;
        audioUsar = GetComponent<AudioSource>();

    }

    void Update()
    {
        Mover();
    }

    void PasoInicial()
    {
        transform.position += transform.right * (50f) * Time.deltaTime; // Transforma la posicion hacia la derecha    
        if (transform.localPosition.z <= 25)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 25f);
            pasos = "Rebotes";

        }
    }

    void PasoRebotes()
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

    void Mover()
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
    

    private void OnCollisionEnter(Collision collision)
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

                Invoke("GoVictory", 0.8f);
                Destroy(gameObject, 1f);
            }
        }
    }

    void GoVictory()
    {
        SceneManager.LoadScene("Ganaste");
    }
}