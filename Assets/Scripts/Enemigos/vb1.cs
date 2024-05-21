using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class vb1 : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida m�xima del enemigo
    public float vidaActual; // Vida actual del enemigo
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public Color damagedColor = new Color(1f, 0.231f, 0.231f, 1f); // Color cuando est� da�ado (rojo)
    private Color originalColor; // Color original del sprite
    public float tiempoColorOriginal = 1.0f; // Tiempo en segundos para volver al color original
    public UnityEvent OnVidaReducida; // Evento que se activa cuando la vida se reduce

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor m�ximo
        originalColor = spriteRenderer.color;

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
        else
        {
            // Disparar el evento de vida reducida
            OnVidaReducida.Invoke();
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

    IEnumerator ChangeColorRoutine()
    {
        spriteRenderer.color = damagedColor; // Cambiar el color del sprite al color da�ado

        yield return new WaitForSeconds(tiempoColorOriginal); // Esperar el tiempo especificado

        spriteRenderer.color = originalColor; // Volver al color original
    }
}
