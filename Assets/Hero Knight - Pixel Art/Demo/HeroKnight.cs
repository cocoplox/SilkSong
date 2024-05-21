using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
public class HeroKnight : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] Text gameOverText; // Texto que se mostrará cuando el jugador muera

    private Vector3 ataquePosicionIzquierda = new Vector3(-0.7f, 0.642f, 0);
    private Vector3 ataquePosicionDerecha = new Vector3(1.077f, 0.642f, 0);

    public bool m_extraJumpUsed = false;
    



    private bool m_jumpCooldown = true;
    private float m_jumpCooldownTimer = 0.5f;
    private float m_jumpCooldownDuration = 0.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    private bool hasPlayedDeathSound = false;


    public GameObject areaAtack;//empty con el trigger de la zona de ataque

    private bool ataque;
    [SerializeField] float distanciaDelAtaque = 1.5f;
    [SerializeField] LayerMask capaDeEnemigos;

    [SerializeField] AudioClip recibirDañoClip; // Clip de audio para recibir daño
    private AudioSource audioSource; // Componente AudioSource


    


    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
        Variables.vidaActual = Variables.vidaMaxima; // Inicializamos la vida del jugador al valor máximo



        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false); // Asegurarnos de que el texto esté oculto al inicio
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Variables.vidaActual <= 0)
        {
            m_speed = 0.0f;
            m_jumpForce = 0.0f;
            m_rollForce = 0.0f;

            // Mostrar el texto de Game Over
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
            }

            // Reproducir el sonido de muerte
            

            // Reiniciar el nivel al presionar 'R'
            if (Input.GetKeyDown(KeyCode.R))
            {
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }

            return; // Salir del método para evitar más procesamiento
        }
        //Enfriamiento Doble Salto
        if (m_grounded)
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);

            // Restablecer la bandera de salto extra
            m_extraJumpUsed = false;
        }
        //Enfiramiento de salto
        if (!m_jumpCooldown)
        {
            m_jumpCooldownTimer += Time.deltaTime;
            if (m_jumpCooldownTimer >= m_jumpCooldownDuration)
            {
                m_jumpCooldown = true;
                //m_jumpCooldownTimer = 0.0f;
            }
        }

        Debug.Log("Vida actual del jugador: " + Variables.vidaActual);
        // Si la vida es menor o igual a 0, el jugador no puede moverse ni saltar ni rodar
        if (Variables.vidaActual <= 0)
        {
            m_speed = 0.0f;
            m_jumpForce = 0.0f;
            m_rollForce = 0.0f;
            return;
        }

        // Aumentar el tiempo desde el último ataque
        m_timeSinceAttack += Time.deltaTime;

        // Aumentar el tiempo que verifica la duración del rodar
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Desactivar el rodar si el temporizador excede la duración
        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        // Verificar si el personaje acaba de aterrizar en el suelo
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Verificar si el personaje acaba de empezar a caer
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Manejar entrada y movimiento --
        float inputX = Input.GetAxis("Horizontal");

        // Cambiar dirección del sprite según la dirección del movimiento
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
            ActualizarPosicionAtaque();  // Llama a la función para actualizar la posición del ataque
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
            ActualizarPosicionAtaque();
        }


        // Mover
        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        // Establecer la velocidad en el aire en el animador
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Manejar Animaciones --
        // Deslizamiento en la pared
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) && (m_wallSensorL1.State() && m_wallSensorL2.State() || Variables.isGarras);
        m_animator.SetBool("WallSlide", m_isWallSliding);

        // Muerte
        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }

        // Daño
        else if (Input.GetKeyDown("q") && !m_rolling)
            m_animator.SetTrigger("Hurt");

        // Ataque
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_currentAttack++;

            // Regresar a uno después del tercer ataque
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Restablecer la combinación de ataque si el tiempo desde el último ataque es demasiado largo
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Llamar a una de las tres animaciones de ataque "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);

            // Restablecer temporizador
            m_timeSinceAttack = 0.0f;
        }

        // Bloqueo
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Rodar
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }


        // Saltar
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        //salto por la paret
        else if (Input.GetKeyDown("space") && (m_grounded || m_isWallSliding) && !m_rolling && m_jumpCooldown)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            if (m_isWallSliding)
            {
                // Calculamos la dirección opuesta a la dirección en la que miraba el personaje antes de saltar
                int oppositeDirection = -m_facingDirection;
                // Aplicamos la fuerza para saltar de la pared en la dirección opuesta
                m_body2d.velocity = new Vector2(oppositeDirection * 20, m_jumpForce);
            }
            else
            {
                // Si está en el suelo, salta normalmente
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            }
            m_groundSensor.Disable(0.2f);

            // Activar el cooldown del salto
            m_jumpCooldown = false;
            m_jumpCooldownTimer = 0;
        }
        //Doble salto
        else if (Input.GetKeyDown("space") && (m_grounded && m_isWallSliding && (!m_grounded && !m_extraJumpUsed)) && !m_rolling && m_jumpCooldown || Variables.isAlas)
        {
            // Restablecer la bandera de salto extra
            m_extraJumpUsed = true;
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            if (m_isWallSliding)
            {
                // Calculamos la dirección opuesta a la dirección en la que miraba el personaje antes de saltar
                int oppositeDirection = -m_facingDirection;
                // Aplicamos la fuerza para saltar de la pared en la dirección opuesta
                m_body2d.velocity = new Vector2(oppositeDirection * 20, m_jumpForce);
            }
            else
            {
                // Si está en el suelo, salta normalmente
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            }
            m_groundSensor.Disable(0.2f);

            // Activar el cooldown del salto
            m_jumpCooldown = false;
            m_jumpCooldownTimer = 0;
        }

        // Correr
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Restablecer temporizador
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        // Reposo
        else
        {
            // Prevenir transiciones intermitentes al reposo
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }


    // Método para reducir la vida del jugador
    public void RecibirDaño(int cantidad)
    {
        if (Variables.vidaActual > 0)
        {
            Variables.vidaActual -= cantidad; // Reducir la vida del jugador
            m_animator.SetTrigger("Hurt"); // Activar la animación de ser golpeado

            // Reproducir el sonido de recibir daño
            if (audioSource != null && recibirDañoClip != null)
            {
                audioSource.PlayOneShot(recibirDañoClip);
            }

            if (Variables.vidaActual <= 0)
            {
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death"); // Activar la animación de muerte si la vida llega a cero
                                                // Aquí puedes añadir cualquier otra lógica que quieras cuando el jugador muere
            }
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Establecer la posición de spawn correcta
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Girar el polvo en la dirección correcta
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Método para establecer ataque en true
    void SetAtaqueTrue()
    {
        ataque = true;

        areaAtack.SetActive(true);
    }

    // Método para establecer ataque en false cuando finaliza la animación de ataque
    void SetAtaqueFalse()
    {
        ataque = false;
        areaAtack.SetActive(false);
    }
    public float GetCurrentHealth()
    {
        return Variables.vidaActual;
    }

    void ActualizarPosicionAtaque()
    {
        if (m_facingDirection == 1)
        {
            areaAtack.transform.localPosition = ataquePosicionDerecha;
        }
        else
        {
            areaAtack.transform.localPosition = ataquePosicionIzquierda;
        }
    }


    /*void RealizarAtaque()
    {
        if (ataque == true)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * m_facingDirection, distanciaDelAtaque, capaDeEnemigos);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("Enemigo"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }*/
}
