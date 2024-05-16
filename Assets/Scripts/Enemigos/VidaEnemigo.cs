using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida m�xima del enemigo
    public float vidaActual; // Vida actual del enemigo

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
            Morir();
        }
    }

    // M�todo para que el enemigo muera
    void Morir()
    {

        Destroy(gameObject); // Destruir el objeto del enemigo
    }

    // M�todo para obtener la vida actual del enemigo
    public float GetVidaActual()
    {
        return vidaActual;
    }
}
