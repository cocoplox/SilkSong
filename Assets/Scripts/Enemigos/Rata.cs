using UnityEditor;
using UnityEngine;

public class Rata : MonoBehaviour
{
    public Transform punto1; // Primer punto de destino
    public Transform punto2; // Segundo punto de destino
    public float velocidadMovimiento = 2.0f;
    public float fuerzaSalto = 5.0f; // Fuerza de salto de la rata
    public float alturaSalto = 1.0f; // Altura máxima del salto
    public float tiempoSalto = 1.0f; // Tiempo de salto antes de descender
    public float distanciaUmbral = 0.1f; // Distancia umbral para cambiar de objetivo
    public int damage = 10; // Daño infligido al jugador

    Rigidbody2D rigidbody2;

    VidaEnemigo vidaEnemigo;

    private Vector3 objetivo; // Objetivo actual de movimiento
    private Vector3 posicionInicial; // Posición inicial antes de saltar
    private bool haciaPunto2 = true; // Indica si la rata se mueve hacia el punto 2
    private float tiempoEnSalto; // Tiempo transcurrido en el salto

    void Start()
    {
        objetivo = punto2.position;
        posicionInicial = transform.position;
        rigidbody2 = GetComponent<Rigidbody2D>();

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
            // Girar el sprite hacia la izquierda
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // Girar el sprite hacia la derecha
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Salto automático
        Saltar();
    }

    void CambiarObjetivo()
    {
        if (haciaPunto2)
        {
            objetivo = punto1.position;
        }
        else
        {
            objetivo = punto2.position;
        }
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
                // Aplicar fuerza vertical hacia arriba al Rigidbody2D
                rigidbody2.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            }
            else
            {
                // Si ya ha alcanzado la altura máxima del salto y ha pasado el tiempo de salto, detener la velocidad vertical
                rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0f);
                // Ajustar la posición en Y para asegurarse de que esté en la altura correcta
                transform.position = new Vector3(transform.position.x, posicionInicial.y, transform.position.z);
            }
        }
        else
        {
            // Reiniciar el tiempo de salto
            tiempoEnSalto = 0.0f;
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
