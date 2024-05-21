using UnityEngine;

public class Mago : MonoBehaviour
{
    public float velocidadAtaque = 2.0f; // Velocidad de ataque en segundos
    public GameObject ataquePrefab; // Prefab del ataque
    public Transform puntoDeLanzamiento; // Punto de lanzamiento del ataque
    public AnimationClip animacionAtaqueMago; // Animación de ataque del mago

    private Transform jugador;
    private bool atacando = false;
    private Animator animator;
    private HeroKnight jugadorScript;
    private VidaEnemigo vidaMago;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        jugadorScript = jugador.GetComponent<HeroKnight>();
        vidaMago = GetComponent<VidaEnemigo>(); // Obtener el componente de vida del mago
    }

    void Update()
    {
        // Girar el sprite del mago dependiendo de la posición del jugador
        if (jugador.position.x > transform.position.x)
        {
            // El jugador está a la derecha, el sprite mira hacia la derecha
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // El jugador está a la izquierda, el sprite mira hacia la izquierda
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Detener los ataques si el jugador ha muerto
        if (vidaMago != null && vidaMago.GetVidaActual() <= 0)
        {
            StopAtacar();
        }
    }

    public void StartAtacar()
    {
        atacando = true;
        InvokeRepeating("LanzarAtaque", 0, velocidadAtaque);
    }

    public void StopAtacar()
    {
        atacando = false;
        CancelInvoke("LanzarAtaque");
    }

    void LanzarAtaque()
    {
        if (atacando && ataquePrefab != null && puntoDeLanzamiento != null && animacionAtaqueMago != null)
        {
            // Reproducir la animación de ataque del mago
            animator.Play(animacionAtaqueMago.name);

            // Invocar la creación de la bola de fuego después de la duración de la animación de ataque
            float duracionAtaque = animacionAtaqueMago.length;
            Invoke("CrearBolaDeFuego", duracionAtaque);
        }
        else
        {
            Debug.LogError("Falta asignar el prefab del ataque, el punto de lanzamiento o la animación del Mago.");
        }
    }

    void CrearBolaDeFuego()
    {
        // Crear la bola de fuego
        GameObject ataque = Instantiate(ataquePrefab, puntoDeLanzamiento.position, Quaternion.identity);
        Animator ataqueAnimator = ataque.GetComponent<Animator>();
        if (ataqueAnimator == null)
        {
            Debug.LogWarning("El prefab del ataque no tiene un componente Animator.");
        }
    }
    
}
