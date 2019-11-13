using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlInicio : MonoBehaviour
{
    void Start()
    {
        Invoke("Volveichon", 13.52f);
    }
    

    void Volveichon() // Volvemos a la seleccion de dificultad
    {
        SceneManager.LoadScene("SeleccionDificultad");
    }
}
