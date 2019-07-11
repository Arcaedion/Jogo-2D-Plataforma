using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class GerenciadorTimeline : MonoBehaviour
{
    [SerializeField]
    private Animator _animatorJogador;

    private PlayableDirector _diretor;
    private RuntimeAnimatorController _controlador;
    private bool _consertado = false;

    void Awake()
    {
        _diretor = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        _controlador = _animatorJogador.runtimeAnimatorController;
        _animatorJogador.runtimeAnimatorController = null;
    }

    void Update()
    {
        if(_diretor.state != PlayState.Playing && !_consertado)
        {
            _animatorJogador.runtimeAnimatorController = _controlador;
            _consertado = true;
        }
    }
}
