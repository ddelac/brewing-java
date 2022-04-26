using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Player : MonoBehaviour
{

    private float _rotationSpeed = 150f;
    private float _maxSpeed = 5.0f;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private GameObject _bulletPrefab;//holds the gameobject of bulletprefab for spawning bullets
    [SerializeField]
    private GameObject _bombPrefab;//holds the gameobject of bombprefab for spawning bombs
    [SerializeField]
    private GameObject _misslePrefab;//holds the gameobject of missleprefab for spawning missles


    [SerializeField]
    private float _bulletFireRate = 0.15f;//holds the fire rate of the laser
    [SerializeField]
    private float _bombFireRate = 3.0f;
    [SerializeField]
    private float _missleFireRate = 10.0f;


    private float _canBulletFire = -.1f;//used to stop unlimited firing of the laser
    private float _canBombFire = -.1f;
    private float _canMissleFire = -.1f;

    private bool _bulletActive;
    private bool _bombActive;
    private bool _missleActive;

    [SerializeField]
    private GameObject explosion;

    public HealthBar healthbar;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
        _bulletActive = true;
        _bombActive = false;
        _missleActive = false;
        healthbar.setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        //update movement
        Movement();

        //if shift pressed run changeWeapon method
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            changeWeapon();
        }

        //if spacebar pressed run shooting method
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting();
        }

        
    }

    void Movement()
    {
        //rotate the ship
        //get the z euler angle
        float z = transform.rotation.eulerAngles.z;

        //change the euler angle based on horizontal input
        z += -Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime;

        //change the rotation
        transform.rotation = Quaternion.Euler(0, 0, z);

        //move the ship
        Vector3 velocity =  new Vector3(0, Input.GetAxis("Vertical") * _maxSpeed * Time.deltaTime, 0);

        transform.position += Quaternion.Euler(0, 0, z) * velocity;

        //cieling and floor on Y axis 
        if (transform.position.y >= 7.5)
        {
            transform.position = new Vector3(transform.position.x, 7.5f, 0);
        }
        else if (transform.position.y <= -7.5)
        {
            transform.position = new Vector3(transform.position.x, -7.5f, 0);
        }//end if else
        //optimization of above code
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.posotion.y,-4,0),0);
        //Math.Clamp(variable to clamp, value 1, value 2) keeps clamped variable between value 1 and value 2

        //wrapping the X axis' 
        if (transform.position.x > 10.5)
        {
            transform.position = new Vector3(10.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -10.5)
        {
            transform.position = new Vector3(-10.5f, transform.position.y, 0);
        }//end if ekse
    }

    void shooting()
    {
        //if i hit the space key
        //spawn game object
        //Debug.Log("Space Key Pressed");
        //Shows message in the terminal if working properly


        //instantiate bullet if currently selected
        if (_bulletActive)
        {
            
            if (Time.time > _canBulletFire)
            {
                _canBulletFire = Time.time + _bulletFireRate;
                Vector3 offset = transform.rotation * new Vector3(0, .7f, 0);
                Instantiate(_bulletPrefab, transform.position + offset, transform.rotation);
            }

        }

        //instantiate bomb if currently selected
        if (_bombActive)
        {
            
            if(Time.time > _canBombFire)
            {
                _canBombFire = Time.time + _bombFireRate;
                Vector3 offset = transform.rotation * new Vector3(0, .7f, 0);
                Instantiate(_bombPrefab, transform.position + offset, transform.rotation);

            }
            
        }

        //instantiate missle if currently selected
        if (_missleActive)
        {
            
            if(Time.time > _canMissleFire)
            {
                _canMissleFire = Time.time + _missleFireRate;
                Vector3 offset = transform.rotation * new Vector3(0, .7f, 0);
                Instantiate(_misslePrefab, transform.position + offset, transform.rotation);

            }
            
        }


    }

    void changeWeapon()
    {
        if (_bulletActive)
        {
            _bulletActive = false;
            _bombActive = true;
        }

        else if (_bombActive)
        {
            _bombActive = false;
            _missleActive = true;
        }

        else if (_missleActive)
        {
            _missleActive = false;
            _bulletActive = true;
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
        healthbar.setHealth(health);
        if (health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
