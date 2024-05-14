using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Intaractable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Hay que checkear si está en ranfo
       //
       if(isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                //Si se cumplen todos los requisitos, ejecuta el metodo
                interactAction.Invoke();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("El Jugador SI que puede interactuar");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("El jugador NO puede interactuar");
        }
    }
}
