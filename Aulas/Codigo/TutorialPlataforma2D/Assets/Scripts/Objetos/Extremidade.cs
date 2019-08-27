using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extremidade : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var jogador = collision.GetComponent<Jogador>();
            jogador.TomaDano(999);
        }
    }
}
