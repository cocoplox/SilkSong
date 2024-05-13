using UnityEngine;

public class Angel : MonoBehaviour
{
    public float vidaMaxima = 100.0f; // Vida máxima del ángel
    private float vidaActual; // Vida actual del ángel

    public float alturaMax;
    public float alturaMin;
    public float velocidad;

    private bool subiendo = true;

    void Start()
    {
        vidaActual = vidaMaxima; // Inicializar la vida actual con el valor máximo
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

    // Método para recibir daño del jugador
    public void RecibirDanio(float cantidad)
    {
        vidaActual -= cantidad; // Restar la cantidad de daño recibida

        // Verificar si la vida del ángel es menor o igual a 0
        if (vidaActual <= 0)
        {
            // Llamar a un método de muerte del ángel
            Morir();
        }
    }

    // Método para que el ángel muera
    void Morir()
    {
        // Aquí puedes agregar cualquier lógica que desees cuando el ángel muere
        // Por ejemplo, reproducir una animación de muerte, dar puntos al jugador, etc.

        Destroy(gameObject); // Destruir el objeto del ángel
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            VidaJugador vidaJugador = collision.gameObject.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDanio(10); // Dañar al jugador
            }
            Debug.Log("Enemigo"); // Esto es solo para verificar si la colisión funciona
        }
    }
}
