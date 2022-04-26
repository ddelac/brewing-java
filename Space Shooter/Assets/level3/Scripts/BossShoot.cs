using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{

    private float _speed = 1f;

    private float widthOrtho;

    private int state = 0;

    private float _fireRate = 0.75f;//holds the fire rate of the laser
    private float _canFire = -.1f;//used to stop unlimited firing of the laser

    [SerializeField]
    private GameObject _bossShotPrefab;//holds the gameobject of bossShot for boss lasers

    [SerializeField]
    private GameObject bossLaser;

    // Start is called before the first frame update
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        widthOrtho = Camera.main.orthographicSize * screenRatio;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        movement(widthOrtho);
        if(Time.time > _canFire)
        {
           shoot();
        }
    }

    void movement(float w)
    {
        if (state == 0)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }

        if (state == 1)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (transform.position.x > w)
        {
            state = 1;
        }

        if (transform.position.x < -w)
        {
            state = 0;
        }
    }

    void shoot()
    {
        _canFire = Time.time + _fireRate;
        
        Instantiate(_bossShotPrefab, transform.position + new Vector3(-1.25f, 1.25f, 0), Quaternion.Euler(0,0,-180));
    }
}
