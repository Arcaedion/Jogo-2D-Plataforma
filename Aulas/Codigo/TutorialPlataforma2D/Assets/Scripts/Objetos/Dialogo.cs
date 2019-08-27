using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Dialogo
{

    [SerializeField]
    private TextoDialogo[] _frases;

    [SerializeField]
    private string _nomeNpc;

    [SerializeField]
    private UnityEvent _acao;

    public TextoDialogo[] GetFrases()
    {
        return _frases;
    }

    public string GetNomeNPC()
    {
        return _nomeNpc;
    }

    public void RealizaAcao()
    {
        if (_acao != null)
            _acao.Invoke();
    }
}
