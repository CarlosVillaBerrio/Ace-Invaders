using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMarcito : MonoBehaviour
{
    Renderer rend; // Variable para acceder al renderer de la imagen
    float levelOffSetY; // Variable que cambia en el tiempo
    public float velocidadAnimacion; // Velocidad con la que se mueve el offset

    void Start()
    {
        rend = GetComponent<Renderer>(); // Obtenemos el offset del fondo del juego
        velocidadAnimacion = 0.03f; // Establecemos una velocidad entre 0 y 1 que permita visualizar bien el movimiento de la imagen
    }

    // Update is called once per frame
    void Update()
    {
        MoverOffSetY(); // Funcion para mover la imagen
    }

    void MoverOffSetY() // Funcion de animacion del fondo
    {
        levelOffSetY = Time.time * velocidadAnimacion; // Cambio temporal del offset
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, levelOffSetY)); // comando que nos permite acceder al offset en Y de la textura principal
    }
}
