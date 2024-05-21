using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    public string message = "W A S D Para moverte. Espacio para saltar CLICK IZQUIERDO del raton para atacarr";
    public GameObject messageUI; // Asignar un GameObject que contenga el UI Text en el editor

    private void Start()
    {
        if (messageUI != null)
        {
            messageUI.SetActive(false); // Asegúrate de que el mensaje esté oculto al principio
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (messageUI != null)
            {
                messageUI.SetActive(true);
                messageUI.GetComponent<Text>().text = message;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (messageUI != null)
            {
                messageUI.SetActive(false);
            }
        }
    }
}