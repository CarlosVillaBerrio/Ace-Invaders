using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    private void OnMouseDown()
    {
        FuncionSalir(); // Funcion que permite salir de la app mediante un boton
    }

    public void FuncionSalir()
    {
        Application.Quit(); // Comando de unity para salir de aplicaciones
    }

}
