using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Select : MonoBehaviour{

    public void selectScene(int x){

        if(x == 1){
            SceneManager.LoadScene(2);
            Debug.Log("level1 selected");
        }

        if(x == 2){
            SceneManager.LoadScene(3);
            Debug.Log("level2 selected");
        }
        if(x == 3){
            SceneManager.LoadScene(4);
            Debug.Log("level3 selected");
        }
        if(x == 4){
            SceneManager.LoadScene(5);
            Debug.Log("level3 selected");
        }
        if(x == 0){
            SceneManager.LoadScene(0);
            Debug.Log("Go back to main menu");
        }
        if (x == 69){
            Application.Quit();
            //UnityEditor.EditorApplication.isPlaying = false;
        }

    }

}


