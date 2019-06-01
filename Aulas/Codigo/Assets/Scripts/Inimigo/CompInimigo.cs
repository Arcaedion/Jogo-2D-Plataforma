using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompInimigo : MonoBehaviour
{
    [SerializeField]
    private int _vida = 3;

    [SerializeField]
    private GerenciadorDeSons _gerenciadorSons;

    public void TomaDano(int dano)
    {
        _vida -= dano;

        if(_vida <= 0)
        {
            Morre();
        }
    }

    private void Morre()
    {
        if(_gerenciadorSons != null)
        {
            _gerenciadorSons.TocaAudio("MorteVogon");
        }
        Destroy(gameObject);
    }
}
