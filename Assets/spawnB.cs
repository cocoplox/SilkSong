using System.Collections;
using UnityEngine;

public class spawnB : MonoBehaviour
{
    public RataB enemigoPrefab; // Prefab del enemigo a spawnear
    public Transform punto1; // Primer punto de destino
    public Transform punto2; // Segundo punto de destino
    public float tiempoSpawn = 5.0f; // Tiempo entre spawns
    public Transform spawnPoint; // Punto de spawn de las ratas

    void Start()
    {
        StartCoroutine(SpawnearEnemigos());
    }

    IEnumerator SpawnearEnemigos()
    {
        while (true)
        {
            RataB nuevaRata = Instantiate(enemigoPrefab, spawnPoint.position, Quaternion.identity, transform);
            nuevaRata.punto1 = punto1;
            nuevaRata.punto2 = punto2;
            yield return new WaitForSeconds(tiempoSpawn);
        }
    }
}
