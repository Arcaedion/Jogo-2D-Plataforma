using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    [SerializeField]
    private int _vidaMax = 3;

    [SerializeField]
    private int _vidaAtual;

    [SerializeField]
    private BarraDeVida _barraDeVida;

    [SerializeField]
    private List<string> _itemsDeInventario = new List<string>();

    public bool EstaInteragindo { get; set; }

    private void Awake()
    {
        AtualizaVidaUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interage"))
        {
            EstaInteragindo = true;
        }
        else
        {
            EstaInteragindo = false;
        }
    }

    public void AdicionaItem(string nomeItem)
    {
        _itemsDeInventario.Add(nomeItem);
    }

    public bool TemItem(string nomeItem)
    {
        return _itemsDeInventario.Contains(nomeItem);
    }

    public void TomaDano(int dano)
    {
        _vidaAtual -= dano;
    }

    public void AtualizaVidaUI()
    {
        _barraDeVida.AtualizaBarrasDeVida(_vidaAtual, _vidaMax);
    }
}
