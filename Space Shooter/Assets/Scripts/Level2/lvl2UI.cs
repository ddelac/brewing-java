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
    [SerializeField]
    private Image _victory;

    //game manager variables
    private GameManager _gameManager;

    private lvl2boss _boss;
    private Asteroidlvl2 _asteroid;

    private float startTime;
    private bool finished = false;

    void Start(){
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _menuText.gameObject.SetActive(false);
        _victory.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _boss = GameObject.Find("BOSS").GetComponent<lvl2boss>();
        _asteroid = GameObject.Find("Asteroid").GetComponent<Asteroidlvl2>();
        if(_gameManager == null){
            Debug.LogError("GameManager is null");
        }
        startTime = Time.time;
    }

    public void UpdateScore(int playerScore){
        _scoreText.text = "Score:" + playerScore;
    }

    public void UpdateLives(int currentLives){

        _LivesImg.sprite = _liveSprites[currentLives];
        if(currentLives == 0){
            GameOverSequence();
            dead();
        }
    }

    IEnumerator GameOverFlicker(){
        while (true){
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }

    void GameOverSequence(){
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _menuText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    void Update(){
        if(_boss.win == true){
            w();
        }
        if(finished){
            return;
        }
        if(_asteroid.startTimer == true){
            float t = Time.time - startTime;
            string minutes = ((int) t/60).ToString();
            string seconds = (t%60).ToString("f2");
            _Timetext.text = "Time: " + minutes + ":" + seconds;
        }
    }

    public void dead(){
        finished = true;
        _Timetext.color = Color.red;
    }

    public void w(){
        finished = true;
        _Timetext.color = Color.green;
        _victory.gameObject.SetActive(true);
    }

}
