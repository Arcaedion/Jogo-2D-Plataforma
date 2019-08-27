using Arcaedion.DevDasGalaxias;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controle2D))]
[RequireComponent(typeof(Animator))]
public class IAInimigo : MonoBehaviour
{
    [SerializeField]
    private Jogador _jogador;
    [SerializeField]
    private float _velocidade = 20f;
    [SerializeField]
    private LayerMask _layersPermitidas;
    [SerializeField]
    private Vector2 _rayCastOffset;
    [SerializeField]
    private float _rangeDetectar;
    [SerializeField]
    private bool _modoZumbi = false;

    private Rigidbody2D _rb;
    private Controle2D _controle;
    private float _movimentoHorizontal;
    private RaycastHit2D _raycastParedeDireitaInfo;
    private int _andandoParaDireita;
    private Animator _animator;
    private bool _seguindoJogador;
    private bool _estaPulando;

    public void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _controle = GetComponent<Controle2D>();
        _andandoParaDireita = 1;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        AplicaMovimento();
        DetectaJogador();
        DetectaParede();
        DetectaBeira();
    }

    private void DetectaParede()
    {
        var origemX = transform.position.x + _rayCastOffset.x;
        var origemY = transform.position.y + _rayCastOffset.y;
        _raycastParedeDireitaInfo = Physics2D.Raycast(new Vector2(origemX, origemY), Vector2.right, 0.5f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.right, Color.cyan);
        if (_raycastParedeDireitaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = -1;
            }
            else
            {
                Pula();
            }
        }

        var raycastParedeEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y + _rayCastOffset.y), Vector2.left, 0.5f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x, origemY), Vector2.left, Color.cyan);
        if (raycastParedeEsquerdaInfo.collider != null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = 1;
            }
            else
            {
                Pula();
            }
        }
    }

    private void Pula()
    {
        if(_controle.GetEstaNoChao())
        {
            _estaPulando = true;
        }
    }

    private void DetectaBeira()
    {
        var raycastChaoDireitaInfo = Physics2D.Raycast(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, 1f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x + _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoDireitaInfo.collider == null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = -1;
            }
            else
            {
                Pula();
            }
        }

        var raycastChaoEsquerdaInfo = Physics2D.Raycast(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, 1f, _layersPermitidas);
        Debug.DrawRay(new Vector2(transform.position.x - _rayCastOffset.x, transform.position.y), Vector2.down, Color.red);
        if (raycastChaoEsquerdaInfo.collider == null)
        {
            if (!_seguindoJogador)
            {
                _andandoParaDireita = 1;
            }
            else
            {
                Pula();
            }
        }
    }

    private void DetectaJogador()
    {
        var diferencaParaJogador = _jogador.gameObject.transform.position.x - transform.position.x;
        _seguindoJogador = Mathf.Abs(diferencaParaJogador) < _rangeDetectar;
        if(_modoZumbi && _seguindoJogador)
        {
            if(diferencaParaJogador < 0)
            {
                _andandoParaDireita = -1;
            }
            else
            {
                _andandoParaDireita = 1;
            }
        }
    }

    private void AplicaMovimento()
    {
        _movimentoHorizontal = _andandoParaDireita * _velocidade;
        _animator.SetFloat("Velocidade", Math.Abs(_rb.velocity.x));
    }

    void FixedUpdate()
    {
        _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _estaPulando);
        if (_estaPulando)
            _estaPulando = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeDetectar);
    }
}
