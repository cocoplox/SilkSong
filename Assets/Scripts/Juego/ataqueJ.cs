using UnityEngine;

public class AtaqueJugador : MonoBehaviour
{
    private AudioSource audioSource; // Componente AudioSource
    public void Start()
    {

    }

    // Método que se llama cuando el collider del objeto entra en contacto con otro collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto con el que colisionamos es un enemigo
        if (other.CompareTag("Enemigo"))
        {
            // Obtener el componente de vida del enemigo
            VidaEnemigo vidaEnemigo = other.GetComponent<VidaEnemigo>();

            // Verificar si el enemigo tiene un componente de vida
            if (vidaEnemigo != null)
            {
                // Reducir la vida del enemigo
                vidaEnemigo.ReducirVida(Variables.currentDamage);

            }
        }
    }
}
