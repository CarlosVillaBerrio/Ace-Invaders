using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    [Header("Atributos Del Jugador")]
    public int vidas; // Veces que puede morir el jugador
    public float tiempoInvulnerable = 3f; // Despues de morir, tiempo de invencibilidad
    public float velocidadMovimiento; // Velocidad con la que se mueve el avion del jugador

    [Header("Efecto De Disparo")]
    public ParticleSystem fuegoDeArma; // Efecto de particulas de disparo normal
    public ParticleSystem fuegoDeArmaEspecial; // Efecto de particulas de disparo especial

    [Header("Efecto De Bala Destruida")]
    public ParticleSystem explosionBalaEspecial;
    public AudioClip explosionBalaEspecialSonido;

    // Recurso para usar los sonidos
    public AudioSource audioUsar;

    [Header("Efectos De Sonido")]
    public AudioClip disparoNormal; // Efecto de sonido cuando el jugador usa el arma normal
    public AudioClip disparoEspecial; // Efecto de sonido cuando el jugador usa el arma especial
    public AudioClip impactoNormal; // Efecto de sonido cuando la bala normal impacta
    public AudioClip impactoEspecial; // Efecto de sonido cuando la bala especial impacta
    public AudioClip explosionJugador; // Efecto de sonido cuando le dan al jugador

    [Header("Efecto De Disparo")]
    public ParticleSystem explosionJugadorP;


    [Header("Lanzador De La Bala")]
    public Transform lanzador;

    [Header("Bala Normal")]
    public GameObject balaNormal; // Prefab del proyectil
    public float velocidadProyectilNormal; // Velocidad a la que viaja el proyectil
    public float cadenciaNormal; // Tiempo entre disparos
    public int dañoNormal; // Daño del proyectil

    [Header("Bala Especial")]
    public GameObject balaEspecial; // Prefab del proyectil
    public float velocidadProyectilEspecial; // Velocidad a la que viaja el proyectil
    public float cadenciaEspecial; // Tiempo entre disparos
    public int dañoEspecial; // Daño del proyectil

    [Header("Restricciones / Reglas Para Armas")]
    public bool tiempoNoDisparo = false;
    public float ritmoDeDisparo;
    GameObject balaInstanceS;
    // Esquema de puntos en la escena
    
    [Header("Arreglos para la camara")]
    Vector3 camaraPantallaXYPlus; // +x +y 
    float offsetX = 16f;
    float offsetY = 12f;
    Transform theTransform;

    [Header("Escudo AntiImpactos")]
    public GameObject proteccion;

    [Header("Textos de la interfaz")]
    public Text laifu;

    void Start()
    {
        audioUsar = GetComponent<AudioSource>();
        theTransform = GetComponent<Transform>();
        camaraPantallaXYPlus = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        proteccion.SetActive(false);
    }


    void Update()
    {
        MovimientoAvion();
        Disparar();
    }

    

    IEnumerator ReiniciarTiempoNoDisparo()
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }

    void Disparar()
    {
        if (tiempoNoDisparo) return;


        if (Input.GetKey(KeyCode.F))
        {
            ritmoDeDisparo = cadenciaNormal;
            TiroNormal();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ritmoDeDisparo = cadenciaEspecial;
            TiroEspecial();
        }

    }

    void efectoBalaSeDestruye()
    {
        if(balaInstanceS != null)
        {
            explosionBalaEspecial.transform.position = balaInstanceS.transform.position;
            audioUsar.PlayOneShot(explosionBalaEspecialSonido);
            explosionBalaEspecial.Stop();
            explosionBalaEspecial.Play();
        }
        
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
    
    void TiroNormal()
    {
        audioUsar.PlayOneShot(disparoNormal);
        tiempoNoDisparo = true;
        EfectoDisparo();
        GameObject balaInstance;

        balaInstance = Instantiate(balaNormal, lanzador.position, Quaternion.identity);

        balaInstance.AddComponent<Rigidbody>().AddForce(lanzador.up * -100 * velocidadProyectilNormal);
        balaInstance.name = "BalaNormal";
        balaInstance.AddComponent<LaBala>().dañoNormal = dañoNormal;

        StartCoroutine(ReiniciarTiempoNoDisparo());
        Destroy(balaInstance, 1f);
    }

    void TiroEspecial()
    {
        audioUsar.PlayOneShot(disparoEspecial);
        tiempoNoDisparo = true;
        EfectoDisparoEspecial();

        balaInstanceS = Instantiate(balaEspecial, lanzador.position, Quaternion.identity);

        balaInstanceS.AddComponent<Rigidbody>().AddForce(lanzador.up * -100 * velocidadProyectilEspecial);
        balaInstanceS.name = "BalaEspecial";
        balaInstanceS.AddComponent<LaBala>().dañoEspecial = dañoEspecial;

        StartCoroutine(ReiniciarTiempoNoDisparo());
        Invoke("efectoBalaSeDestruye", 2f);
        Destroy(balaInstanceS, 2f);
    }

    /// <summary>
    /// Perdida de vida cuando recibe un ataque de los enemigos
    /// </summary>
    /// <param name="collision"></param>

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.name == "BalaEspecial") && !proteccion.activeSelf)
        {
            vidas--;
            laifu.GetComponent<Text>().text = "Vidas: " + vidas.ToString();
            if(vidas > 0)
            {
                ActivacionProteccion();
            }
            else
            {

                AnimacionMuerteAvion();
                Invoke("muerteAvion", 1f); // AQUI TERMINA EL JUEGO POR RECIBIR MUCHO DAÑO

            }
            print("Aviso Aqui OUCH!!!");
        }
    }

    void ActivacionProteccion()
    {
        if(!proteccion.activeSelf)
        {
            proteccion.SetActive(true);
        }

        Invoke("DesactivadorDeEscudo", tiempoInvulnerable);
    }

    void DesactivadorDeEscudo()
    {
        proteccion.SetActive(false);
    }

    void AnimacionMuerteAvion()
    {
        audioUsar.PlayOneShot(explosionJugador);
        explosionJugadorP.Stop();
        explosionJugadorP.Play();
    }

    void muerteAvion()
    {
        if(vidas <= 0)
        {
            SceneManager.LoadScene("Moriste");
        }
    }

    void MovimientoAvion()
    {
        // transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (Input.GetKey(KeyCode.W)) // Condicion para moverse hacia adelante con la tecla W
        {            
            transform.position -= transform.right * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la derecha

        }
        if (Input.GetKey(KeyCode.S)) // Condicion para moverse hacia atras con la tecla S
        {            
            transform.position += transform.right * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia la izquierda            
        }
        if (Input.GetKey(KeyCode.A)) // Condicion para moverse hacia la izquierda con la tecla A
        {
            transform.position += transform.up * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia atras            

        }
        if (Input.GetKey(KeyCode.D)) // Condicion para moverse hacia la derecha con la tecla D
        {            
            transform.position -= transform.up * (velocidadMovimiento) * Time.deltaTime; // Transforma la posicion hacia el frente 

        }

        theTransform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -camaraPantallaXYPlus.x + offsetX, camaraPantallaXYPlus.x - offsetX),
            Mathf.Clamp(transform.position.y, -camaraPantallaXYPlus.y + offsetY, camaraPantallaXYPlus.y - offsetY),
            transform.position.z
            );

    }
}