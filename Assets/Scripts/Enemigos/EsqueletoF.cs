using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public Transform punto1; // Primer punto
    public Transform punto2; // Segundo punto
    public float velocidad = 2f; // Velocidad de movimiento
    public int dañoAlJugador = 1; // Cantidad de daño que causa al jugador
    public float vidaMaxima = 10f; // Vida máxima del enemigo

    private bool moviendoseHaciaPunto1 = false; // Variable para controlar la dirección de movimiento
    private float vidaActual; // Vida actual del enemigo
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
    }

    // Método para reducir la vida del enemigo
    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de daño recibida
        if (vidaActual <= 0)
        {
            Morir(); // Llamar al método de muerte si la vida llega a 0 o menos
        }
    }

    // Método para que el enemigo muera
    void Morir()
    {
        Destroy(gameObject); // Destruir el objeto del enemigo
    }

    // Método para detectar colisiones con el jugador
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verificar si la colisión es con el jugador
        {
            // Obtener el componente del jugador
            HeroKnight jugador = collision.gameObject.GetComponent<HeroKnight>();
            if (jugador != null)
            {
                // Reducir la vida del jugador
                jugador.RecibirDaño(dañoAlJugador);
            }
        }
    }

    void Update()
    {
        // Mover al enemigo entre los dos puntos
        if (moviendoseHaciaPunto1)
        {
            transform.position = Vector3.MoveTowards(transform.position, punto1.position, velocidad * Time.deltaTime);
            // Si el enemigo llega al punto 1, cambiar la dirección y la orientación del sprite
            if (transform.position == punto1.position)
            {
                moviendoseHaciaPunto1 = false;
                spriteRenderer.flipX = true; // Sprite mira hacia la derecha
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, punto2.position, velocidad * Time.deltaTime);
            // Si el enemigo llega al punto 2, cambiar la dirección y la orientación del sprite
            if (transform.position == punto2.position)
            {
                moviendoseHaciaPunto1 = true;
                spriteRenderer.flipX = false; // Sprite mira hacia la izquierda
            }
        }
    }
}
