using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class virgenAldea : MonoBehaviour
{
    private Jugador jugador;
    void Start()
    {
        jugador = FindObjectOfType<Jugador>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            jugador.SumaPuntuacionTiempo();
            SceneManager.LoadScene("Castillo");
        }
    }
}
