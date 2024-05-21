using UnityEngine;
using UnityEngine.Events;

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del enemigo
    public float vidaActual; // Vida actual del enemigo

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
        Destroy(gameObject); // Destruir el objeto del enemigo
    }

    // Método para obtener la vida actual del enemigo
    public float GetVidaActual()
    {
        return vidaActual;
    }
}
