using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcaedion.DevDasGalaxias
{
    [RequireComponent(typeof(Controle2D))]
    public class MovimentoDeJogador : MonoBehaviour
    {
        [SerializeField]
        private float _velocidade = 50f;

        private Controle2D _controle;
        private float _movimentoHorizontal = 0f;
        private bool _pulando = false;

        private void Awake()
        {
            _controle = GetComponent<Controle2D>(); // Obtém o componente Controle2D que está acoplado ao jogador e salva na variável privada
        }

        void Update()
        {
            // Obtém o movimento horizontal que está sendo aplicado: esquerdo ou direito.
            _movimentoHorizontal = Input.GetAxisRaw("Horizontal") * _velocidade;
            // Detecta se o botão de pulo foi pressionado
            if (Input.GetButtonDown("Jump"))
            {
                _pulando = true;
            }
        }

        void FixedUpdate()
        {
            // Aplica movimento ao jogador
            _controle.Movimento(_movimentoHorizontal * Time.fixedDeltaTime, _pulando);
            _pulando = false;
        }
    }
}
