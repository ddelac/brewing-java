using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    //initialize global variables

    //speed variables
   [SerializeField]
    private float _speed = 5f;//holds the global speed variable in m/s
    private float _SpeedMutiliplier = 2;

    //laser variables
    [SerializeField]
    private GameObject _laserPrefab;//holds the gameobject of laserprefab for spawning lasers
    [SerializeField]
    private GameObject _TripleShotPrefab;//holds the gameobject of TripleShot for spawning lasers
    [SerializeField]
    private float _fireRate = 0.5f;//holds the fire rate of the laser
    private float _canFire = -.1f;//used to stop unlimited firing of the laser

    //lives and spawn manager
    [SerializeField]
    private int _lives = 4;//holds the amount of lives the player has
    private SpawnManager _spawnManager;//holds the gameobject of the spawn manager for spawning enemies

    //powerup boolean values
    private bool _isTripleShotActive = false;//used to determine if triple shot is active
    private bool _isShieldActive = false;

    //shield manager variables
    [SerializeField]
    private GameObject _shieldVisualizer;

    //player damaage visualization variables
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    //ui manager variables
    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    //audio control variables
    [SerializeField]
    private AudioClip _laserShot;
    private AudioSource _audioSource;

    //explosion effect
    [SerializeField]
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start(){
        //take the current position and give it a start position(0,0,0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        //null check on _spawnManager
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }
        //null check on _uiManager
        if (_uiManager == null)
        {
            Debug.LogError("UI manager is null");
        }
        //null check on _audioSource
        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source on the player is NULL");
        }//end if
        else
        {
            _audioSource.clip = _laserShot;
        }
    }//end start

    // Update is called once per frame
    void Update(){
        //call movement
        movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            shooting();
        }
            

    }//end update

    void movement(){

        //movement 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //                  new Vector3(1,0,0) * UsrInput * 3.5 * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //optimization of above code
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        //cieling and floor on Y axis 
        if (transform.position.y >= 2){
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -4){
            transform.position = new Vector3(transform.position.x, -4, 0);
        }//end if else
        //optimization of above code
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.posotion.y,-4,0),0);
        //Math.Clamp(variable to clamp, value 1, value 2) keeps clamped variable between value 1 and value 2

        //wrapping the X axis' 
        if (transform.position.x > 11){
            transform.position = new Vector3(-10, transform.position.y, 0);
        }
        else if (transform.position.x < -11){
            transform.position = new Vector3(10, transform.position.y, 0);
        }//end if ekse

    }//end movement

    void shooting()
    {
        //if i hit the space key
        //spawn game object
        //Debug.Log("Space Key Pressed");
        //Shows message in the terminal if working properly
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            //instantiate 3 lasers(triple shot prefab)
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            //instantiate 1 laser                          //offset for laser to instantiate above player and not in player

            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);

        }

        //play the audio clip for firing the laser
        _audioSource.Play();
       
       
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        //if lives = 2 make the visual player damage on left side active
        //else if lives == 1 make the visual on the right side active
        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        //update lives ui display
        _uiManager.UpdateLives(_lives);

        //check if we are dead
        if(_lives < 1)
        {
            //communicate with spawn manager
            //let them know to stop spawning
            //destory Player object
            _spawnManager.onPlayerDeath();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void onTripleShotCollection()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }


    //IEnumerator tripleshot powerdownroutine
    //wait 5 seconds
    //set triple shot to false

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;

    }

    public void onSpeedBoostCollection()
    {
        _speed *= _SpeedMutiliplier;
        StartCoroutine(SpeedBoostPowerDown());
    }

    //IEnumerator speedboost powerdownroutine
    //wait 5 seconds
    //set speed boost to false

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _SpeedMutiliplier;

    }

    public void onShieldCollection()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void addScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void addlife(){
        if(_lives == 2){
            _lives = 3;
            _leftEngine.SetActive(false);
            _uiManager.UpdateLives(_lives);
        }
        if(_lives == 1){
            _lives = 2;
            _rightEngine.SetActive(false);
            _uiManager.UpdateLives(_lives);
        }
    }

}//end class
