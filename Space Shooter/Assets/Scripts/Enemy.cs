using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;

    //handle to animator
    //private Animator _anim;

    //explosion effect
    [SerializeField]
    public GameObject explosionEffect;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();//assign player component to access player methods
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }

       /* _anim = GetComponent <Animator>();
        if (_player == null)
        {
            Debug.LogError("Animator is null"); 
        }*/

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on enemy is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ScrubEnemyMovement();
    }

    void ScrubEnemyMovement()
    {
        //move enemy down 4 m/s
        //when goes off screen
        //respawn at top w/ new random x position
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4f)
        {
            float xPos = Random.Range(-10, 10);
            transform.position = new Vector3(xPos, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage player
        //destroy US
        if(other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }

            //destroy collider
            Destroy(GetComponent<Collider2D>());
            //trigger anim
            //_anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            //_audioSource.Play();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Destroy(this.gameObject,2.5f);
            
        }

        //if other is laser
        //destroy laser
        //destroy us
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);//destroy laser
            if(_player != null)
            {
                _player.addScore(10);
            }

            //destroy collider
            Destroy(GetComponent<Collider2D>());
            //trigger anim
            //_anim.SetTrigger("OnEnemyDeath");
            //_speed = 0;
            //_audioSource.Play();
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //Destroy(this.gameObject,2.5f);//destroy enemy

        }

    }
}
