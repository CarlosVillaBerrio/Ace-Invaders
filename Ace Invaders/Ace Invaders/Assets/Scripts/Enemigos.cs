using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    [Header("Atributos Del Enemigo")]
    public int durabilidad; // Nivel de resistencia del enemigo
    public float velocidadMovimiento; // Velocidad con la que se mueve el avion enemigo

    [Header("Efecto De Disparo")]
    public ParticleSystem fuegoDeArma; // Efecto de particulas de disparo normal
    public ParticleSystem fuegoDeArmaEspecial; // Efecto de particulas de disparo especial

    [Header("Efecto De Destruido")]
    public ParticleSystem explosionMuere; // Efecto de particulas de disparo normal
    public AudioClip explosionMuerteSonido;
    // Recurso para usar los sonidos
    public AudioSource audioUsar;

    [Header("Lanzador De La Bala")]
    public Transform lanzador1;
    public Transform lanzador2;

    [Header("Efectos De Sonido")]
    public AudioClip disparoNormal; // Efecto de sonido para el bombardero
    public AudioClip disparoEspecial; // Efecto de sonido para el jefe
    public AudioClip impactoNormal; // Efecto de sonido para el bombardero
    public AudioClip impactoEspecial; // Efecto de sonido para el jefe
    public AudioClip explosionEnemigo; // Efecto de sonido cuando le dan al enemigo

    [Header("Bala Normal")]
    public GameObject balaNormal3; // Prefab del proyectil
    public float velocidadProyectilNormal; // Velocidad a la que viaja el proyectil
    public float cadenciaNormal; // Tiempo entre disparos
    public int dañoNormal3;

    [Header("Bala Especial")]
    public GameObject BalaEspecial; // Prefab del proyectil
    public float velocidadProyectilEspecial; // Velocidad a la que viaja el proyectil
    public float cadenciaEspecial; // Tiempo entre disparos

    [Header("Restricciones / Reglas Para Armas")]
    public bool tiempoNoDisparo = false;
    public float ritmoDeDisparo;
    public float tiempoDespliegue;
    public float hordaAnterior = 1;

    public bool estadoMuerto = false;



    void Start()
    {
        audioUsar = GetComponent<AudioSource>(); // Obtiene el audiosource para los sonidos de las naves enemigas
    }

    public void EfectoMuerteExplosiva() // Efectos audiovisuales de retroalimentacion de los enemigos
    {
        audioUsar.PlayOneShot(explosionMuerteSonido);
        explosionMuere.Stop();
        explosionMuere.Play();
    }

    public void CaeAvion() // Funcion que permite contar los enemigos destruidos
    {
        if (!estadoMuerto)
        {
            FindObjectOfType<ControladorEnemigos>().ComprobarOla();
            estadoMuerto = true; // variable auxiliar para que no cuente varias veces al mismo muerto
        }
    }
}
