using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIempo : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            collision.gameObject.GetComponent<Jugador>().SumarTiempo();
            Destroy(gameObject); //Desaparece el objeto
        }
    }


}
