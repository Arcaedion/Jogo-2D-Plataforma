using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Jogador : MonoBehaviour
{
    [SerializeField]
    private int _vidaMax = 3;

    [SerializeField]
    private int _vidaAtual;

    [SerializeField]
    private BarraDeVida _barraDeVida;

    [SerializeField]
    private GameObject _arma;

    [SerializeField]
    private Collider2D _areaDeAtaque;

    [SerializeField]
    private List<string> _itemsDeInventario = new List<string>();

    private bool _meleeHabilitado;
    private Animator _animator;

    public bool EstaInteragindo { get; set; }

    private void Awake()
    {
        AtualizaVidaUI();
        _animator = GetComponent<Animator>();
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

        if (Input.GetButtonDown("Fire2"))
        {
            _animator.SetTrigger("TriggerAtaque");
        }

    }

    public void IniciaAtaque()
    {
        _areaDeAtaque.enabled = true;
    }

    public void TerminaAtaque()
    {
        _areaDeAtaque.enabled = false;
    }

    public void AdicionaItem(string nomeItem)
    {
        _itemsDeInventario.Add(nomeItem);
        if (TemItem("toalha"))
        {
            HabilitaMelee();
        }
    }

    private void HabilitaMelee()
    {
        _meleeHabilitado = true;
        _arma.SetActive(true);
    }


    public bool TemItem(string nomeItem)
    {
        return _itemsDeInventario.Contains(nomeItem);
    }

    public void TomaDano(int dano)
    {
        _vidaAtual -= dano;
        
        if(_vidaAtual <= 0)
        {
            Morre();
        }
    }

    private void Morre()
    {
        Destroy(gameObject);
    }


    public void AtualizaVidaUI()
    {
        _barraDeVida.AtualizaBarrasDeVida(_vidaAtual, _vidaMax);
    }
}
