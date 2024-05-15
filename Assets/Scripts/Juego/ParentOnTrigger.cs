using UnityEngine;

public class ParentOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hacer que el jugador sea hijo de este objeto
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hacer que el jugador deje de ser hijo de este objeto
            other.transform.SetParent(null);
        }
    }
}
