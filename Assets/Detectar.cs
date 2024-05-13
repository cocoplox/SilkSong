using UnityEngine;

public class Detectar : MonoBehaviour
{
    private Mago mago;

    void Start()
    {
        mago = GetComponentInParent<Mago>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mago.StartAtacar();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mago.StopAtacar();
        }
    }
}
