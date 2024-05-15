using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuntoNoRetorno : MonoBehaviour
{
    public GameObject objetoOculto; // Variable para almacenar el objeto oculto que se activará al colisionar con el jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activa el objeto oculto asignado
            objetoOculto.SetActive(true);
        }
    }
}
