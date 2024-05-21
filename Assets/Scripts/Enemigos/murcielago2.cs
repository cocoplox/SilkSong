using UnityEngine;
using System.Collections;

public class MurcielagoLoopRuta : MonoBehaviour
{
    public Transform punto1; // Primer punto de destino
    public Transform punto2; // Segundo punto de destino
    public GameObject objetoPrefab; // Prefab del objeto a spawnear
    public float velocidadMovimiento = 2.0f;
    public float tiempoEntreSpawns = 5.0f; // Tiempo entre cada spawn de objeto

    private bool haciaPunto2 = true; // Indica si el murciélago se mueve hacia el punto 2
    private Vector3 direccion; // Dirección del movimiento
    private SpriteRenderer spriteRenderer;
    private VidaEnemigo vidaEnemigo;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        CalcularDireccion();
        StartCoroutine(SpawnearObjeto());

        // Obtener el componente VidaEnemigo del murciélago
        vidaEnemigo = GetComponent<VidaEnemigo>();
    }

    void Update()
    {
        // Mover el murciélago hacia el punto correspondiente
        Vector3 movimiento = direccion * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movimiento);

        // Girar el sprite según la dirección de movimiento
        if (direccion.x < 0)
        {
            spriteRenderer.flipX = false; // Girar el sprite hacia la izquierda
        }
        else
        {
            spriteRenderer.flipX = true; // Girar el sprite hacia la derecha
        }

        // Si el murciélago llega al punto 2, cambiar la dirección
        if (haciaPunto2 && Vector3.Distance(transform.position, punto2.position) < 0.1f)
        {
            haciaPunto2 = false;
            CalcularDireccion();
        }
        // Si el murciélago llega al punto 1, cambiar la dirección
        else if (!haciaPunto2 && Vector3.Distance(transform.position, punto1.position) < 0.1f)
        {
            haciaPunto2 = true;
            CalcularDireccion();
        }
    }

    IEnumerator SpawnearObjeto()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);
            Instantiate(objetoPrefab, transform.position, Quaternion.identity);
        }
    }

    void CalcularDireccion()
    {
        if (haciaPunto2)
        {
            direccion = (punto2.position - transform.position).normalized;
        }
        else
        {
            direccion = (punto1.position - transform.position).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplicar daño al jugador
            other.GetComponent<HeroKnight>().RecibirDaño(1);
        }
    }

    // Método para recibir daño
    public void RecibirDaño(float cantidad)
    {
        // Reducir la vida del murciélago
        vidaEnemigo.ReducirVida(cantidad);
    }
}
