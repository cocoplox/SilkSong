using System.Collections;
using UnityEngine;

public class rataboss : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public Color damagedColor = new Color(1f, 0.231f, 0.231f, 1f); // Color cuando está dañado (rojo)
    private Color originalColor; // Color original del sprite
    public float tiempoColorOriginal = 1.0f; // Tiempo en segundos para volver al color original
    public int damage;

    private VidaEnemigo vidaEnemigo; // Referencia al script VidaEnemigo

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = GetComponent<VidaEnemigo>(); // Asegúrate de que haya un componente VidaEnemigo en el mismo GameObject

        if (vidaEnemigo == null)
        {
            Debug.LogError("VidaEnemigo no se encontró en el GameObject");
        }

        // Guardar el color original del sprite
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaEnemigo != null && vidaEnemigo.GetVidaActual() < vidaEnemigo.vidaMaxima)
        {
            StartCoroutine(ChangeColorRoutine());
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            other.GetComponent<HeroKnight>().RecibirDaño(damage);
        }
    }
    IEnumerator ChangeColorRoutine()
    {
        spriteRenderer.color = damagedColor; // Cambiar el color del sprite al color dañado

        yield return new WaitForSeconds(tiempoColorOriginal); // Esperar el tiempo especificado

        spriteRenderer.color = originalColor; // Volver al color original
    }
}
