using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;

    public Animator canvasAnim;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        canvasAnim.SetBool("gameover", true);
    }

    public void GameWin()
    {
        canvasAnim.SetBool("gamewin", true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
