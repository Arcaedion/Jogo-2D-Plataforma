using UnityEngine;

namespace Arcaedion.DevDasGalaxias
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Controle2D : MonoBehaviour
    {
        #region Variaveis do editor
        [SerializeField]
        private float _forcaDoPulo = 200f;          // Força do pulo
        [SerializeField]
        private LayerMask _camadaChao;              // Qual camada é considerada chão
        [SerializeField]
        private Transform _posicaoDeBaixo;          // Posição do pé
        
        #endregion

        #region Variaveis privadas
        private float _suavizacaoMovimento = .05f;  // Suavização de movimento. Adicionar [SerializeField] se precisar alterar no editor
        private bool _controleAereo = true;         // Habilitar controle aéreo. Adicionar [SerializeField] se precisar alterar no editor
        private bool _estaNoChao;                   // Verdadeiro caso o jogador esteja tocando no chão
        private Rigidbody2D _rigidbody2D;           // O objeto Rigidbody2D que está acoplado ao objeto de jogo do jogador
        private bool _viradoParaDireita = true;     // Verdadeiro se o jogador estiver virado para a direita
        const float _raioParaChao = .3f;            // O Raio do circulo ou caixa
        #endregion

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D que está acoplado ao jogador e salva na variável privada
        }


        private void FixedUpdate()
        {
            _estaNoChao = false;

            // É considerado que está no chão caso algum cast à posição de baixo detecte algum objeto de jogo que seja considerado chão
            // Isso é feito usando camadas(layers)
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_posicaoDeBaixo.position, _raioParaChao, _camadaChao);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    _estaNoChao = true;
            }
        }

        public bool GetEstaNoChao()
        {
            return _estaNoChao;
        }

        /// <summary>
        /// Controla o movimento do jogador a partir de uma quantidade de movimento aplicada e um valor booleano que define se o jogador está tentando pular.
        /// </summary>
        /// <param name="qtdMovimento">A quantidade de movimento aplicada</param>
        /// <param name="pulando">Verdadeiro se o jogador estiver tentando pular</param>
        public void Movimento(float qtdMovimento, bool pulando)
        {
            // Apenas permite movimento caso o jogador esteja no chão. 
            // Se o controle aereo estiver ativado, o movimento também é ativado.
            if (_estaNoChao || _controleAereo)
            {
                AplicaMovimento(qtdMovimento);
                DetectaGirar(qtdMovimento);
            }
            // Se o jogador está no chão
            if (_estaNoChao && pulando)
            {
                // Add a vertical force to the player.
                _estaNoChao = false;
                _rigidbody2D.AddForce(new Vector2(0f, _forcaDoPulo));
            }
        }

        /// <summary>
        /// Aplica movimento ao jogador
        /// </summary>
        /// <param name="qtdMovimento">A quantidade de movimento aplicada</param>
        private void AplicaMovimento(float qtdMovimento)
        {
            // Encontra a velocidade do jogador
            var velocidadeJogador = new Vector2(qtdMovimento * 10f, _rigidbody2D.velocity.y);
            // Suavizando a velocidade de movimento
            Vector3 velocity = Vector3.zero;
            _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, velocidadeJogador, ref velocity, _suavizacaoMovimento);
        }

        /// <summary>
        /// Detecta se o jogador precisa ser girado.
        /// </summary>
        /// <param name="qtdMovimento">A quantidade de movimento aplicada</param>
        private void DetectaGirar(float qtdMovimento)
        {
            // Se a quantidade de movimento é maior que 0, significa que a velocidade aplicada é para a direita
            // Se o jogador estiver virado para a esquerda, devemos girar ele para a direita e vice-versa
            if (qtdMovimento > 0 && !_viradoParaDireita)
            {
                GiraJogador();
            }
            else if (qtdMovimento < 0 && _viradoParaDireita)
            {
                GiraJogador();
            }
        }

        private void GiraJogador()
        {
            // Troca o valor do boolean
            _viradoParaDireita = !_viradoParaDireita;

            transform.Rotate(0f, 180f, 0f);
        }
    }
}