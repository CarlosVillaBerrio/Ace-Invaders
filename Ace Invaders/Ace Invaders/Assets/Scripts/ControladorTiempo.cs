using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorTiempo : MonoBehaviour
{
    float tiempoDeJuego = 300f;
    int timer;
    public Text taimu;

    void Start()
    {
        
    }

    void Update()
    {
        CuentaRegresiva();
    }

    void CuentaRegresiva()
    {
        tiempoDeJuego -= Time.deltaTime;
        timer = (int)tiempoDeJuego;
        taimu.GetComponent<Text>().text = "Tiempo: " + timer.ToString();
        if (tiempoDeJuego <= 0)
        {
            SceneManager.LoadScene("Moriste");
        }
    }
}
