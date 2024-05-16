using UnityEngine;

public class SpawnerBT : MonoBehaviour
{
    public GameObject prefabBT; // Prefab de la bola de fuego
    public bool derecha = true; // Si es true, la bola se moverá hacia la derecha
    public bool izquierda = false; // Si es true, la bola se moverá hacia la izquierda
    public float cooldown = 1.0f; // Tiempo de enfriamiento entre spawns
    private float tiempoUltimoSpawn; // Tiempo del último spawn

    void Start()
    {
        // Inicializar el tiempo del último spawn al inicio del juego
        tiempoUltimoSpawn = -cooldown;
    }

    void Update()
    {
        SpawnBT();
    }

    public void SpawnBT()
    {
        // Verificar si ha pasado suficiente tiempo desde el último spawn
        if (Time.time - tiempoUltimoSpawn >= cooldown)
        {
            // Instanciar el prefab de la bola de fuego
            GameObject bola = Instantiate(prefabBT, transform.position, Quaternion.identity);

            // Obtener el componente BT del objeto instanciado
            BT btScript = bola.GetComponent<BT>();

            // Verificar si el script BT existe en el objeto instanciado
            if (btScript != null)
            {
                // Establecer los valores de los booleanos en el script BT según los valores definidos en el spawner
                btScript.derecha = derecha;
                btScript.izquierda = izquierda;
            }
            else
            {
                Debug.LogWarning("El prefab de la bola de fuego no tiene el componente BT.");
            }

            // Actualizar el tiempo del último spawn al tiempo actual
            tiempoUltimoSpawn = Time.time;
        }
    }
}
