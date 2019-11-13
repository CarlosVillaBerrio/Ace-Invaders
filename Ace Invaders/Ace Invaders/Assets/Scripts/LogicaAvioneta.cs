using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaAvioneta : Enemigos
{
    float randomY;
    float randomX;

    void Awake()
    {
        audioUsar = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        AtaqueAvioneta(); // Se mueve de un lado a otro
    }

    void AtaqueAvioneta()
    {
        transform.position += transform.right * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha   

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 14f); // Corrige la altura del jet

        if (transform.localPosition.x >= 250f)
        {
            randomX = Random.Range(-500f, -360f); // Establece las posiciones iniciales al terminar de recorrer la paantalla
            randomY = Random.Range(-40f, 200f);
            transform.localPosition = new Vector3(randomX, randomY, transform.localPosition.z);
        }
    }

    private void OnCollisionEnter(Collision collision) // Tipos de collisiones que acepta el avion
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