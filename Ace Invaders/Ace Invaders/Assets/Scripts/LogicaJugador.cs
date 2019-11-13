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

    [Header("Modo APK")]
    protected FixedJoystick joystick;
    LogicaBotones[] botones = new LogicaBotones[2];

    void Start()
    {
        audioUsar = GetComponent<AudioSource>(); // Obtiene el audiosource para manipular sonidos
        theTransform = GetComponent<Transform>(); // Obtiene el transform del objeto para aplicar la restriccion de camara
        camaraPantallaXYPlus = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)); // Adquiere el tamaño de la pantalla dependiendo de la resolucion
        proteccion.SetActive(false); // Desactiva el escudo al inicio
        joystick = FindObjectOfType<FixedJoystick>(); // Joystick para mover la nave
        botones = FindObjectsOfType<LogicaBotones>(); // Botones que permiten disparar los 2 tipos de balas
    }


    void Update()
    {
        MovimientoAvion();
        Disparar();
    }

    

    IEnumerator ReiniciarTiempoNoDisparo() // Permite ajustar la cadencia de disparo
    {
        yield return new WaitForSeconds(ritmoDeDisparo);
        tiempoNoDisparo = false;
    }
     
    void Disparar() // Funcion para disparar los proyectiles
    {
        if (tiempoNoDisparo) return; // Condicion para que se cumpla la cadencia


        if (Input.GetKey(KeyCode.F) || botones[0].pressed) // Botones que permiten el disparo
        {
            ritmoDeDisparo = cadenciaNormal;
            TiroNormal();
        }
        else if (Input.GetKeyDown(KeyCode.G) || botones[1].pressed) // Botones que permiten el disparo
        {
            ritmoDeDisparo = cadenciaEspecial;
            TiroEspecial();
        }

    }

    void efectoBalaSeDestruye() // Detalle de la bala especial cuando se destruye
    {
        if(balaInstanceS != null)
        {
            explosionBalaEspecial.transform.position = balaInstanceS.transform.position;
            audioUsar.PlayOneShot(explosionBalaEspecialSonido);
            explosionBalaEspecial.Stop();
            explosionBalaEspecial.Play();
        }
        
    }

    void EfectoDisparo() // Efecto de fogocidad del disparo
    {       
        fuegoDeArma.Stop();
        fuegoDeArma.Play();        
    }

    void EfectoDisparoEspecial() // Efecto de fogocidad del disparo
    {
        fuegoDeArmaEspecial.Stop();
        fuegoDeArmaEspecial.Play();
    }
    
    void TiroNormal() // Permite lanzar un proyectil y aplicar efectos audiovisuales
    {
        audioUsar.PlayOneShot(disparoNormal); // Inicia el sonido que representa el disparo
        tiempoNoDisparo = true;
        EfectoDisparo();
        GameObject balaInstance; // Crea la variable de la bala

        balaInstance = Instantiate(balaNormal, lanzador.position, Quaternion.identity); // Instancia la bala

        balaInstance.AddComponent<Rigidbody>().AddForce(lanzador.up * -100 * velocidadProyectilNormal); // añade rigidbody para aplicarle fuerza al objeto
        balaInstance.name = "BalaNormal"; // Le pone nombre al objeto para futuras busquedas
        balaInstance.AddComponent<LaBala>().dañoNormal = dañoNormal; // Le asigna el daño a la bala

        StartCoroutine(ReiniciarTiempoNoDisparo()); // Aplica la cadencia de tiro
        Destroy(balaInstance, 1f); // Destruye la bala
    }

    void TiroEspecial() // Lo mismo que el tiro normal, solo que esta vez aplicado a la bala especial
    {
        audioUsar.PlayOneShot(disparoEspecial);
        tiempoNoDisparo = true;
        EfectoDisparoEspecial();

        balaInstanceS = Instantiate(balaEspecial, lanzador.position, Quaternion.identity);

        balaInstanceS.AddComponent<Rigidbody>().AddForce(lanzador.up * -100 * velocidadProyectilEspecial);
        balaInstanceS.name = "BalaEspecial";
        balaInstanceS.AddComponent<LaBala>().dañoEspecial = dañoEspecial;

        StartCoroutine(ReiniciarTiempoNoDisparo());
        Invoke("efectoBalaSeDestruye", 2f); // Aplica efecto especial de destruccion
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

    void ActivacionProteccion() // Despliega el escudo cuando es llamada la funcion
    {
        if(!proteccion.activeSelf)
        {
            proteccion.SetActive(true);
        }

        Invoke("DesactivadorDeEscudo", tiempoInvulnerable); // Esta linea permite desactivar el escudo en unos segundos
    }

    void DesactivadorDeEscudo() // Funcion para desactivar el escudo
    {
        proteccion.SetActive(false);
    }

    void AnimacionMuerteAvion() // Animacion para la muerte del jugador
    {
        audioUsar.PlayOneShot(explosionJugador);
        explosionJugadorP.Stop();
        explosionJugadorP.Play();
    }

    void muerteAvion() // Funcion que nos lleva a una escena que describe que perdiste
    {
        if(vidas <= 0)
        {
            SceneManager.LoadScene("Moriste"); // comando para cargar una escena determinada
        }
    }

    void MovimientoAvion()
    {
        // transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(joystick.Horizontal * velocidadMovimiento,
            joystick.Vertical * velocidadMovimiento,
            rigidbody.velocity.z); 

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
        // theTransform aplica la restriccion que solo permite moverse donde ve la camara
        theTransform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -camaraPantallaXYPlus.x + offsetX, camaraPantallaXYPlus.x - offsetX),
            Mathf.Clamp(transform.position.y, -camaraPantallaXYPlus.y + offsetY, camaraPantallaXYPlus.y - offsetY),
            transform.position.z
            );

    }
}