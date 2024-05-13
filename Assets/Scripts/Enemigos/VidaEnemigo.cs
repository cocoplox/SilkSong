using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del enemigo
    private float vidaActual; // Vida actual del enemigo

    // Se llama cuando se inicializa el objeto
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
            // Llamar a un método de muerte del enemigo
            Morir();
        }
    }

    // Método para que el enemigo muera
    void Morir()
    {
        // Aquí puedes agregar cualquier lógica que desees cuando el enemigo muere
        // Por ejemplo, reproducir una animación de muerte, dar puntos al jugador, etc.

        Destroy(gameObject); // Destruir el objeto del enemigo
    }

    // Método para obtener la vida actual del enemigo
    public float GetVidaActual()
    {
        return vidaActual;
    }
}
