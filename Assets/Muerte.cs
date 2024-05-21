using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    public int damage = 1000000;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reducir la vida del jugador
            other.GetComponent<HeroKnight>().RecibirDaño(damage);
        }
    }
}
