using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    //Movimiento del jugador
    [Range(1, 10)] public float velocidad;
    Rigidbody2D rb2d;
    SpriteRenderer spRd;
    private Vector3 posInicial; //Usado para la caida por precipicio
    //Salto del jugador
    //Para averiguar si esta saltando. De esta forma no permitiremos que se salte varias veces en el aire
    bool isJumping = false;
    [Range(1, 500)] public float potenciaSalto; //Potencia de salto

    //Para la utilizacion del Animator del jugador
    private Animator animator;

    //Para controlar cuando coincida con enemigos
    private bool vulnerable;

    //Control de vidas con GameManager
    private GameManager gameManager;

    //Control del Canvas
    public Canvas canvas;
    private ControlHUD hud;

    //Control de tiempo
    public int tiempoempleado; //Pasar a private tras las pruebas
    public float tiempoInicio; //Pasar a private tras las pruebas

    public int tiempoNivel;

    //Sonido de salto
    private AudioSource SonidoSalto;

    //Movimiento Joystick
    public Joystick joystick;
    private float movimientoH;
    private float movimientoV;

    // Start is called before the first frame update

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //Busco mi objeto GameManager
        gameManager = FindObjectOfType<GameManager>();
        vulnerable = true;

        tiempoInicio = Time.time;
        hud = canvas.GetComponent<ControlHUD>();
        hud.setVidasTXT(gameManager.getVidas());
        hud.setPuntuacionTXT(gameManager.getPuntos());

        posInicial = transform.position;

        SonidoSalto = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        tiempoempleado = (int)(Time.time - tiempoInicio);

        if ((tiempoNivel - tiempoempleado <= 0))
        {
            SceneManager.LoadScene("FinDelJuego");
        }
        hud.setTiempoTXT(tiempoNivel - tiempoempleado);


        //Averiguo si estoy parado (0), moviendome hacia la derecha (1) o moviendome hacia la izquierda (-1)
        if ((joystick.Horizontal >= .2f) | (joystick.Horizontal <= -.2f))
        {
            movimientoH = joystick.Horizontal;
        }
        else
        {
            movimientoH = 0f;
        }

        //Eje x:
        //movimientoH: Para indicar la direccion del movimiento
        //Eje y:
        //Obtengo la que tenia antes mediante rb2d.velocity.y
        //rb2d.velocity: Le indica al Rigidbody2d la velocidad que queremos que tenga
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        //Comprobamos el valor de movimientoH , si te mueves a la derecha el personaje mira a la derecha y viceversa
        if (movimientoH > 0)
        {
            spRd.flipX = false;
        }
        else if (movimientoH < 0)
        {
            spRd.flipX = true;
        }


        //Movimiento de salto
        if (isJumping)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        //Si no se mueve tendra una animacion y si se mueve tendra otra, lo hacemos con el isWalking que lo declaramos anteriormente en Animator
        if (movimientoH != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //Si se pullsa el botón de saltar (Jump) y además ya no estoy saltando
        if (Input.GetButton("Jump") && !isJumping)
        {
            //Añado una fuerza al RigidBody con un parámetro de entrada que es un vector de 
            //2 dimensiones en el cual la x se queda en 0 y la y pasa a 1
            //Multiplico el eje y por la potencia de salto ya que x=0
            rb2d.AddForce(Vector2.up * potenciaSalto);
            //Indico que estoy saltando
            isJumping = true;
            SonidoSalto.Play();
        }
    }
    public void onBotonSaltar()
    {
        if (!isJumping)
        {
            rb2d.AddForce(Vector2.up * potenciaSalto);
            //Indico que estoy saltando
            isJumping = true;
            SonidoSalto.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("Plataformas"))
        {
            isJumping = false;
            //Reducimos la velocidad en el eje y a 0 (le quitamos la fuerza de salto)
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
        else if (otherObject.gameObject.CompareTag("Caida")) 
        {
            quitarUnaVida();
            transform.position = posInicial;
        }
    }
    public void QuitarVida()
    {
        if (vulnerable)
        {
            vulnerable = false;
            gameManager.decrementarVidas();
            hud.setVidasTXT(gameManager.getVidas());
            if (gameManager.getVidas() == 0)
            {
                SceneManager.LoadScene("FinDelJuego");
            }
            Invoke("HacerVulnerable", 1f); //Cuando pase un segundo vuelves a hacerle vulnerable
            spRd.color = Color.red;
        }
    }
    private void HacerVulnerable()
    {
        vulnerable = true;
        spRd.color = Color.white;
    }

    public void sumarUnaVida()
    {
      gameManager.sumarVidas();
      hud.setVidasTXT(gameManager.getVidas());
    }

    public void quitarUnaVida()
    {
        gameManager.decrementarVidas();
        if (gameManager.getVidas() == 0)
        {
            SceneManager.LoadScene("FinDelJuego");
        }
        hud.setVidasTXT(gameManager.getVidas());

    }

    public void IncrementarPuntos()
    {
        gameManager.sumarPuntos();
        hud.setPuntuacionTXT(gameManager.getPuntos());
    }

    public void DecrementarPuntos()
    {
        gameManager.restarPuntos();
        hud.setPuntuacionTXT(gameManager.getPuntos());
    }

    public void SumaPuntuacionTiempo()
    {
        int puntos =(int)(tiempoNivel-(tiempoempleado - tiempoInicio))/10;
        gameManager.sumarPuntos(puntos);
    }

    public void SumarTiempo()
    {
        tiempoInicio += 10;
    }

}
