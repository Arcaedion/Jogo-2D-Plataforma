using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class InteragirComBotao : MonoBehaviour
{
    [SerializeField]
    private Jogador _jogador;

    [SerializeField]
    private bool _deveChecarInventario;

    [SerializeField]
    private string _nomeDoItem;

    [SerializeField]
    private UnityEvent _botaoApertado;

    private bool _podeExecutar;

    void Update()
    {
        if (_podeExecutar && _jogador.EstaInteragindo == true)
        {
            if (_deveChecarInventario)
            {
                if(_jogador.TemItem(_nomeDoItem))
                    _botaoApertado.Invoke();
            }
            else
            {
                _botaoApertado.Invoke();
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _podeExecutar = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _podeExecutar = false;

    }
}
