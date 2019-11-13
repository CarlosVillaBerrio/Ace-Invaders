using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaJet : Enemigos
{

    void Awake()
    {
        audioUsar = GetComponent<AudioSource>(); // Para usar los sonidos del avion

    }

    void Update()
    {
        AtaqueKamikaze(); // Funcion para los ataques suicidas del avion
    }

    void AtaqueKamikaze() // Funcion del tipo de ataque del avion
    {
        transform.position += transform.right * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha   

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 2f); // Corrige la altura del jet

        if (transform.localPosition.y <= -140)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 270f, transform.localPosition.z);
        }
    }

    private void OnCollisionEnter(Collision collision) // colisiones que admite el avion
    {
        if (collision.gameObject.name == "BalaNormal")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoNormal;
            if (durabilidad <= 0)
            {
                EfectoMuerteExplosiva();
                CaeAvion();
                Destroy(gameObject, 1f);
            }
        }

        if (collision.gameObject.name == "BalaEspecial")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoEspecial;

            if (durabilidad <= 0)
            {
                EfectoMuerteExplosiva();
                CaeAvion();
                Destroy(gameObject, 1f);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            EfectoMuerteExplosiva();
            CaeAvion();
            Destroy(gameObject, 0.25f);
        }
    }
}
