using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesControl : MonoBehaviour
{
    public static ScenesControl instanciar = null;

    [SerializeField] private Animator transitionAnimator;

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
    public void LoadSceneWithTransition(string cena)
    {
        StartCoroutine(LoadTransition(transitionAnimator, 0.3f, cena));
    }
    private IEnumerator LoadTransition(Animator anim,  float time, string scene)
    {
        anim.enabled = true;
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame() 
    {
        Application.Quit();    
    }

}