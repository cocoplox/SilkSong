using UnityEngine;

public class Bdash : MonoBehaviour
{
    public Transform punto1; // Primer punto
    public Transform punto2; // Segundo punto
    public float velocidad = 2f; // Velocidad de movimiento normal
    public float velocidadAumentada = 5f; // Velocidad de movimiento aumentada
    public float tiempoAumentoVelocidad = 5f; // Tiempo que la velocidad estará aumentada
    public float tiempoQuieto = 2f; // Tiempo que el enemigo estará quieto después de aumentar la velocidad
    public float tiempoEntreAumentos = 10f; // Tiempo entre aumentos de velocidad
    public int repeticionesAumento = 3; // Número de veces que se aumentará la velocidad antes de quedarse quieto
    public float tiempoQuietoFinal = 3f; // Tiempo que el enemigo estará quieto al final
    public int dañoAlJugador = 1; // Cantidad de daño que causa al jugador
    public float vidaMaxima = 10f; // Vida máxima del enemigo

    private bool moviendoseHaciaPunto1 = false; // Variable para controlar la dirección de movimiento
    private float vidaActual; // Vida actual del enemigo
    private SpriteRenderer spriteRenderer; // Referencia al componente SpriteRenderer
    private float tiempoAumentoActual; // Tiempo actual que lleva la velocidad aumentada
    private int repeticionesAumentoActual; // Número de repeticiones de velocidad aumentada realizadas
    private bool quieto = false; // Indica si el enemigo está quieto

    private Animator m_animator;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        vidaActual = vidaMaxima; // Inicializar la vida del enemigo
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtener el componente SpriteRenderer
    }

    void Update()
    {
        // Verificar si el enemigo está quieto
        if (quieto)
        {
            // Contar el tiempo que el enemigo está quieto
            tiempoQuietoFinal -= Time.deltaTime;
            if (tiempoQuietoFinal <= 0f)
            {
                // Reiniciar variables para volver a moverse
                tiempoQuietoFinal = 3f;
                quieto = false;
                repeticionesAumentoActual = 0;
                tiempoAumentoActual = 0f;
            }
            return; // Salir del método Update si el enemigo está quieto
        }

        

        // Contar el tiempo de aumento de velocidad
        if (tiempoAumentoActual > 0f)
        {
            tiempoAumentoActual -= Time.deltaTime;
            if (tiempoAumentoActual <= 0f)
            {
                // Restaurar la velocidad normal después del tiempo de aumento
                velocidad = 2f;
            }
        }

        // Contar el tiempo entre aumentos de velocidad
        if (repeticionesAumentoActual < repeticionesAumento && tiempoAumentoActual <= 0f)
        {
            tiempoEntreAumentos -= Time.deltaTime;
            if (tiempoEntreAumentos <= 0f)
            {
                // Aumentar la velocidad y reiniciar el temporizador
                velocidad = velocidadAumentada;
                tiempoAumentoActual = tiempoAumentoVelocidad;
                tiempoEntreAumentos = 10f;
                repeticionesAumentoActual++;
            }
        }

        // Mover al enemigo entre los dos puntos
        if (moviendoseHaciaPunto1)
        {
            m_animator.SetInteger("AnimState", 2);
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

    // Método para detectar colisiones con el jugador
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            other.GetComponent<HeroKnight>().RecibirDaño(dañoAlJugador);
        }
    }
}
