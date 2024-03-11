using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject gameManager;

    private int vidas;

    public int vidasGlobal;

    public int puntuacionGlobal;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        DontDestroyOnLoad(gameManager);
        cambiarEscena("Menu");
        vidas = vidasGlobal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    public void decrementarVidas()
    {
        vidasGlobal--;
    }

    public void sumarVidas()
    {
        vidasGlobal++;
    }

    public void sumarPuntos()
    {
        puntuacionGlobal++;
    }
    public void sumarPuntos(int puntos)
    {
        puntuacionGlobal += puntos ;
    }

    public void restarPuntos()
    {
        puntuacionGlobal--;
    }

    public int getVidas()
    {
        return vidasGlobal;
    }

    public int getPuntos()
    {
        return puntuacionGlobal;

    }
    public void setPuntosReset()
    {
      puntuacionGlobal = 0;
    }
    public void setVidasReset()
    {
        vidasGlobal = vidas;
    }

}
