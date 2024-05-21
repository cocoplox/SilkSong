using UnityEngine;

public class AtaqueF : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f; // Velocidad de movimiento del ataque
    public float tiempoVida = 3.0f; // Tiempo de vida del ataque en segundos
    public int danio = 10; // Cantidad de daño que inflige el ataque
    private Transform jugador; // Referencia al jugador
    Vector3 direccionJugador;
    Vector3 direccionMovimiento;

    void Start()
    {
        // Destruir el ataque después de un tiempo
        Destroy(gameObject, tiempoVida);

        // Obtener la posición actual del jugador
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        // Obtener la dirección hacia el jugador
        direccionJugador = (jugador.position - transform.position).normalized;

        // Calcular la dirección de movimiento del ataque
        direccionMovimiento = new Vector3(direccionJugador.x, direccionJugador.y + 0.07f, 0);
    }

    private void Update()
    {
        // Mover el ataque en la dirección calculada
        transform.Translate(direccionMovimiento * velocidadMovimiento * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si colisionó con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el script del jugador
            HeroKnight jugadorScript = other.GetComponent<HeroKnight>();

            // Verificar si el jugador tiene un script HeroKnight
            if (jugadorScript != null)
            {
                // Reducir la vida del jugador
                jugadorScript.RecibirDaño(danio);
                // Destruir el ataque cuando golpea al jugador
                Destroy(gameObject);
            }
        }
    }
}
