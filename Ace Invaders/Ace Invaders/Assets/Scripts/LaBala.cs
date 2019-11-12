using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaBala : MonoBehaviour
{
    public int dañoNormal; // Daño del proyectil
    public int dañoEspecial; // Daño del proyectil
    public ParticleSystem explosionBalaEspecial;
    public AudioSource audioUsar;
    public AudioClip explosionBalaEspecialSonido;

    private void Start()
    {
        explosionBalaEspecial = FindObjectOfType<LogicaJugador>().explosionBalaEspecial;
        audioUsar = FindObjectOfType<LogicaJugador>().audioUsar;
        explosionBalaEspecialSonido = FindObjectOfType<LogicaJugador>().explosionBalaEspecialSonido;
    }   

    void efectoBalaSeDestruye()
    {
        explosionBalaEspecial.transform.position = transform.position;
        audioUsar.PlayOneShot(explosionBalaEspecialSonido);
        explosionBalaEspecial.Stop();
        explosionBalaEspecial.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(gameObject.name == "BalaEspecial")
            {
                efectoBalaSeDestruye();
                Destroy(gameObject, 0.1f);
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }

        }

        if (collision.gameObject.tag == "Player")
        {
            if(gameObject.name == "BalaEspecial")
            {
                efectoBalaSeDestruye();
                Destroy(gameObject, 0.1f);
            }
            else
            {
                Destroy(gameObject, 0.1f);
            }

        }
    }
}
