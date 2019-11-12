using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarReto : MonoBehaviour
{
    public void CargarNivel(string escenaNivel) // FUNCION PARA SELECCIONAR DIFICULTAD
    { 
        SceneManager.LoadScene(escenaNivel); // SE USA EN LA UI DE UN BOTON DEL CANVAS Y SE LLAMA EN EL ON CLICK DEL MISMO
    }
}
