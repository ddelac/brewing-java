using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Playerlvl2 _player;
    private SpawnManagerlvl2 _spawn;

    [SerializeField]
    public GameObject explosionEffect;

    private AudioSource _audioSource;


    void Start(){
        _player = GameObject.Find("Player").GetComponent<Playerlvl2>();//assign player component to access player methods
        if(_player == null){
            Debug.LogError("Player is null");
        }
        _audioSource = GetComponent<AudioSource>();
        if(_audioSource == null){
            Debug.LogError("Audio Source on enemy is NULL");
        }
        _spawn = GameObject.Find("SpawnManager").GetComponent<SpawnManagerlvl2>();
        if(_spawn == null){
            Debug.LogError("spawn is null");
        }
    }

    // Update is called once per frame
    void Update(){
        ScrubEnemyMovement();
    }

    void ScrubEnemyMovement(){
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4f){
            float xPos = Random.Range(-10, 10);
            transform.position = new Vector3(xPos, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player"){

            Playerlvl2 player = other.transform.GetComponent<Playerlvl2>();
            if(player != null){
                player.Damage();
            }
            Destroy(GetComponent<Collider2D>());
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(other.tag == "Enemy"){
            Destroy(GetComponent<Collider2D>());
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject); 
        }

        if(other.tag == "Laser" || other.tag == "TripleShot"){
            Destroy(other.gameObject);
            if(_player != null){
                _player.addScore(10);
                _spawn.counter(1);
            }
            Destroy(GetComponent<Collider2D>());
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(other.tag == "FMJ"){ 
            if(_player != null){
                _player.addScore(10);
                _spawn.counter(1);
            }
            Destroy(GetComponent<Collider2D>());
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
