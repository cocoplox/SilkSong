using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT : MonoBehaviour
{
    public int danio = 1;
    public bool derecha = true; // Si es true, la bola se mover� hacia la derecha
    public bool izquierda = false; // Si es true, la bola se mover� hacia la izquierda
    public float velocidad = 5f; // Velocidad de la bola

    // Start is called before the first frame update
    void Start()
    {
        // Si tanto derecha como izquierda son true, desactivar izquierda
        if (derecha && izquierda)
        {
            izquierda = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mover la bola en la direcci�n establecida
        if (derecha)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
        else if (izquierda)
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        }
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {
       
        // Verificar si colision� con el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el script del jugador
            HeroKnight jugadorScript = other.GetComponent<HeroKnight>();

            // Verificar si el jugador tiene un script HeroKnight
            if (jugadorScript != null)
            {
                // Reducir la vida del jugador
                jugadorScript.RecibirDa�o(danio);
                // Destruir el ataque cuando golpea al jugador
                Destroy(gameObject);
            }
        }
    }
}
