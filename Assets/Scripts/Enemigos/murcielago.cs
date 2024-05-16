using UnityEngine;

public class Murcielago : MonoBehaviour
{
    public Transform punto1; // Primer punto de destino
    public Transform punto2; // Segundo punto de destino
    public Transform punto3; // Tercer punto de destino
    public float velocidadMovimiento = 2.0f;
    public int daño = 1; // Cantidad de daño que inflige el murciélago
    public float cooldownTiempo = 3.0f; // Tiempo de cooldown para el cambio de punto aleatorio

    private bool enCooldown = false; // Indica si el murciélago está en cooldown
    private Vector3 puntoActual; // Punto de destino actual
    private SpriteRenderer spriteRenderer;
    private VidaEnemigo vidaEnemigo;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        puntoActual = ObtenerPuntoAleatorio();

        // Obtener el componente VidaEnemigo del murciélago
        vidaEnemigo = GetComponent<VidaEnemigo>();
    }

    void Update()
    {
        // Si no está en cooldown, mover hacia el punto actual
        if (!enCooldown)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoActual, velocidadMovimiento * Time.deltaTime);

            // Si llega al punto actual, seleccionar uno nuevo y activar el cooldown
            if (transform.position == puntoActual)
            {
                puntoActual = ObtenerPuntoAleatorio();
                enCooldown = true;
                Invoke("FinalizarCooldown", cooldownTiempo); // Programar el final del cooldown
            }
        }

        // Girar el sprite según la dirección de movimiento
        if (transform.position.x < puntoActual.x)
        {
            spriteRenderer.flipX = false; // Girar el sprite hacia la izquierda
        }
        else
        {
            spriteRenderer.flipX = true; // Girar el sprite hacia la derecha
        }
    }

    Vector3 ObtenerPuntoAleatorio()
    {
        int puntoAleatorio = Random.Range(1, 4);

        switch (puntoAleatorio)
        {
            case 1:
                return punto1.position;
            case 2:
                return punto2.position;
            case 3:
                return punto3.position;
            default:
                return punto1.position;
        }
    }

    void FinalizarCooldown()
    {
        enCooldown = false; // Finalizar el cooldown
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplicar daño al jugador
            other.GetComponent<HeroKnight>().RecibirDaño(daño);
        }
        
    }

    public void RecibirDaño(float cantidad)
    {
        // Reducir la vida del murciélago
        vidaEnemigo.ReducirVida(cantidad);
    }
}
