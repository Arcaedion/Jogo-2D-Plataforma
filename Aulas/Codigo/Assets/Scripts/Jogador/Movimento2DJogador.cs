using Arcaedion.DevDasGalaxias;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controle2D))]
[RequireComponent(typeof(Animator))]
public class Movimento2DJogador : MonoBehaviour
{

    [SerializeField]
    private float _velocidade = 30f;

    private Controle2D _controle;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _movimentoHorizontal;
    private bool _pulando;

    private void Awake()
    {
        _controle = GetComponent<Controle2D>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        _movimentoHorizontal = Input.GetAxisRaw("Horizontal") * _velocidade;
        
        if (Input.GetButtonDown("Jump"))
        {
            _pulando = true;
            
        }


        if (_controle.GetEstaNoChao())
        {
            _animator.SetFloat("Velocidade", System.Math.Abs(_rigidbody.velocity.x));
            _animator.SetBool("Pulando", false);
        }
        else
        {
            _animator.SetBool("Pulando", true);
        }
            
    }

    void FixedUpdate()
    {
        _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _pulando);
        _pulando = false;
    }
}
