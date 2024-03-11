using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaFinal : MonoBehaviour
{
    // Start is called before the first frame update
    private Jugador jugador;
    void Start()
    {
        jugador = FindObjectOfType<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            jugador.SumaPuntuacionTiempo();
            SceneManager.LoadScene("FinDelJuegoWin");
        }
    }
}
