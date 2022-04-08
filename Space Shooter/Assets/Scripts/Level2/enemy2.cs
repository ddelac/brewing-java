using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour{

    [SerializeField]
    private float _speed = 4f;
    private Playerlvl2 _player;
    [SerializeField]
    public GameObject explosionEffect;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Playerlvl2>();//assign player component to access player methods
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }

        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source on enemy is NULL");
        }
    }

    // Update is called once per frame
    void Update(){
        enmov();
    }

    void enmov(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4f){
            float xPos = Random.Range(-10, 10);
            transform.position = new Vector3(xPos, 7, 0);
        }

        Vector3 direction = new Vector3(0f, 0f, .5f);
        transform.Rotate(direction);
    }

    private void OnTriggerEnter2D(Collider2D other){
        //if other is player
        //damage player
        //destroy US
        if(other.tag == "Player"){
            //damage player
            Playerlvl2 player = other.transform.GetComponent<Playerlvl2>();
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
                _player.addScore(15);
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
