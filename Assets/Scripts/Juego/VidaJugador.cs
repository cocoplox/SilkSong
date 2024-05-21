using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del jugador
    private float vidaActual; // Vida actual del jugador

    private Animator animator; // Referencia al componente Animator del jugador

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor máximo
        animator = GetComponent<Animator>(); // Obtener el componente Animator del jugador
    }

    // Método para recibir daño
    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de daño recibida

        // Verificar si la vida del jugador es menor o igual a 0
        if (vidaActual <= 0)
        {
            // Llamar a un método de muerte del jugador
            Morir();
        }
        else
        {
            // Si el jugador aún tiene vida, activar la animación de daño
            animator.SetTrigger("Hurt");
        }
    }

    // Método para que el jugador muera
    void Morir()
    {
        // Aquí puedes agregar cualquier lógica que desees cuando el jugador muere
        // Por ejemplo, reproducir una animación de muerte, reiniciar el nivel, etc.
        animator.SetBool("IsDead", true); // Activar la animación de muerte

        // Desactivar otros componentes o scripts del jugador si es necesario
        // Por ejemplo, desactivar el script de movimiento, controlar la entrada del jugador, etc.

        // Desactivar el collider del jugador para evitar colisiones adicionales
        //GetComponent<Collider2D>().enabled = false;

        // Finalmente, puedes hacer que el juego se detenga o realizar cualquier otra acción necesaria
        // Por ejemplo, mostrar un mensaje de "Game Over" y permitir al jugador reiniciar el nivel
        Debug.Log("¡El jugador ha muerto!");
    }
}
