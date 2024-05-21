using UnityEngine;

public class Angel : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida m�xima del �ngel
    private float vidaActual; // Vida actual del �ngel

    public float alturaMax;
    public float alturaMin;
    public float velocidad;
    public int damage = 1;

    private bool subiendo = true;

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor m�ximo
    }

    private void Update()
    {
        if (subiendo && transform.position.y < alturaMax)
        {
            transform.Translate(Vector3.up * velocidad * Time.deltaTime);
        }
        else if (!subiendo && transform.position.y > alturaMin)
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
        }
        else
        {
            subiendo = !subiendo;
        }
    }

    // M�todo para recibir da�o del jugador
    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de da�o recibida

        // Verificar si la vida del �ngel es menor o igual a 0
        if (vidaActual <= 0)
        {
            // Llamar a un m�todo de muerte del �ngel
            Morir();
        }
    }

    // M�todo para que el �ngel muera
    void Morir()
    {
        // Aqu� puedes agregar cualquier l�gica que desees cuando el �ngel muere
        // Por ejemplo, reproducir una animaci�n de muerte, dar puntos al jugador, etc.

        Destroy(gameObject); // Destruir el objeto del �ngel
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            other.GetComponent<HeroKnight>().RecibirDa�o(damage);
        }
    }
}
