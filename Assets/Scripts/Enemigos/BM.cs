using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BM : MonoBehaviour
{
    public Transform punto1; // Primer punto de spawn
    public Transform punto2; // Segundo punto de spawn
    public BMS bms; // Referencia al script del objeto hijo
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Iniciar el spawn de las bolas de fuego
        if (bms != null)
        {
            bms.IniciarSpawnBolasDeFuego(punto1, punto2);
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

    // Update is called once per frame
    void Update()
    {

    }
}
