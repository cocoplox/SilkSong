using UnityEngine;

public class AtaqueM : MonoBehaviour
{
    public GameObject ataqueFPrefab; // Prefab de la bola de fuego
    public Transform puntoDeLanzamiento; // Punto de lanzamiento del ataque

    private float velocidadLanzamiento; // Velocidad de lanzamiento de la bola de fuego

    private Animator animator;
    private Transform jugador;

    void Start()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        velocidadLanzamiento = GetComponent<Mago>().velocidadAtaque; // Tomar la velocidad de ataque del Magoda
        InvokeRepeating("Atacar", 0, velocidadLanzamiento);
    }

    void Update()
    {
        // Orientar hacia el jugador
        transform.LookAt(jugador.position);
    }

    void Atacar()
    {
        animator.SetTrigger("AtaqueMago");
        // Instanciar la bola de fuego en el punto de lanzamiento
        GameObject ataqueFInstance = Instantiate(ataqueFPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);
        // Aplicar velocidad hacia adelante a la bola de fuego
        ataqueFInstance.GetComponent<Rigidbody2D>().velocity = puntoDeLanzamiento.right * velocidadLanzamiento;
    }
}
