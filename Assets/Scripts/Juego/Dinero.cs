using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Dinero : MonoBehaviour
{
    //public float cantidadPuntos;

    //public Score Score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Score.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
    }
}
