using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControladorEnemigos : MonoBehaviour
{
    int[] hordas = new int[4]; // establecemos el numero de hordas

    int contadorOleadas = 0;

    float contadorTiempo = 0f;

    public GameObject[] grupos;

    public Text nEnemigos;
    int cantidadEnemigos = 0;

    void Start()
    {
        var horda1 = FindObjectsOfType<LogicaJet>(); // Realizamos la busqueda de los grupos que componen las hordas
        var horda2 = FindObjectsOfType<LogicaAvioneta>();
        var horda3 = FindObjectsOfType<LogicaBombardero>();
        var horda4 = FindObjectsOfType<LogicaJefeFinal>();
        
        hordas[0] = horda1.Length;
        hordas[1] = horda2.Length;
        hordas[2] = horda3.Length;
        hordas[3] = horda4.Length;  
        
        grupos[0].SetActive(false);
        grupos[1].SetActive(false);
        grupos[2].SetActive(false);
        grupos[3].SetActive(false);

        LanzarOleada(grupos[contadorOleadas]); // Esto Inicia el juego
    }

    void Update()
    {
        contadorTiempo += Time.deltaTime; 
    }

    public void ComprobarOla() // Funcion que comprueba la ola actual y lanza otra si se cumple la condicion
    {
        contadorDeEnemis();
        if ((cantidadEnemigos <= 0 && contadorOleadas < 4) || contadorTiempo > 70f)
        {
            LanzarOleada(grupos[contadorOleadas]); // funcion que tira otra horda
            contadorTiempo = 0f;
        }
    }

    public void LanzarOleada(GameObject oleadaActual) // Activa las hordas en orden numerico
    {
        oleadaActual.SetActive(true);
        cantidadEnemigos += hordas[contadorOleadas];
        nEnemigos.GetComponent<Text>().text = "Enemigos: " + cantidadEnemigos.ToString();
        contadorOleadas++;
    }

    public void contadorDeEnemis() // Permite que el Ui muestre el numero de enemigos actuales
    {
        cantidadEnemigos--;
        nEnemigos.GetComponent<Text>().text = "Enemigos: " + cantidadEnemigos.ToString();
    }
}
