using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorTiempo : MonoBehaviour
{
    float tiempoDeJuego = 300f;
    int timer; // Variable auxiliar para mostrar el tiempo en numeros enteros
    public Text taimu; // Ui del canvas que muestra el tiempo
    

    void Update()
    {
        CuentaRegresiva(); // Llama a la funcion para actualizar las condiciones de juego
    }

    void CuentaRegresiva() // Funcion que controla la duracion de la partida
    {
        tiempoDeJuego -= Time.deltaTime;
        timer = (int)tiempoDeJuego;
        taimu.GetComponent<Text>().text = "Tiempo: " + timer.ToString();
        if (tiempoDeJuego <= 0) // Si se agota el tiempo, perdiste.
        {
            SceneManager.LoadScene("Moriste");
        }
    }
}
