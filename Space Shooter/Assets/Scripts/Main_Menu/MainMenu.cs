using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameMenu(int x)
    {
        //load the game scene
        if(x == 0){
            SceneManager.LoadScene(1); //main game scene
        }
        if(x == 1){
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }
       
    }
}