using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    private void OnMouseDown()
    {
        FuncionSalir();
    }

    public void FuncionSalir()
    {
        Application.Quit();
    }

}
