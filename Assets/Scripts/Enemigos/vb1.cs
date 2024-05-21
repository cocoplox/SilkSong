using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class vb1 : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del enemigo
    public float vidaActual; // Vida actual del enemigo
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public Color damagedColor = new Color(1f, 0.231f, 0.231f, 1f); // Color cuando está dañado (rojo)
    private Color originalColor; // Color original del sprite
    public float tiempoColorOriginal = 1.0f; // Tiempo en segundos para volver al color original
    public UnityEvent OnVidaReducida; // Evento que se activa cuando la vida se reduce

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor máximo
        originalColor = spriteRenderer.color;

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

    IEnumerator ChangeColorRoutine()
    {
        spriteRenderer.color = damagedColor; // Cambiar el color del sprite al color dañado

        yield return new WaitForSeconds(tiempoColorOriginal); // Esperar el tiempo especificado

        spriteRenderer.color = originalColor; // Volver al color original
    }
}
