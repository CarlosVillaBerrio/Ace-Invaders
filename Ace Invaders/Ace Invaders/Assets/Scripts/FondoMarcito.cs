using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMarcito : MonoBehaviour
{
    Renderer rend;
    float levelOffSetY;
    public float velocidadAnimacion;

    void Start()
    {
        rend = GetComponent<Renderer>();
        velocidadAnimacion = 0.03f;
    }

    // Update is called once per frame
    void Update()
    {
        MoverOffSetY();
    }

    void MoverOffSetY()
    {
        levelOffSetY = Time.time * velocidadAnimacion;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, levelOffSetY));
    }
}
