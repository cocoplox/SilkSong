using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Aseg�rate de que tienes esta l�nea para trabajar con UI

public class TiendaPaso : MonoBehaviour
{
    public string message = "Pulsa 'F' para entrar";
    public GameObject messageUI; // Asignar un GameObject que contenga el UI Text en el editor
    private bool isPlayerInRange = false;

    private void Start()
    {
        if (messageUI != null)
        {
            messageUI.SetActive(false); // Aseg�rate de que el mensaje est� oculto al principio
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            if (messageUI != null)
            {
                messageUI.SetActive(true);
                messageUI.GetComponent<Text>().text = message;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            if (messageUI != null)
            {
                messageUI.SetActive(false);
            }
        }
    }
}
