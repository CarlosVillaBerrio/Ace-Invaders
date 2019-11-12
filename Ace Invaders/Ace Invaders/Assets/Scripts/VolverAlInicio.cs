using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlInicio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Volveichon", 13.52f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Volveichon()
    {
        SceneManager.LoadScene("SeleccionDificultad");
    }
}
