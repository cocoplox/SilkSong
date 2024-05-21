using System.Collections;
using UnityEngine;

public class BMS : MonoBehaviour
{
    public GameObject bolaDeFuegoPrefab; // Prefab de la bola de fuego
    public float tiempoSpawnNormal = 1.0f; // Tiempo entre spawns en el ataque normal
    public float tiempoSpawnRapido = 0.5f; // Tiempo entre spawns en el ataque rápido
    public float tiempoAtaque = 5.0f; // Duración de cada tipo de ataque

    private Transform punto1; // Primer punto de spawn
    private Transform punto2; // Segundo punto de spawn

    // Tipos de ataque
    private enum TipoAtaque { Normal, Rapido, Multiple }
    private TipoAtaque ataqueActual;
    private Coroutine ataqueCoroutine;

    // Método para iniciar el spawn de las bolas de fuego
    public void IniciarSpawnBolasDeFuego(Transform p1, Transform p2)
    {
        punto1 = p1;
        punto2 = p2;
        StartCoroutine(CambiarAtaque());
    }

    // Coroutine para cambiar el tipo de ataque aleatoriamente
    IEnumerator CambiarAtaque()
    {
        while (true)
        {
            ataqueActual = (TipoAtaque)Random.Range(0, 3);
            if (ataqueCoroutine != null)
            {
                StopCoroutine(ataqueCoroutine);
            }

            switch (ataqueActual)
            {
                case TipoAtaque.Normal:
                    ataqueCoroutine = StartCoroutine(SpawnearBolasDeFuegoNormal());
                    break;
                case TipoAtaque.Rapido:
                    ataqueCoroutine = StartCoroutine(SpawnearBolasDeFuegoRapido());
                    break;
                case TipoAtaque.Multiple:
                    ataqueCoroutine = StartCoroutine(SpawnearBolasDeFuegoMultiple());
                    break;
            }

            yield return new WaitForSeconds(tiempoAtaque);
        }
    }

    // Coroutine para el spawn de las bolas de fuego normal
    IEnumerator SpawnearBolasDeFuegoNormal()
    {
        while (ataqueActual == TipoAtaque.Normal)
        {
            Vector3 posicionSpawn = new Vector3(
                Random.Range(punto1.position.x, punto2.position.x),
                Random.Range(punto1.position.y, punto2.position.y),
                punto1.position.z
            );

            Instantiate(bolaDeFuegoPrefab, posicionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(tiempoSpawnNormal);
        }
    }

    // Coroutine para el spawn de las bolas de fuego rápido
    IEnumerator SpawnearBolasDeFuegoRapido()
    {
        while (ataqueActual == TipoAtaque.Rapido)
        {
            Vector3 posicionSpawn = new Vector3(
                Random.Range(punto1.position.x, punto2.position.x),
                Random.Range(punto1.position.y, punto2.position.y),
                punto1.position.z
            );

            Instantiate(bolaDeFuegoPrefab, posicionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(tiempoSpawnRapido); // Doble de rápido
        }
    }

    // Coroutine para el spawn de múltiples bolas de fuego a la vez
    IEnumerator SpawnearBolasDeFuegoMultiple()
    {
        while (ataqueActual == TipoAtaque.Multiple)
        {
            int cantidadBolas = Random.Range(2, 4); // Número aleatorio de bolas de fuego entre 2 y 3

            for (int i = 0; i < cantidadBolas; i++)
            {
                Vector3 posicionSpawn = new Vector3(
                    Random.Range(punto1.position.x, punto2.position.x),
                    Random.Range(punto1.position.y, punto2.position.y),
                    punto1.position.z
                );

                Instantiate(bolaDeFuegoPrefab, posicionSpawn, Quaternion.identity);
            }

            yield return new WaitForSeconds(tiempoSpawnNormal);
        }
    }
}
