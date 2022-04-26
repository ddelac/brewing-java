using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    public static bool paused = false;

    public GameObject pauseMenu;

    private void Update()
    {
        //if r key is pressed
        //restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);//load current Game Scene (indexed at 1)
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //PauseMenu.gameObject.SetActive(true);
        }


    }

    public void GameOver()
    {
        _isGameOver = true;
    }


    
}
