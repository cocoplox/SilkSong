using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida m�xima del enemigo
    private float vidaActual; // Vida actual del enemigo

    // Se llama cuando se inicializa el objeto
    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor m�ximo
    }

    // M�todo para reducir la vida del enemigo
    public void ReducirVida(float cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de da�o recibida
        Debug.Log(vidaActual);
        // Verificar si la vida del enemigo es menor o igual a 0
        if (vidaActual <= 0)
        {
            // Llamar a un m�todo de muerte del enemigo
            Morir();
        }
    }

    // M�todo para que el enemigo muera
    void Morir()
    {
        // Aqu� puedes agregar cualquier l�gica que desees cuando el enemigo muere
        // Por ejemplo, reproducir una animaci�n de muerte, dar puntos al jugador, etc.

        Destroy(gameObject); // Destruir el objeto del enemigo
    }

    // M�todo para obtener la vida actual del enemigo
    public float GetVidaActual()
    {
        return vidaActual;
    }
}
