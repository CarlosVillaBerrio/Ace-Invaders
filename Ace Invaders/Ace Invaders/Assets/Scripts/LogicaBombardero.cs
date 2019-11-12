using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaBombardero : Enemigos
{
    string pasos = "Iniciar";
    int direccional = 1;
    float contador = 0f;
    float duracionBalaEnEscena = 1.5f;
    
    void Awake()
    {
        durabilidad = 200;
        velocidadMovimiento = 40f;
        audioUsar = GetComponent<AudioSource>();
    }

    void Update()
    {
        Mover();
        Atacar();
    }

    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }

    void EfectoDisparo()
    {
        fuegoDeArma.Stop();
        fuegoDeArma.Play();
    }

    void EfectoDisparoEspecial()
    {
        fuegoDeArmaEspecial.Stop();
        fuegoDeArmaEspecial.Play();
    }

    void TiroNormalR()
    {
        audioUsar.PlayOneShot(disparoNormal);
        tiempoNoDisparo = true;
        EfectoDisparo();
        GameObject balaInstance;

        balaInstance = Instantiate(balaNormal3, lanzador1.position, Quaternion.identity);

        balaInstance.AddComponent<Rigidbody>().AddForce(lanzador1.right * 100 * velocidadProyectilNormal);
        balaInstance.name = "BalaEspecial";
        balaInstance.AddComponent<LaBala>().dañoNormal = dañoNormal3;

        StartCoroutine(ReiniciarTiempoNoDisparo());
        Destroy(balaInstance, duracionBalaEnEscena);
    }

    void TiroNormalL()
    {
        audioUsar.PlayOneShot(disparoNormal);
        tiempoNoDisparo = true;
        EfectoDisparo();
        GameObject balaInstance;

        balaInstance = Instantiate(balaNormal3, lanzador2.position, Quaternion.identity);

        balaInstance.AddComponent<Rigidbody>().AddForce(lanzador2.right * 100 * velocidadProyectilNormal);
        balaInstance.name = "BalaEspecial";
        balaInstance.AddComponent<LaBala>().dañoNormal = dañoNormal3;

        StartCoroutine(ReiniciarTiempoNoDisparo());
        Destroy(balaInstance, duracionBalaEnEscena);
    }

    void PasoInicial()
    {
        transform.position += transform.right * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha    
        if (transform.localPosition.y <= 180)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 180f, transform.localPosition.z);
            pasos = "Ejecucion";

        }
    }

    void PasoEjecucion()
    {
        transform.position += direccional * transform.up * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha  
        contador += Time.deltaTime;
        if (contador > 2f)
        {
            direccional *= -1;
            contador = 0f;
        }
    }

    void Mover()
    {
        switch (pasos)
        {
            case "Iniciar":
                PasoInicial();
                break;
            case "Ejecucion":
                PasoEjecucion();
                break;
            default:
                break;
        }
    }

    void Atacar()
    {
        if (tiempoNoDisparo) return;

        if (transform.localPosition.y <= 180)
        {
            ritmoDeDisparo = cadenciaNormal;
            TiroNormalR();
            TiroNormalL();
        }
    }

    private void OnCollisionEnter(Collision collision)    
    {
        if (collision.gameObject.name == "BalaNormal")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoNormal;
            if (durabilidad <= 0)
            {
                EfectoMuerteExplosiva();
                CaeAvion();

                Destroy(gameObject,1f);
            }
        }

        if (collision.gameObject.name == "BalaEspecial")
        {
            durabilidad -= collision.gameObject.GetComponent<LaBala>().dañoEspecial;

            if (durabilidad <= 0)
            {
                EfectoMuerteExplosiva();
                CaeAvion();

                Destroy(gameObject,1f);
            }
        }
    }
}
