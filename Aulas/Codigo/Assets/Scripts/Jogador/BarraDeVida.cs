using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField]
    private Sprite _vidaCheia;

    [SerializeField]
    private Sprite _vidaVazia;

    [SerializeField]
    private GameObject _vida;

    private List<GameObject> _baterias = new List<GameObject>();

    public void AtualizaBarrasDeVida(int vidaAtual, int totalDeVidas)
    {
        ResetaLista();
        for (int i = 0; i < totalDeVidas; i++)
        {
            if(vidaAtual <= i)
            {
                _vida.GetComponent<Image>().sprite = _vidaVazia;
            }
            else
            {
                _vida.GetComponent<Image>().sprite = _vidaCheia;
            }

            var posXCalculada = transform.position.x + (i* 50);
            var go = Instantiate(_vida, new Vector3(posXCalculada, transform.position.y, 0), Quaternion.identity, this.transform);
            _baterias.Add(go);
        }
    }

    private void ResetaLista()
    {
        foreach (var bateria in _baterias)
        {
            Destroy(bateria);
        }
    }
}
