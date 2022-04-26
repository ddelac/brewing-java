using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlvl2 : MonoBehaviour{

    [SerializeField]
    private float _speed = 5f;
    private float _SpeedMutiliplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _FMJPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -.1f;

    [SerializeField]
    private int _lives = 4;
    private SpawnManagerlvl2 _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isFMJActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private GameObject _shieldVisualizer;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    [SerializeField]
    public int _score;
    private lvl2UI _uiManager;

    [SerializeField]
    private AudioClip _laserShot;
    private AudioSource _audioSource;

    [SerializeField]
    public GameObject explosionEffect;

    void Start(){

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerlvl2>();
        _uiManager = GameObject.Find("Canvas").GetComponent<lvl2UI>();
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
    }


    void Update(){
        movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire){
            shooting();
        }
    }

    void movement(){

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //                  new Vector3(1,0,0) * UsrInput * 3.5 * real time
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        //optimization of above code
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);


        //cieling and floor on Y axis 
        if (transform.position.y >= 4){
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        else if (transform.position.y <= -4){
            transform.position = new Vector3(transform.position.x, -4, 0);
        }

        if (transform.position.x > 11){
            transform.position = new Vector3(-10, transform.position.y, 0);
        }
        else if (transform.position.x < -11){
            transform.position = new Vector3(10, transform.position.y, 0);
        }//end if ekse

    }//end movement

    void shooting(){
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive){
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
        }if(_isFMJActive == true){
            Instantiate(_FMJPrefab, transform.position, Quaternion.identity);
        }
        else{
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyLaser"){
            Destroy(other.gameObject);
            Damage();
            Destroy(GetComponent<Collider2D>());
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
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

        public void onFMJCollection()
    {
        _isFMJActive = true;
        StartCoroutine(FMJPowerDown());
    }

    IEnumerator FMJPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _isFMJActive = false;

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
