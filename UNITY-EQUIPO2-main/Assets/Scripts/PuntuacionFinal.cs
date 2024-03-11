using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuacionFinal : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager gameManager;

    //Control del Canvas
    public Canvas canvas;
    private ControlHUD hud;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        hud = canvas.GetComponent<ControlHUD>();
        hud.setPuntuacionTXT(gameManager.getPuntos());

        gameManager.setVidasReset();
        gameManager.setPuntosReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
