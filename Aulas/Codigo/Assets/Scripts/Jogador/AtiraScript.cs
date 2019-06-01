using UnityEngine;

public class AtiraScript : MonoBehaviour
{
    [SerializeField]
    private Transform _posicaoDeTiro;
    [SerializeField]
    private GameObject _lazerPrefab;

    void Update()
    {
        // Apenas detectamos se o jogador apertou o botão de tiro
        if (Input.GetButtonDown("Fire1"))
        {
            Atira();
        }
    }

    private void Atira()
    {
        // Codigo para o tiro
        Instantiate(_lazerPrefab, _posicaoDeTiro.position, _posicaoDeTiro.rotation);
    }
}
