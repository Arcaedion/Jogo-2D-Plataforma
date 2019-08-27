using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemDeInventario : MonoBehaviour
{
    [SerializeField]
    private string _nome;

    [SerializeField]
    private AudioClip _som;

    private CircleCollider2D _circleCollider;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            var jogador = collision.gameObject.GetComponent<Jogador>();
            jogador.AdicionaItem(_nome);
            if(_som)
                AudioSource.PlayClipAtPoint(_som, transform.position);
            Destroy(this.gameObject);
        }
    }
}
