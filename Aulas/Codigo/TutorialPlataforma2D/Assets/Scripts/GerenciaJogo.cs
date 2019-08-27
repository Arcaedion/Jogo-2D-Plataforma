using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciaJogo : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var cena = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cena.name);
        }
    }
}
