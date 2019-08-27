using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeSons : MonoBehaviour
{

    [SerializeField]
    private AudioClip _morte;

    private List<AudioSource> _audios;

    private void Awake()
    {

        _audios = new List<AudioSource>();
        if(_morte != null)
        {
            var ac = gameObject.AddComponent<AudioSource>();
            ac.clip = _morte;
            ac.name = "Morte";
            _audios.Add(ac);
        }
    }

    public void TocaAudio(string nome)
    {
        foreach(var audio in _audios)
        {
            if (audio.name.Equals(nome))
            {
                audio.Play();
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
