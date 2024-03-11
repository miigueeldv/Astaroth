using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBotonJugar()
    {
        SceneManager.LoadScene("Campo");
    }
    public void onBotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void onBotonSalir()
    {
        Application.Quit();
    }
    public void onBotonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void onBotonReiniciar()
    {
        SceneManager.LoadScene("Campo");
    }
    public void onBotonElegirNivel()
    {
        SceneManager.LoadScene("SeleccionarEscena");
    }
    public void onBotonCampo()
    {
        SceneManager.LoadScene("Campo");
    }
    public void onBotonAldea()
    {
        SceneManager.LoadScene("Aldea");
    }
    public void onBotonCastillo()
    {
        SceneManager.LoadScene("Castillo");
    }

}
