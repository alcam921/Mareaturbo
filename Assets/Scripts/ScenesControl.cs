using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesControl : MonoBehaviour
{
    public static ScenesControl instanciar = null;

    private void Awake()
    {
        if (instanciar == null)
        {
            instanciar = this;
        }
        else if (instanciar != null)
        {
            Destroy(gameObject);
        }
    }

    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void ExitGame() 
    {
        Application.Quit();    
    }

}