using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitaVidas : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.gameObject.GetComponent<Jugador>().quitarUnaVida();
            Destroy(gameObject); //Desaparece el objeto
            Debug.Log("-1");
        }
    }
}
