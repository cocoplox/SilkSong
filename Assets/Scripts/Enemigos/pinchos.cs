using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchos : MonoBehaviour
{
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Reducir la vida del jugador
                other.GetComponent<HeroKnight>().RecibirDaño(damage);
            }
        }
    
}
