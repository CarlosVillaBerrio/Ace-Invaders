using UnityEngine;
using UnityEngine.EventSystems;

public class LogicaBotones : MonoBehaviour, IPointerUpHandler, IPointerDownHandler // Herencias para interactuar con las imagenes como si fuesen botones
{
    public bool pressed;


    public void OnPointerDown(PointerEventData eventData) // Activa el bool al presionar el boton
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) // Desactiva el bool al soltar el boton
    {
        pressed = false;

    }
}
