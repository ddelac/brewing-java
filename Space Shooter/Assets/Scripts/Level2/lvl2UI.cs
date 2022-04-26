using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//need for changing UI elements in Unity


public class lvl2UI : MonoBehaviour
{
    //score variable
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    public Text _Timetext;

    //lives ui image variables
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImg;

    //game over ui image variable
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Text _menuText;

    //game manager variables
    private GameManager _gameManager;

    private float startTime;
    private bool finished = false;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;//initialize the score at the top right of screen to 0
        _gameOverText.gameObject.SetActive(false);//keep the game over text hidden while currentLives > 0
        _restartText.gameObject.SetActive(false);//keep the restart text hidden while currentLives > 0
        _menuText.gameObject.SetActive(false);//keep the restart text hidden while currentLives > 0
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is null");
        }
        startTime = Time.time;
    }

    //update the score displayed
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore; //update the score at the top right of the screen
    }


    //update the lives displayed 
    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the current lives index
        _LivesImg.sprite = _liveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
            dead();
        }
    }

    //make the game over text flicker on and off for .5 seconds each
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);//make the game over text visable to the player
        _restartText.gameObject.SetActive(true);
        _menuText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());//make game over text flicker on and off
    }

    void Update(){
        if(finished){
            return;
        }
        float t = Time.time - startTime;
        string minutes = ((int) t/60).ToString();
        string seconds = (t%60).ToString("f2");

        _Timetext.text = "Time: " + minutes + ":" + seconds;
    }

    public void dead(){
        finished = true;
        _Timetext.color = Color.red;
    }

}
