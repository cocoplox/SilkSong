using UnityEngine;
using UnityEngine.Events;

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del enemigo
    public float vidaActual; // Vida actual del enemigo
    public bool J1;
    public bool J2;
    public bool J3;
    public bool J4;
    [SerializeField] AudioClip daño;
    public UnityEvent OnVidaReducida; // Evento que se activa cuando la vida se reduce

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor máximo
    }

    // Método para reducir la vida del enemigo
    public void ReducirVida(float cantidad)
    {

        vidaActual -= cantidad; // Restar la cantidad de daño recibida
        Debug.Log(vidaActual);

        // Verificar si la vida del enemigo es menor o igual a 0
        if (vidaActual <= 0)
        {
            Morir();
        }
        else
        {
            // Disparar el evento de vida reducida
            OnVidaReducida.Invoke();
        }
    }

    // Método para que el enemigo muera
    void Morir()
    {
        if (J1 == true)
        {
            Variables.isJefe1Alive = false;
            Variables.currency += 100;
            Destroy(gameObject); // Destruir el objeto del enemigo
        }
        if (J2 == true)
        {
            Variables.isJefe2Alive = false;
            Variables.currency += 100;
            Destroy(gameObject); // Destruir el objeto del enemigo
        }
        if (J3 == true)
        {
            Variables.isJefe3Alive = false;
            Variables.currency += 100;
            Destroy(gameObject); // Destruir el objeto del enemigo
        }
        if (J4 == true)
        {
            Variables.isJefe4Alive = false;
            Variables.currency += 100;
            Destroy(gameObject); // Destruir el objeto del enemigo
        }
        else
        {
            Variables.currency += 15;
            Destroy(gameObject);
        }

    }

    // Método para obtener la vida actual del enemigo
    public float GetVidaActual()
    {
        return vidaActual;
    }
}
