using UnityEngine;

public class Mbola : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aplicar da�o al jugador
            other.GetComponent<HeroKnight>().RecibirDa�o(1);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            // Si el objeto entra en contacto con la capa "Suelo", destruirlo
            Destroy(gameObject);
        }
    }
}
