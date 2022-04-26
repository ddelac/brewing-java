using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    private void Update()
    {
        //if r key is pressed
        //restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(5);//load current Game Scene (indexed at 1)
        }

        if(Input.GetKeyDown(KeyCode.Q) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);//load current Game Scene (indexed at 1)
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
