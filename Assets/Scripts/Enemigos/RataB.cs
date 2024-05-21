using UnityEngine;

public class RataB : MonoBehaviour
{
    public Transform punto1; // Primer punto de destino
    public Transform punto2; // Segundo punto de destino
    public float velocidadMovimiento = 2.0f;
    public float fuerzaSalto = 5.0f; // Fuerza de salto de la rata
    public float alturaSalto = 1.0f; // Altura máxima del salto
    public float tiempoSalto = 1.0f; // Tiempo de salto antes de descender
    public float distanciaUmbral = 0.1f; // Distancia umbral para cambiar de objetivo
    public int damage = 10; // Daño infligido al jugador

    private Rigidbody2D rigidbody2;
    private VidaEnemigo vidaEnemigo;

    private Vector3 objetivo; // Objetivo actual de movimiento
    private Vector3 posicionInicial; // Posición inicial antes de saltar
    private bool haciaPunto2 = true; // Indica si la rata se mueve hacia el punto 2
    private float tiempoEnSalto; // Tiempo transcurrido en el salto

    void Start()
    {
        vidaEnemigo = GetComponent<VidaEnemigo>(); // Obtener componente de vida enemiga
        rigidbody2 = GetComponent<Rigidbody2D>(); // Obtener componente Rigidbody2D

        // Establecer objetivo inicial
        objetivo = punto2.position;
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Verificar si la rata ha alcanzado el objetivo y cambiar de objetivo
        if (Vector3.Distance(transform.position, objetivo) < distanciaUmbral)
        {
            CambiarObjetivo();
        }

        // Mover la rata hacia el objetivo
        Vector3 movimiento = Vector3.Lerp(transform.position, objetivo, velocidadMovimiento * Time.deltaTime);
        transform.position = movimiento;

        // Girar el sprite según la dirección de movimiento
        if (transform.position.x < objetivo.x)
        {
            transform.localScale = new Vector3(1, 1, 1); // Girar hacia la derecha
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1); // Girar hacia la izquierda
        }

        // Salto automático
        Saltar();
    }

    void CambiarObjetivo()
    {
        // Cambiar el objetivo hacia el punto 1 o punto 2
        objetivo = haciaPunto2 ? punto1.position : punto2.position;
        haciaPunto2 = !haciaPunto2;
    }

    void Saltar()
    {
        // Aplicar una fuerza vertical solo si la rata está en la posición inicial
        if (transform.position.y <= posicionInicial.y)
        {
            // Incrementar el tiempo de salto
            tiempoEnSalto += Time.deltaTime;

            // Aplicar el salto solo si el tiempo de salto es menor que el tiempo de salto definido
            if (tiempoEnSalto < tiempoSalto)
            {
                rigidbody2.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); // Aplicar fuerza vertical hacia arriba
            }
            else
            {
                rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0f); // Detener la velocidad vertical
                transform.position = new Vector3(transform.position.x, posicionInicial.y, transform.position.z); // Ajustar posición en Y
            }
        }
        else
        {
            tiempoEnSalto = 0.0f; // Reiniciar el tiempo de salto
        }
    }

    // Detectar colisiones con el jugador
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            other.GetComponent<HeroKnight>().RecibirDaño(damage);
        }
    }

    public void RecibirDaño(float cantidad)
    {
        // Reducir la vida de la rata
        vidaEnemigo.ReducirVida(cantidad);
    }
}
